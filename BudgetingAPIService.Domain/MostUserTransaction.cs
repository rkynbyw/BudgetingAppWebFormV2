using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetingAPIService.Domain;

//[Keyless]
public partial class MostUserTransaction
{
    [Column("UserID")]
    public int UserId { get; set; }

    [Column("Count Transaction")]
    public int? CountTransaction { get; set; }
}
