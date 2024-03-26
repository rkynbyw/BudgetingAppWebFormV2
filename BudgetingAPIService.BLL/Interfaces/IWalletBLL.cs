using BudgetingAPIService.BLL.DTOs;

namespace BudgetingAPIService.BLL.Interfaces
{
    public interface IWalletBLL
    {
        IEnumerable<WalletDTO> GetAll();
        WalletDTO GetById(int id);
        IEnumerable<WalletDTO> GetWalletByType(int WalletTypeID);
        IEnumerable<WalletDTO> GetWalletDataByUser(int UserID);
        void Insert(WalletCreateDTO entity);
        void Update(WalletUpdateDTO entity);
        void Delete(int id);

        decimal GetTotalBalance(int id);
    }
}
