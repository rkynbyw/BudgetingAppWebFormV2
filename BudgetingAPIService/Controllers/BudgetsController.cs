using BudgetingAPIService.BLL.DTOs;
using BudgetingAPIService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetingAPIService.Controllers
{
    [Authorize(Roles = "userplus")]
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : Controller
    {
        private readonly IBudgetBLL _budgetBLL;

        public BudgetsController(IBudgetBLL budgetBLL)
        {
            _budgetBLL = budgetBLL;
        }


        // GET: api/Budgets
        [HttpGet]
        public IEnumerable<BudgetDTO> Get()
        {
            var budgets = _budgetBLL.GetUserBudgets(15);
            return budgets;
        }

        // GET: api/Budgets/user
        [HttpGet("user/{id}")]
        public IEnumerable<BudgetDTO> GetUserBudgets()
        {
            var budgets = _budgetBLL.GetUserBudgets(15);
            return budgets;
        }

        // GET: api/Budgets/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var budget = _budgetBLL.GetBudgetByID(id);
                return Ok(budget);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving budget: {ex.Message}");
            }
        }

        // POST: api/Budgets
        [HttpPost]
        public IActionResult Post([FromBody] BudgetCreateDTO budgetCreateDTO)
        {
            try
            {
                _budgetBLL.InsertBudget(budgetCreateDTO);
                return Ok("Budget created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating budget: {ex.Message}");
            }
        }

        // PUT: api/Budgets/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BudgetUpdateDTO budgetUpdateDTO)
        {
            try
            {
                budgetUpdateDTO.BudgetID = id;
                _budgetBLL.UpdateBudget(budgetUpdateDTO);
                return Ok("Budget updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating budget: {ex.Message}");
            }
        }

        // DELETE: api/Budgets/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _budgetBLL.DeleteBudget(id);
                return Ok("Budget deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting budget: {ex.Message}");
            }
        }
    }
}
