using System.ComponentModel.DataAnnotations;

namespace BudgetingApp.BLL.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
