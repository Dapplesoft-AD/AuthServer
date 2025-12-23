using System.Collections.Immutable;
using Application.Abstractions.Openiddict;
using OpenIddict.Abstractions;
using SharedKernel.Models;

namespace Infrastructure.Openiddict;

internal class OpenIddictClientManager : IOpeniddictClientManager
{
    private readonly IOpenIddictApplicationManager manager;

    public OpenIddictClientManager(IOpenIddictApplicationManager manager)
    {
        this.manager = manager;
    }

    public async Task<bool> IsAlreadyExist(string ClientId, CancellationToken ct)
    {
        return await manager.FindByClientIdAsync(ClientId, ct) is not null;
    }

    public async Task CreateAsync(ClientDescriptor descriptor, CancellationToken ct)
    {
        if (await IsAlreadyExist(descriptor.ClientId, ct))
        {
            throw new InvalidOperationException("Client already exists");
        }

        var app = new OpenIddictApplicationDescriptor
        {
            ClientId = descriptor.ClientId,
            DisplayName = descriptor.DisplayName,
            ClientSecret = descriptor.ClientSecret
        };

        foreach (Uri uri in descriptor.RedirectUris)
        {
            app.RedirectUris.Add(uri);
        }

        app.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Authorization);
        app.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Token);
        app.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode);
        app.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.Code);
        app.Permissions.Add(OpenIddictConstants.Permissions.Scopes.Profile);
        app.Permissions.Add(OpenIddictConstants.Permissions.Scopes.Email);

        app.Requirements.Add(
            OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange);

        await manager.CreateAsync(app, ct);
    }

    public async Task DeleteAsync(string clientId, CancellationToken ct)
    {
        object? app = await manager.FindByClientIdAsync(clientId, ct);
        if (app is null)
        {
            return;
        }

        await manager.DeleteAsync(app, ct);
    }
    public async Task<IReadOnlyList<ClientView>> GetAllAsync(CancellationToken ct)
    {
        var result = new List<ClientView>();

        await foreach (object app in manager.ListAsync(count: null, offset: null, ct))
        {
            string? clientId = await manager.GetClientIdAsync(app, ct);
            string? displayName = await manager.GetDisplayNameAsync(app, ct);
            string? clientType = await manager.GetClientTypeAsync(app, ct);

            var redirectUris =
                (await manager.GetRedirectUrisAsync(app, ct))
                    .Select(u => RemoveSigninOidc(u.ToString()))
                    .ToList();

            ImmutableArray<string> permissions = await manager.GetPermissionsAsync(app, ct);

            result.Add(new ClientView
            {
                ClientId = clientId!,
                DisplayName = displayName,
                ClientType = clientType,
                RedirectUris = redirectUris,
                Scopes = permissions
                    .Where(p => p.StartsWith(OpenIddictConstants.Permissions.Prefixes.Scope, StringComparison.Ordinal)) // Change here
                    .Select(p => p.Replace(
                        OpenIddictConstants.Permissions.Prefixes.Scope, ""))
                    .ToList(),

                GrantTypes = permissions
                    .Where(p => p.StartsWith(OpenIddictConstants.Permissions.Prefixes.GrantType, StringComparison.Ordinal)) // Change here
                    .Select(p => p.Replace(
                        OpenIddictConstants.Permissions.Prefixes.GrantType, ""))
                    .ToList()
            });
        }

        return result;
    }

    private static string RemoveSigninOidc(string uri)
    {
        var builder = new UriBuilder(uri);

        string path = builder.Path.TrimEnd('/');

        if (path.EndsWith("/signin-oidc", StringComparison.OrdinalIgnoreCase))
        {
            builder.Path = path[..^"/signin-oidc".Length];

            if (string.IsNullOrEmpty(builder.Path))
            {
                builder.Path = "/";
            }
        }

        return builder.Uri.ToString();
    }

}
