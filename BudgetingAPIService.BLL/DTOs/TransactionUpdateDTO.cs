namespace BudgetingAPIService.BLL.DTOs
{
    public class TransactionUpdateDTO
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public int TransactionCategoryID { get; set; }
        public int Amount { get; set; }
        public int WalletID { get; set; }
        public string? Description { get; set; }
    }
}
