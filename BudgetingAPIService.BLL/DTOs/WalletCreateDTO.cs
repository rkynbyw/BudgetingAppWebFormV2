namespace BudgetingAPIService.BLL.DTOs
{
    public class WalletCreateDTO
    {
        public decimal Balance { get; set; }
        public int UserID { get; set; }
        public string WalletName { get; set; }
    }
}
