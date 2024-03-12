namespace BudgetingApp.BLL.DTOs
{
    public class UserCreateDTO
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
}
