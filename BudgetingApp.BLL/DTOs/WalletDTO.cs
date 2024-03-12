namespace BudgetingApp.BLL.DTOs
{
    public class WalletDTO
    {
        public int WalletID { get; set; }
        public int WalletTypeID { get; set; }
        public decimal Balance { get; set; }
        public int UserID { get; set; }

        public string Name { get; set; }

        public WalletTypeDTO WalletType { get; set; }
        public string WalletName { get; set; }





    }
}
