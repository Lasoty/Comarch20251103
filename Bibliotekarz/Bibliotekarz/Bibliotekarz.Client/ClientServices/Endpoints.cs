namespace Bibliotekarz.Client.ClientServices;

public static class Endpoints
{
    public static class Books
    {
        public static string GetAll = "api/Books";
    }

    public static class Auth
    {
        public static string Register = "api/auth/register";
        public static string Login = "api/auth/login";
    }
}
