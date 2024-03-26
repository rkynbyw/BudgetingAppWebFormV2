using System.ComponentModel.DataAnnotations;
namespace BudgetingAPIService.BLL.DTOs

{
    public class UserCreateDTO
    {
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
