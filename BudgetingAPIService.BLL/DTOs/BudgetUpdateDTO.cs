namespace BudgetingAPIService.BLL.DTOs
{
    public class BudgetUpdateDTO
    {
        public int BudgetID { get; set; }
        public DateTime MonthDate { get; set; }
        public decimal Amount { get; set; }
        public int UserID { get; set; }
        public int? TransactionCategoryID { get; set; }
    }
}
