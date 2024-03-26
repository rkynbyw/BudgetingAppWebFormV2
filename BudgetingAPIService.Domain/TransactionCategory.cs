using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BudgetingAPIService.Domain;

[Table("TransactionCategory")]
public partial class TransactionCategory
{
    [Key]
    [Column("TransactionCategoryID")]
    public int TransactionCategoryId { get; set; }

    [Column("TransactionTypeID")]
    public int TransactionTypeId { get; set; }

    [StringLength(45)]
    public string Name { get; set; } = null!;

    [InverseProperty("TransactionCategory")]
    public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();

    [InverseProperty("TransactionCategory")]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
