namespace BudgetingAPIService.BLL.DTOs
{
    public class BudgetDTO
    {
        public int BudgetID { get; set; }
        public DateTime MonthDate { get; set; }
        public decimal Amount { get; set; }
        public int UserID { get; set; }
        public int? TransactionCategoryID { get; set; }
        public string TransactionCategoryName { get; set; }
        public decimal Expense { get; set; } // Tambahkan properti Expense di sini

        public decimal RemainingAmount { get; set; }
    }
}
