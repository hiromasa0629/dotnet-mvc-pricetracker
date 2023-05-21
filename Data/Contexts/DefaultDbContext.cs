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
			mysqlConnectionBuilder.Server = "localhost";
			mysqlConnectionBuilder.Port = 3306;
			mysqlConnectionBuilder.Pooling = true;
			mysqlConnectionBuilder.UserID = this._configuration["DbUser"];
			mysqlConnectionBuilder.Password = this._configuration["DbPassword"];
			mysqlConnectionBuilder.Database = this._configuration["DbName"];
			
			optionsBuilder.UseMySql(mysqlConnectionBuilder.ToString(), MySqlServerVersion.LatestSupportedServerVersion);
			base.OnConfiguring(optionsBuilder);
		}
	}

	public required DbSet<Token> Tokens { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Token>();
		base.OnModelCreating(modelBuilder);
	}
}