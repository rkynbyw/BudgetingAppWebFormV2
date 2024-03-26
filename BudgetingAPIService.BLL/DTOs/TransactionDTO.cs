﻿namespace BudgetingAPIService.BLL.DTOs
{
    public class TransactionDTO
    {
        public int TransactionID { get; set; }
        public int WalletID { get; set; }

        public int TransactionCategoryID { get; set; }
        public int TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        public string WalletName { get; set; }
    }
}
