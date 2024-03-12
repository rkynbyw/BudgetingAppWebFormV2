using System;

namespace BudgetingApp.BO
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int WalletID { get; set; }
        public int TransactionCategoryID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }

        public Wallet Wallet { get; set; }
        public TransactionCategory TransactionCategory { get; set; }
        public User User { get; set; }
    }
}
