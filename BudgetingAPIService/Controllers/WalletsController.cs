using BudgetingAPIService.BLL.DTOs;
using BudgetingAPIService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetingAPIService.Controllers
{
    [Authorize(Roles = "userplus,user")]
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletBLL _walletBLL;

        public WalletsController(IWalletBLL walletBLL)
        {
            _walletBLL = walletBLL;
        }


        [HttpGet]
        public IEnumerable<WalletDTO> Get()
        {
            return _walletBLL.GetAll();
        }

        [HttpGet("user/{id}")]
        public IEnumerable<WalletDTO> GetUserWallets(int id)
        {
            var userId = id;
            var results = _walletBLL.GetWalletDataByUser(userId);
            return results;
        }

        [HttpPost]
        public IActionResult Post([FromBody] WalletCreateDTO walletCreateDTO)
        {
            try
            {
                _walletBLL.Insert(walletCreateDTO);
                return Ok("Transaction created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] WalletUpdateDTO walletUpdateDTO)
        {
            walletUpdateDTO.WalletID = id;
            try
            {
                _walletBLL.Update(walletUpdateDTO);
                return Ok("Wallet updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _walletBLL.Delete(id);
                return Ok("Wallet deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
