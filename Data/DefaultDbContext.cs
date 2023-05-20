using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

public class DefaultDbContext : DbContext
{
	private readonly IConfiguration _configuration;
	public DefaultDbContext(DbContextOptions<DefaultDbContext> options, IConfiguration configuration) : base(options)
	{
		_configuration = configuration;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			var mysqlConnectionBuilder = new MySqlConnectionStringBuilder();
			mysqlConnectionBuilder.Server = "localhost";
			mysqlConnectionBuilder.Port = 3306;
			mysqlConnectionBuilder.Pooling = true;
			mysqlConnectionBuilder.UserID = this._configuration["DbUser"];
			mysqlConnectionBuilder.Password = this._configuration["DbPassword"];
			mysqlConnectionBuilder.Database = this._configuration["DbName"];
			optionsBuilder.UseMySQL(mysqlConnectionBuilder.ToString());
			base.OnConfiguring(optionsBuilder);
		}
	}
}