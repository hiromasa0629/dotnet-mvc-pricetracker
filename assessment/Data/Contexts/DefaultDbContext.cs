using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using assessment.Models;

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
			mysqlConnectionBuilder.Server = Environment.GetEnvironmentVariable("MYSQL_SERVER") ?? "localhost" ;
			mysqlConnectionBuilder.Port = 3306;
			mysqlConnectionBuilder.Pooling = true;
			mysqlConnectionBuilder.UserID = Environment.GetEnvironmentVariable("MYSQL_USER");
			mysqlConnectionBuilder.Password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
			mysqlConnectionBuilder.Database = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
			
			Console.WriteLine(mysqlConnectionBuilder.ToString());
			
			optionsBuilder.UseMySql(mysqlConnectionBuilder.ToString(), MySqlServerVersion.LatestSupportedServerVersion);
			base.OnConfiguring(optionsBuilder);
		}
	}

	public required DbSet<Token> Token { get; set; }
}