using assessment.Models;

public interface ITokenRepository : IGenericRepository<Token>
{
	IEnumerable<Token> GetAllSortByTotalSupplyDesc();
}