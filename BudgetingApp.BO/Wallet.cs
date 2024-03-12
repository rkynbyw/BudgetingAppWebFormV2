using System.Collections.Generic;

namespace BudgetingApp.BO
{
    public class Wallet
    {
        public Wallet()
        {
            this.Transactions = new List<Transaction>();
        }

        public int WalletID { get; set; }
        public int WalletTypeID { get; set; }
        public decimal Balance { get; set; }
        public int UserID { get; set; }

        public string WalletName { get; set; }

        public string Name { get; set; }


        public WalletType WalletType { get; set; }
        public User User { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
