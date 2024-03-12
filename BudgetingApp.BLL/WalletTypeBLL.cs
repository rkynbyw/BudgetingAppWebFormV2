using BudgetingApp.BLL.DTOs;
using BudgetingApp.BLL.Interfaces;
using BudgetingApp.DAL;
using BudgetingApp.DAL.Interfaces;
using System.Collections.Generic;

namespace BudgetingApp.BLL
{

    public class WalletTypeBLL : IWalletTypeBLL
    {
        private readonly IWalletTypeDAL _walletTypeDAL;

        public WalletTypeBLL()
        {
            _walletTypeDAL = new WalletTypeDAL();
        }

        public IEnumerable<WalletTypeDTO> GetAll()
        {
            List<WalletTypeDTO> listWalletsTypeDto = new List<WalletTypeDTO>();
            var walletstype = _walletTypeDAL.GetAll();
            foreach (var wallettype in walletstype)
            {
                listWalletsTypeDto.Add(new WalletTypeDTO
                {
                    WalletTypeID = wallettype.WalletTypeID,
                    Name = wallettype.Name
                });
            }

            return listWalletsTypeDto;
        }
    }
}
