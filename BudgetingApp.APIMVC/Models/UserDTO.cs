﻿namespace BudgetingApp.APIMVC.Models
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        //public string Password { get; set; }

        public string Role { get; set; }
    }
}
