using Microsoft.EntityFrameworkCore;

public class DefaultDbContext : DbContext
{
	public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
	{
		
	}
}