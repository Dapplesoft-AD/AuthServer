namespace Web.Api.Endpoints;

public static class ApiRoutes
{
    private const string _Prefix = "api/";
    private const string _Login = "/signin";
    private const string _Create = "";
    private const string _Register = "/signup";
    private const string _GetAll = "";
    private const string _GetById = "/{id:guid}";
    private const string _Update = "/{id:guid}";
    private const string _Delete = "/{id:guid}";

    private static string Path(string resource, string suffix)
        => _Prefix + resource + suffix;

    public static string Login(string resource) => Path(resource, _Login);
    public static string Register(string resource) => Path(resource, _Register);
    public static string Create(string resource) => Path(resource, _Create);
    public static string GetAll(string resource) => Path(resource, _GetAll);
    public static string GetById(string resource) => Path(resource, _GetById);
    public static string Update(string resource) => Path(resource, _Update);
    public static string Delete(string resource) => Path(resource, _Delete);
}

public static class Base
{
    public const string User = "user"; // login, reg
    public const string Users = "users";
    public const string UserProfile = "user-profile";
    public const string UserLoginHistory = "user-login-history";
    public const string Areas = "areas";
    public const string AuditLogs = "audit-logs";
    public const string ClientApps = "client-apps";
    public const string CommonOtp = "otp";
    public const string Projects = "projects";
    public const string Countries = "countries";
    public const string Customers = "customers";
    public const string Districts = "districts";
    public const string EmailVerifications = "email-verifications";
    public const string Localities = "email-localities";
    public const string Mfalogs = "mfa-logs";
    public const string MfaSettings = "mfa-settings";
    public const string PasswordResets = "password-resets";
    public const string Permissions = "permissions";
    public const string Regions = "regions";
    public const string OtpRequest = "otp-request";
    public const string PasswordReset = "password-reset";
    public const string Roles = "roles";
    public const string OtpVerify = "otp-verify";
    public const string ForgotPassReset = "forgot-password-reset";
    public const string Addresses = "addresses";
}
