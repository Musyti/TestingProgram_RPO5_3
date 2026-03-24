namespace Bubnov_omg
{
    public class User
    {
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = "User";

        public bool IsAdmin => Role == "Admin";

        // Простая проверка админа
        public static bool IsAdminLogin(string login, string password)
        {
            return login == "admin" && password == "admin123";
        }
    }
}