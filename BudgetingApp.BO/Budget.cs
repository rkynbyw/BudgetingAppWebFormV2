using System;

namespace BudgetingApp.BO
{
    public class Budget
    {
        public int BudgetID { get; set; }
        public DateTime MonthDate { get; set; }
        public decimal Amount { get; set; }
        public int UserID { get; set; }
        public int? TransactionCategoryID { get; set; }

        public User User { get; set; }
        public TransactionCategory TransactionCategory { get; set; }
    }
}
