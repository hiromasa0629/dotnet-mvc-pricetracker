public class UnitOfWork : IUnitOfWork
{
	private readonly DefaultDbContext _context;
	
	public UnitOfWork(DefaultDbContext context)
	{
		_context = context;
		Tokens = new TokenRepository(_context);
	}
	
	public ITokenRepository Tokens { get; private set; }
	
	public int Complete()
	{
		return _context.SaveChanges();
	}
	
	public void Dispose()
	{
		_context.Dispose();
	}
}