namespace BudgetingApp.MVC.Controllers
{
	public static class Helper
	{
		public static string FormatToIDR(decimal amount)
		{
			return string.Format(System.Globalization.CultureInfo.GetCultureInfo("id-ID"), "{0:C}", amount);
		}

		public static string FormatDate(DateTime date)
		{
			return date.ToString("dd/MM/yyyy"); // Adjust the format as per your requirement
		}
	}
}
