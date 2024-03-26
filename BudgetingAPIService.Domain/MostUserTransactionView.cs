using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetingAPIService.Domain;

//[Keyless]
public partial class MostUserTransactionView
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(50)]
    //[Unicode(false)]
    public string FullName { get; set; } = null!;

    public int? CountTransaction { get; set; }
}
