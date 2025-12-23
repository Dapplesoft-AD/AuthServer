using System.Reflection;
using Application;
using HealthChecks.UI.Client;
using Infrastructure;
using Infrastructure.Database;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Org.BouncyCastle.Ocsp;
using Serilog;
using Web.Api;
using Web.Api.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
string[] allowedOrigins = builder.Configuration
                                    .GetSection("Cors:AllowedOrigins")
                                    .Get<string[]>() ?? [];

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddSwaggerGenWithAuth();

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options => options.AddPolicy("Allowed_Origins", builder => builder
            .WithOrigins(allowedOrigins)
            .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH")
            .WithHeaders("Content-Type", "Authorization")
            .AllowCredentials()));

builder.Services.AddCors(options => options.AddPolicy("LocalhostPolicy", policy => policy
            .SetIsOriginAllowed(origin =>
            {
                if (string.IsNullOrWhiteSpace(origin))
                {
                    return false;
                }

                var uri = new Uri(origin);

                return uri.Host == "localhost"
                    || uri.Host == "127.0.0.1";
            })
            .AllowAnyMethod()
            .AllowAnyHeader()
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
        options.SetIssuer(builder.Configuration["IssuerUrl"] ?? "https://"+"localhost:5001"); // Issuer URL (authapi)
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

builder.Services.AddAuthentication( options =>
    {
        options.DefaultAuthenticateScheme = "AuthCookie";
        options.DefaultSignInScheme = "AuthCookie";
        options.DefaultChallengeScheme = "AuthCookie";
    })
    .AddCookie("AuthCookie", options =>
    {
        options.Cookie.Name = "auth_server_session";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        //options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
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
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        options.OAuthClientId("swagger");
        //options.OAuthUsePkce();
        options.OAuthScopes("openid", "profile", "email", "api");
        options.OAuthAppName("Swagger UI");
    });

    app.ApplyMigrations();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHttpsRedirection();

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseRouting();

//if (app.Environment.IsDevelopment())
//{
//    app.UseCors("LocalhostPolicy");
//}
//else
//{
//    app.UseCors("Allowed_Origins");
//}

app.UseCors("Allowed_Origins");

app.UseAuthentication();

app.UseAuthorization();

app.MapEndpoints();

// REMARK: If you want to use Controllers, you'll need this.
app.MapControllers();

await app.RunAsync();

// REMARK: Required for functional and integration tests to work.
namespace Web.Api
{
    public partial class Program;
}
