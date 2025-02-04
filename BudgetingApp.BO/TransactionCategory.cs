﻿using System.Collections.Generic;

namespace BudgetingApp.BO
{
    public class TransactionCategory
    {
        public TransactionCategory()
        {
            this.Transactions = new List<Transaction>();
        }

        public int TransactionCategoryID { get; set; }
        public int TransactionTypeID { get; set; }
        public string Name { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
