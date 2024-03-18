using BudgetingApp.BLL;
using BudgetingApp.BLL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddScoped<IBudgetBLL, BudgetBLL>();
builder.Services.AddScoped<ITransactionBLL, TransactionBLL>();
builder.Services.AddScoped<IWalletBLL, WalletBLL>();
builder.Services.AddScoped<ITransactionCategoryBLL, TransactionCategoryBLL>();
builder.Services.AddScoped<IUserBLL, UserBLL>();
builder.Services.AddScoped<IBudgetBLL, BudgetBLL>();


builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
