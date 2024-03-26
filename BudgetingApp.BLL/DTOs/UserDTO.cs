namespace BudgetingApp.BLL.DTOs

{
	public class UserDTO
	{
		public UserDTO()
		{
			//this.Budgets = new List<Budget>();
			//this.Transactions = new List<Transactions>();
			//this.Wallet = new List<Wallet>();
		}

		public int UserID { get; set; }
		public string Email { get; set; }
		public string Username { get; set; }
		public string FullName { get; set; }
		//public string Password { get; set; }

		public string Role { get; set; }

		//public List<Budget> Budgets { get; set; }
		//public List<Transactions> Transactions { get; set; }
		//public List<Wallet> Wallet { get; set; }
	}
}
