using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using assessment.Models;

namespace assessment.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
	private readonly IUnitOfWork _uow;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork uow)
    {
        _logger = logger;
		_uow = uow;
    }

    public IActionResult Index()
    {
		List<TokenViewModel> newTokens = new List<TokenViewModel>();
		
		var tokens = _uow.Tokens.GetAllSortByTotalSupplyDesc();
		
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
		
		ViewBag.Tokens = newTokens;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
