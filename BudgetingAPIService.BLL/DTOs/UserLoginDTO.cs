using System.ComponentModel.DataAnnotations;

namespace BudgetingAPIService.BLL.DTOs
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
