using BudgetingAPIService.BLL.DTOs;
using BudgetingAPIService.BLL.Interfaces;
using BudgetingApp.BO;
using BudgetingApp.DAL;
using BudgetingApp.DAL.Interfaces;

namespace BudgetingAPIService.BLL
{
    public class WalletBLL : IWalletBLL
    {
        private readonly IWalletDAL _walletDAL;

        public WalletBLL()
        {
            _walletDAL = new WalletDAL();
        }

        IEnumerable<WalletDTO> IWalletBLL.GetWalletByType(int WalletTypeID)
        {
            List<WalletDTO> walletsbytypeDTO = new List<WalletDTO>();
            var wallets = _walletDAL.GetWalletByType(WalletTypeID);
            foreach (var wallet in wallets)
            {
                walletsbytypeDTO.Add(new WalletDTO
                {
                    WalletID = wallet.WalletID,
                    WalletTypeID = wallet.WalletTypeID,
                    Balance = wallet.Balance,
                    UserID = wallet.UserID,
                    WalletType = new WalletTypeDTO
                    {
                        WalletTypeID = wallet.WalletType.WalletTypeID,
                        Name = wallet.WalletType.Name
                    }
                });
            }
            return walletsbytypeDTO;
        }

        public IEnumerable<WalletDTO> GetAll()
        {
            List<WalletDTO> listWalletsDTO = new List<WalletDTO>();
            var wallets = _walletDAL.GetAll();
            foreach (var wallet in wallets)
            {
                listWalletsDTO.Add(new WalletDTO
                {
                    WalletID = wallet.WalletID,
                    WalletTypeID = wallet.WalletTypeID,
                    Balance = wallet.Balance,
                    UserID = wallet.UserID
                });

            }
            return listWalletsDTO;
        }



        public WalletDTO GetById(int id)
        {

            WalletDTO walletDto = new WalletDTO();
            var wallet = _walletDAL.GetById(id);
            if (wallet != null)
            {
                walletDto.WalletID = wallet.WalletID;
                walletDto.WalletTypeID = wallet.WalletTypeID;
                walletDto.Balance = wallet.Balance;
                walletDto.UserID = wallet.UserID;
                walletDto.WalletName = wallet.WalletType.Name;
                walletDto.WalletType = new WalletTypeDTO
                {
                    WalletTypeID = wallet.WalletType.WalletTypeID,
                    Name = wallet.WalletType.Name
                };
            }

            else
            {
                throw new ArgumentException($"Wallet {id} not found");
            }
            return walletDto;

        }

        public void Insert(WalletCreateDTO entity)
        {
            try
            {
                var newWallet = new Wallet
                {
                    UserID = entity.UserID,
                    Balance = entity.Balance,
                    WalletName = entity.WalletName
                };
                _walletDAL.Insert(newWallet);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }

        public void Update(WalletUpdateDTO entity)
        {

            try
            {
                var newWallet = new Wallet
                {
                    UserID = entity.UserID,
                    Balance = entity.Balance,
                    WalletName = entity.WalletName,
                    WalletID = entity.WalletID,
                };
                _walletDAL.Update(newWallet);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }

        public void Delete(int walletID)
        {
            try
            {
                _walletDAL.Delete(walletID);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }

        public IEnumerable<WalletDTO> GetWalletDataByUser(int UserID)
        {
            List<WalletDTO> walletsbyuserDTO = new List<WalletDTO>();
            var wallets = _walletDAL.GetWalletDataByUser(UserID);
            foreach (var wallet in wallets)
            {
                walletsbyuserDTO.Add(new WalletDTO
                {
                    WalletID = wallet.WalletID,
                    WalletTypeID = wallet.WalletTypeID,
                    Balance = wallet.Balance,
                    UserID = wallet.UserID,
                    WalletName = wallet.WalletType.Name,
                    Name = wallet.WalletType.Name,

                    WalletType = new WalletTypeDTO
                    {
                        WalletTypeID = wallet.WalletType.WalletTypeID,
                        Name = wallet.WalletType.Name
                    }
                });
            }
            return walletsbyuserDTO;
        }

        public decimal GetTotalBalance(int id)
        {
            try
            {
                return _walletDAL.GetTotalBalance(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
