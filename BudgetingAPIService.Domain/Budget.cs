using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetingAPIService.Domain;

[Table("Budget")]
public partial class Budget
{
    [Key]
    [Column("BudgetID")]
    public int BudgetId { get; set; }

    public DateOnly MonthDate { get; set; }

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [Column("TransactionCategoryID")]
    public int? TransactionCategoryId { get; set; }

    [ForeignKey("TransactionCategoryId")]
    [InverseProperty("Budgets")]
    public virtual TransactionCategory? TransactionCategory { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Budgets")]
    public virtual User User { get; set; } = null!;
}
