using BudgetingApp.BLL.DTOs;
using System.Collections.Generic;

namespace BudgetingApp.BLL.Interfaces
{
    public interface IWalletTypeBLL
    {
        IEnumerable<WalletTypeDTO> GetAll();
    }
}
