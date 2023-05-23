using Microsoft.AspNetCore.Mvc;
using assessment.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

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
		var tokens = _uow.Tokens.GetAllSortByTotalSupplyDesc();
		
		List<TokenViewModel> newTokens = new List<TokenViewModel>();
		
		UInt64 sum_total_supply = 0;
		foreach (var t in tokens)
		{
			sum_total_supply += t.total_supply;
		}
		
		foreach (var t in tokens)
		{
			TokenViewModel tmp = new TokenViewModel
			{
				id = t.id,
				name = t.name,
				symbol = t.symbol,
				total_supply = t.total_supply,
				contract_address = t.contract_address,
				total_holders = t.total_holders,
				total_supply_perc = ((double)t.total_supply / (double)sum_total_supply) * 100
			};
			
			newTokens.Add(tmp);
		}
		
		return Ok(newTokens);
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
	
	[HttpGet("export")]
	public IActionResult ExportCSV()
	{
		List<Token> tokens = _uow.Tokens.GetAll().ToList();
		
		var stream = new MemoryStream();
		using (var writer = new StreamWriter(stream, leaveOpen: true))
		{
			var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
			csv.WriteRecords(tokens);
			writer.Flush();
		}
		stream.Position = 0;
		return File(stream, "application/octet-stream", "Tokens.csv");
	}
}