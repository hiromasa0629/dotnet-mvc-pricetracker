using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Initialize ConnectionString to postgres
var connectionStringBuilder = new NpgsqlConnectionStringBuilder();
connectionStringBuilder.Host = "localhost";
connectionStringBuilder.Port = 5432;
connectionStringBuilder.Pooling = true;
connectionStringBuilder.Database = builder.Configuration["DbName"];
connectionStringBuilder.Username = builder.Configuration["DbUser"];
connectionStringBuilder.Password = builder.Configuration["DbPassword"];

// Establish connection to postgres
builder.Services.AddDbContext<DefaultDbContext>(options => 
	options.UseNpgsql(connectionStringBuilder.ConnectionString)
);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
