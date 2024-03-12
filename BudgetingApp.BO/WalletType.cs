using System.Collections.Generic;

namespace BudgetingApp.BO
{
    public class WalletType
    {
        public WalletType()
        {
            this.Wallets = new List<Wallet>();
        }

        public int WalletTypeID { get; set; }
        public string Name { get; set; }

        public IEnumerable<Wallet> Wallets { get; set; }
    }
}
