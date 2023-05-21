using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
		var res = JsonConvert.SerializeObject(_uow.Tokens.GetById(id));
		return Ok(res);
	}
}