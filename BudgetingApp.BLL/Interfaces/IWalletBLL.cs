using BudgetingApp.BLL.DTOs;
using System.Collections.Generic;

namespace BudgetingApp.BLL.Interfaces
{
    public interface IWalletBLL
    {
        IEnumerable<WalletDTO> GetAll();
        WalletDTO GetById(int id);
        IEnumerable<WalletDTO> GetWalletByType(int WalletTypeID);

        IEnumerable<WalletDTO> GetWalletDataByUser(int UserID);
        void Insert(WalletDTO entity);
        void Update(WalletDTO entity);
        void Delete(int id);

        decimal GetTotalBalance(int id);


    }
}
