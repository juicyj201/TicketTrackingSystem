using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicketTrackingSystem.Models;
using TicketTrackingSystem.Repository;

namespace TicketTrackingSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RepositoryImpl repo = new();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var items = repo.GetTickets();
        return View(items);
    }

    public IActionResult Privacy()
    {
        return View();
    } 

    #region CRUD operations
    public IActionResult Create(){
        return View();
    }

    public IActionResult CreateTicket(TicketsViewModel model)
    {
        string msg = repo.CreateTicket(model);
        ViewBag.Message = msg;
        return View();
    }

    public IActionResult Edit(){
        return View();
    }

    public IActionResult EditTicket(TicketsViewModel model)
    {
        string msg = repo.CreateTicket(model);
        ViewBag.Message = msg;
        return View();
    }
    #endregion

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
