using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetingAPIService.Domain;

[Table("Wallet")]
public partial class Wallet
{
    [Key]
    [Column("WalletID")]
    public int WalletId { get; set; }

    [Column("WalletTypeID")]
    public int WalletTypeId { get; set; }

    [Column(TypeName = "money")]
    public decimal Balance { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [InverseProperty("Wallet")]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    [ForeignKey("UserId")]
    [InverseProperty("Wallets")]
    public virtual User User { get; set; } = null!;

    [ForeignKey("WalletTypeId")]
    [InverseProperty("Wallets")]
    public virtual WalletType WalletType { get; set; } = null!;
}
