using System.Net;
using System.Reflection;
using Application;
using HealthChecks.UI.Client;
using Infrastructure;
using Infrastructure.Database;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using OpenIddict.Abstractions;
using Serilog;
using Web.Api;
using Web.Api.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string[] allowedOrigins = builder.Configuration
                                    .GetSection("Cors:AllowedOrigins")
                                    .Get<string[]>() ?? [];

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddSwaggerGenWithAuth();

builder.Services.AddCertificateForwarding(options => options.CertificateHeader = "X-SSL-CERT");

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options => options.AddPolicy("Allowed_Origins", builder => builder
            .WithOrigins(allowedOrigins)
            .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH")
            .WithHeaders("Content-Type", "Authorization")
            .AllowCredentials()));

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
        ArgumentNullException.ThrowIfNull(options);
        options.UseEntityFrameworkCore()
               .UseDbContext<ApplicationDbContext>();
    })
    .AddServer(options =>
    {
        options.SetAuthorizationEndpointUris("/connect/authorize");
        options.SetTokenEndpointUris("/connect/token");
        options.SetUserInfoEndpointUris("/connect/userinfo");
        options.SetIssuer(builder.Configuration["IssuerUrl"] ?? "https://" + "localhost:5001"); // Issuer URL (authapi)
        options.AllowAuthorizationCodeFlow()
               .RequireProofKeyForCodeExchange();

        options.AddDevelopmentEncryptionCertificate()
               .AddDevelopmentSigningCertificate();

        options.RegisterScopes(
            OpenIddictConstants.Scopes.OpenId,
            OpenIddictConstants.Scopes.Profile,
            OpenIddictConstants.Scopes.Email);

        options.UseAspNetCore()
               .EnableAuthorizationEndpointPassthrough()
               .EnableTokenEndpointPassthrough()
               .EnableUserInfoEndpointPassthrough();
    });

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "AuthCookie";
        options.DefaultSignInScheme = "AuthCookie";
        options.DefaultChallengeScheme = "AuthCookie";
    })
    .AddCookie("AuthCookie", options =>
    {
        options.Cookie.Name = "auth_server_session";
        options.Cookie.HttpOnly = true;
        //options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SameSite = SameSiteMode.None;

        options.Cookie.SecurePolicy = builder.Configuration.GetValue<bool>("AuthServer:AlwaysHTTPS")
                ? CookieSecurePolicy.Always
                : CookieSecurePolicy.SameAsRequest;
        options.LoginPath = "/login";
        // do NOT redirect for API calls
        options.Events.OnRedirectToLogin = context =>
        {
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            }

            string frontendLoginUrl = builder.Configuration["FrontendLoginUrl"] ?? "http://localhost:4200/login"; // Frontend login URL
            string redirectUri = Uri.EscapeDataString(context.Request.Path + context.Request.QueryString);
            string finalRedirectUrl = $"{frontendLoginUrl}?ReturnUrl={redirectUri}";

            context.Response.Redirect(finalRedirectUrl);
            return Task.CompletedTask;
        };
    });

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// ------------------ proxy (need for SSL)----------------------
string[]? knownProxies = builder.Configuration.GetSection("KnownProxies").Get<string[]>();

var forwardedHeadersOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto
};

if (knownProxies != null)
{
    foreach (string proxy in knownProxies)
    {
        if (System.Net.IPAddress.TryParse(proxy, out IPAddress? ip))
        {
            forwardedHeadersOptions.KnownProxies.Add(ip);
        }
    }
}
else
{
    forwardedHeadersOptions.KnownProxies.Add(IPAddress.Parse("172.18.240.206"));
}
app.UseForwardedHeaders(forwardedHeadersOptions);
// ------------------------------------------------------------

app.UseCertificateForwarding();

app.MapGet("/remote-ip", (HttpContext ctx) => ctx.Connection.RemoteIpAddress?.ToString());

app.UseRequestContextLogging();

app.UseExceptionHandler();

app.UseSerilogRequestLogging();

app.UseRouting();

app.UseCors("Allowed_Origins");

app.UseAuthentication();

app.UseAuthorization();

app.MapEndpoints();

await app.RunAsync();

// REMARK: Required for functional and integration tests to work.
namespace Web.Api
{
    public partial class Program;
}
