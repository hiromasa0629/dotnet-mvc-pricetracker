using assessment.Models;

public class TokenRepository : GenericRepository<Token>, ITokenRepository
{
	public TokenRepository(DefaultDbContext context) : base(context)
	{
	}
	
	public IEnumerable<Token> GetAllSortByTotalSupplyDesc()
	{
		return this._context.Set<Token>().ToList().OrderByDescending(val => val.total_supply);
	}
}