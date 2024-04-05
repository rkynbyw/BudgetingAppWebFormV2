using System.ComponentModel.DataAnnotations;

namespace BudgetingApp.APIMVC.Models
{
    public class UserLogin
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
