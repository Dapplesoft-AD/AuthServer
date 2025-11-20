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
}
