using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/token")]
public class TokenApiController : ControllerBase
{
	private readonly IUnitOfWork _uow;
	
	public TokenApiController(IUnitOfWork uow)
	{
		_uow = uow;
	}
	
	[HttpGet("{id}")]
	public IActionResult Test(int id)
	{
		var res = _uow.Tokens.GetById(id);
		return Ok(res);
	}
}