using System.Security.Claims;
using Application.Abstractions.Authentication;
using Domain.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Infrastructure.Authentication;

public sealed class UserSession : IUserSession
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserSession(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;

    public async Task SignInAsync(User user, CancellationToken cancellationToken)
    {
        //var identity = new ClaimsIdentity(
        //    OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
        //    Claims.Name, Claims.Role);

        var identity = new ClaimsIdentity(
            "AuthCookie",
            ClaimTypes.Name,
            ClaimTypes.Role);


        identity.AddClaim(OpenIddictConstants.Claims.Subject, user.Id.ToString());
        identity.AddClaim(OpenIddictConstants.Claims.Email, user.Email);

        var principal = new ClaimsPrincipal(identity);

        await _httpContextAccessor.HttpContext!.SignInAsync("AuthCookie", principal);
    }
}
