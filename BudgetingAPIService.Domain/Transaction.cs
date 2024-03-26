using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetingAPIService.Domain;

public partial class Transaction
{
    [Key]
    [Column("TransactionID")]
    public int TransactionId { get; set; }

    [Column("WalletID")]
    public int WalletId { get; set; }

    [Column("TransactionCategoryID")]
    public int TransactionCategoryId { get; set; }

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    public DateOnly? Date { get; set; }

    [StringLength(50)]
    public string? Description { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [ForeignKey("TransactionCategoryId")]
    [InverseProperty("Transactions")]
    public virtual TransactionCategory TransactionCategory { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Transactions")]
    public virtual User User { get; set; } = null!;

    [ForeignKey("WalletId")]
    [InverseProperty("Transactions")]
    public virtual Wallet Wallet { get; set; } = null!;
}
