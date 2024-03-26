using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetingAPIService.Domain;

[Table("WalletType")]
public partial class WalletType
{
    [Key]
    [Column("WalletTypeID")]
    public int WalletTypeId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("WalletType")]
    public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}
