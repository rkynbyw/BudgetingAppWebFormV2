using System.ComponentModel.DataAnnotations;
namespace BudgetingApp.BLL.DTOs

{
    public class UserCreateDTO
    {
        public int UserID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RePassword { get; set; }
    }
}
