﻿using System.Diagnostics;
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
		ViewBag.Tokens = _uow.Tokens.GetAll();
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
