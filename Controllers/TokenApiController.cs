using Microsoft.AspNetCore.Mvc;
using assessment.Models;
using AppHttpExceptionHandling.Exceptions;
using System.Net;

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
	public IActionResult GetTokenDetail(int id)
	{
		var res = _uow.Tokens.GetById(id);
		return Ok(res);
	}
	
	[HttpGet]
	public IActionResult GetTokens()
	{
		var res = _uow.Tokens.GetAll();
		return Ok(res);
	}
	
	[HttpPost]
	public IActionResult CreateOrUpdateToken([FromForm] Token body)
	{
		Token? token = this._uow.Tokens.Find(t => t.symbol == body.symbol).FirstOrDefault();
		
		if (token == null) // Create new record if symbol does not exist
		{
			_uow.Tokens.Add(body);
		}
		else
		{
			token.name = body.name;
			token.contract_address = body.contract_address;
			token.total_holders = body.total_holders;
			token.total_supply = body.total_supply;
		}
		
		_uow.Complete();
		
		return Ok();
	}
}