using BudgetingAPIService.BLL.DTOs;
using BudgetingAPIService.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetingAPIService.Controllers
{

    //[Authorize(Roles = "userplus,user")]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionBLL _transactionBLL;

        public TransactionsController(ITransactionBLL transactionBLL)
        {
            _transactionBLL = transactionBLL;
        }


        // GET: api/<TransactionsController>
        [HttpGet]
        public IEnumerable<TransactionDTO> Get()
        {
            var result = _transactionBLL.GetAll();
            return result;
        }

        // GET api/<TransactionsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _transactionBLL.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET api/<TransactionsController>/user/5
        [HttpGet("user/{id}")]
        public IActionResult GetUserTransactions(int id)
        {
            var result = _transactionBLL.GetUserTransaction(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET api/<TransactionsController>/user/5/v2
        [HttpGet("user/{id}/v2")]
        public IActionResult GetUserTransactionsV2(int id, [FromQuery] int? year, [FromQuery] int? month, [FromQuery] int? transactionCategoryID, [FromQuery] int? transactionTypeID)
        {
            try
            {
                var result = _transactionBLL.GetUserTransactionV2(id, year, month, transactionCategoryID, transactionTypeID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<TransactionsController>/user/5/expense
        [HttpGet("user/{id}/expense")]
        public IActionResult GetUserExpense(int id)
        {
            try
            {
                var result = _transactionBLL.GetUserExpense(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<TransactionsController>/user/5/expense/2024/3/1
        [HttpGet("user/{id}/expense/{year}/{month}/{transactionCategoryID}")]
        public IActionResult GetUserExpenseByMonth(int id, int year, int month, int transactionCategoryID)
        {
            try
            {
                var result = _transactionBLL.GetUserExpenseByMonth(id, year, month, transactionCategoryID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<TransactionsController>
        [HttpPost]
        public IActionResult Post([FromBody] TransactionCreateDTO transactionCreateDTO)
        {
            try
            {
                _transactionBLL.Insert(transactionCreateDTO);
                return Ok("Transaction created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<TransactionsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TransactionUpdateDTO transactionDTO)
        {
            transactionDTO.TransactionID = id;
            try
            {
                _transactionBLL.Update(transactionDTO);
                return Ok("Transaction updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<TransactionsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _transactionBLL.Delete(id);
                return Ok("Transaction deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
