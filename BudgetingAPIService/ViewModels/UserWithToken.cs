namespace MyRESTServices.ViewModels
{
    public class UserWithToken
    {
        public int? UserID { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
    }

}
