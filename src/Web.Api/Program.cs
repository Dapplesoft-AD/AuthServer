using System.Reflection;
using Application;
using HealthChecks.UI.Client;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Web.Api;
using Web.Api.Endpoints.AuditLogs;
using Web.Api.Endpoints.MfaSettings;
using Web.Api.Endpoints.MfaLogs; 
using Web.Api.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Logging (Serilog)
builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

// Swagger with Auth
builder.Services.AddSwaggerGenWithAuth();

// Application Layers
builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

// Controllers with JSON enum support
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter()));

// Minimal API endpoint discovery
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

WebApplication app = builder.Build();

// --------------------------------------------------
// 🚀 Map Endpoints (AuditLogs + MfaSettings + MfaLogs)
// --------------------------------------------------

// Minimal API discovery
app.MapEndpoints();

//--------AuditLogs Endpoints--------
app.MapUpdateAuditLogEndpoint();
app.MapGetAuditLogsEndpoint();
app.MapGetAuditLogByIdEndpoint();
app.MapDeleteAuditLogEndpoint();

//--------MfaSettings Endpoints--------

app.MapUpdateMfaSettingEndpoint();
app.MapGetMfaSettingsEndpoint();
app.MapGetMfaSettingByIdEndpoint();
app.MapDeleteMfaSettingEndpoint();

// -------- MfaLogs Endpoints --------
app.MapUpdateMfaLogEndpoint();
app.MapGetMfaLogsEndpoint();
app.MapGetMfaLogByIdEndpoint();
app.MapDeleteMfaLogEndpoint();

// --------------------------------------------------
// Development Tools
// --------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
    app.ApplyMigrations();
}

// Health Check
app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// Logging middlewares
app.UseRequestContextLogging();
app.UseSerilogRequestLogging();

// Global exception handler
app.UseExceptionHandler();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// MVC Controller support
app.MapControllers();

await app.RunAsync();

// Needed for integration tests
namespace Web.Api
{
    public partial class Program { }
}
