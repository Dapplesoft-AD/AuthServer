
namespace Web.Api.Extensions;

internal static class AuthenticationExtensions
{
    internal static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
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

                options.Cookie.SecurePolicy = configuration.GetValue<bool>("AuthServer:AlwaysHTTPS")
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

                    string frontendLoginUrl = configuration["FrontendLoginUrl"] ?? "http://localhost:4200/login"; // Frontend login URL
                    string redirectUri = Uri.EscapeDataString(context.Request.Path + context.Request.QueryString);
                    string finalRedirectUrl = $"{frontendLoginUrl}?ReturnUrl={redirectUri}";

                    context.Response.Redirect(finalRedirectUrl);
                    return Task.CompletedTask;
                };

                options.Events.OnRedirectToAccessDenied = context =>
                {
                    if (context.Request.Path.StartsWithSegments("/api"))
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return Task.CompletedTask;
                    }

                    return Task.CompletedTask;
                };
            });

        return services;
    }
}
