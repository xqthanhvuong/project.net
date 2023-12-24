using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using webhotel.Models;

var builder = WebApplication.CreateBuilder(args);

// add services to the container.
builder.Services.AddControllersWithViews(); ;
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o =>
{
	o.LoginPath = "/login/access";
	o.ExpireTimeSpan = TimeSpan.FromDays(1);
}
);
builder.Services.AddSession();

builder.Services.AddDbContext<WebhotelContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("myHotel")));

var app = builder.Build();


// configure the http request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/home/error");
	// the default hsts value is 30 days. you may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=home}/{action=index}/{id?}");
app.MapControllerRoute(
	name: "login",
	pattern: "{controller=login}/{action=access}/{id?}");

app.Run();
