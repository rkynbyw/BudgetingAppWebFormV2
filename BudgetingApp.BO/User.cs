using System.Collections.Generic;

namespace BudgetingApp.BO
{
    public class User
    {
        public User()
        {
            //this.Budgets = new List<Budget>();
            this.Transactions = new List<Transaction>();
            this.Wallets = new List<Wallet>();
        }

        public int UserID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }


        //public List<Budget> Budgets { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<Wallet> Wallets { get; set; }
    }

}
