using BudgetingApp.BLL.DTOs;
using BudgetingApp.BLL.Interfaces;
using BudgetingApp.BO;
using BudgetingApp.DAL;
using BudgetingApp.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace BudgetingApp.BLL
{
    public class BudgetBLL : IBudgetBLL
    {
        private readonly IBudgetDAL _budgetDAL;

        public BudgetBLL()
        {
            _budgetDAL = new BudgetDAL();
        }

        public void DeleteBudget(int budgetID)
        {
            try
            {
                _budgetDAL.Delete(budgetID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting budget: {ex.Message}");
            }
        }

        public BudgetDTO GetBudgetByID(int budgetID)
        {
            try
            {
                var budget = _budgetDAL.GetById(budgetID);
                if (budget == null)
                {
                    throw new ArgumentException($"Budget with ID {budgetID} not found");
                }

                return new BudgetDTO
                {
                    BudgetID = budget.BudgetID,
                    MonthDate = budget.MonthDate,
                    Amount = budget.Amount,
                    UserID = budget.UserID,
                    TransactionCategoryID = budget.TransactionCategoryID,
                    TransactionCategoryName = budget.TransactionCategory?.Name
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting budget by ID: {ex.Message}");
            }
        }

        public IEnumerable<BudgetDTO> GetUserBudgets(int userID)
        {
            try
            {
                var budgets = _budgetDAL.GetUserBudgets(userID);
                var budgetDTOs = new List<BudgetDTO>();
                foreach (var budget in budgets)
                {
                    budgetDTOs.Add(new BudgetDTO
                    {
                        BudgetID = budget.BudgetID,
                        MonthDate = budget.MonthDate,
                        Amount = budget.Amount,
                        UserID = budget.UserID,
                        TransactionCategoryID = budget.TransactionCategoryID,
                        TransactionCategoryName = budget.TransactionCategory?.Name
                    });
                }
                return budgetDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting user budgets: {ex.Message}");
            }
        }

        public void InsertBudget(BudgetDTO entity)
        {
            try
            {
                var budget = new Budget
                {
                    MonthDate = entity.MonthDate,
                    Amount = entity.Amount,
                    UserID = entity.UserID,
                    TransactionCategoryID = entity.TransactionCategoryID
                };
                _budgetDAL.Insert(budget);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting budget: {ex.Message}");
            }
        }

        public void UpdateBudget(BudgetDTO entity)
        {
            try
            {
                var budget = new Budget
                {
                    BudgetID = entity.BudgetID,
                    MonthDate = entity.MonthDate,
                    Amount = entity.Amount,
                    TransactionCategoryID = entity.TransactionCategoryID
                };
                _budgetDAL.Update(budget);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating budget: {ex.Message}");
            }
        }
    }
}
