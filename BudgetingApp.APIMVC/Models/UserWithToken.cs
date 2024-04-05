namespace BudgetingApp.APIMVC.Models
{
    public class UserWithToken
    {
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
