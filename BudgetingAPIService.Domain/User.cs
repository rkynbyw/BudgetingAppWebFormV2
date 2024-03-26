using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetingAPIService.Domain;

public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(50)]

    public string Email { get; set; } = null!;

    [StringLength(50)]

    public string Username { get; set; } = null!;

    [StringLength(50)]
    public string FullName { get; set; } = null!;

    [StringLength(50)]
    public string? Password { get; set; }

    [StringLength(50)]
    public string? Role { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();

    [InverseProperty("User")]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    [InverseProperty("User")]
    public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}
