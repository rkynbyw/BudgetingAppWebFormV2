using BudgetingAPIService.BLL.DTOs;

namespace BudgetingAPIService.BLL.Interfaces
{
    public interface IWalletTypeBLL
    {
        IEnumerable<WalletTypeDTO> GetAll();
    }
}
