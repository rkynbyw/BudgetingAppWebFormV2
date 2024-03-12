using BudgetingApp.BO;
using System.Collections.Generic;

namespace BudgetingApp.DAL.Interfaces
{
    public interface IWalletDAL : Icrud<Wallet>
    {
        IEnumerable<Wallet> GetByWallet();
        IEnumerable<Wallet> GetByUser();
        IEnumerable<Wallet> GetWalletByType(int WalletTypeID);
        void TransferWallet(int WalletSCID, int WalletDSTID, int Amount);
        IEnumerable<Wallet> GetWalletDataByUser(int UserID);

        decimal GetTotalBalance(int id);
    }
}
