namespace BudgetingAPIService.BLL.DTOs
{
    public class WalletUpdateDTO
    {
        public int WalletID { get; set; }
        public decimal Balance { get; set; }
        public int UserID { get; set; }
        public string WalletName { get; set; }
    }
}
