using Microsoft.EntityFrameworkCore;
using AppHttpExceptionHandling.Middleware;
// using Npgsql;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DefaultDbContext>();
// Pass user secrets to DefaultDbContext
builder.Configuration.AddUserSecrets<DefaultDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<AppHttpExceptionMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<AppHttpExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "detail",
	pattern: "detail.aspx",
	defaults: new { controller = "Home", action = "Index" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
