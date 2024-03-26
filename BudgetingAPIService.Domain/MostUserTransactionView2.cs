using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetingAPIService.Domain;

public partial class MostUserTransactionView2
{
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(50)]
    public string FullName { get; set; } = null!;

    public int? CountTransaction { get; set; }

    [Column("Wallet Balance", TypeName = "money")]
    public decimal? WalletBalance { get; set; }
}
