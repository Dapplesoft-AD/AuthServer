namespace Web.Api.Endpoints;

public static class ApiRoutes
{
    public static class Users
    {
        public const string Register = "api/users/register";
        public const string Login = "api/users/login";
        public const string Update = "api/users/{id:guid}";
        public const string GetByID = "api/users/{id:guid}";
        public const string Delete = "api/my-account/{id:guid}";
    }

    public static class UserProfile
    {
        public const string Create = "api/user-profile/{id:guid}";
        public const string Get = "api/user-profile/{id:guid}";
    }

    public static class UserLoginHistory
    {
        public const string Create = "api/user-login-history";
        public const string Delete = "api/user-login-history/{id:guid}";
        public const string GetByUserID = "api/user-login-history/{id:guid}";
    }

}
