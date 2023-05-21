using assessment.Models;

public class TokenRepository : GenericRepository<Token>, ITokenRepository
{
	public TokenRepository(DefaultDbContext context) : base(context)
	{
	}
}