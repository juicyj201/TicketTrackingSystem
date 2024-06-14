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

    public IActionResult Create(){
        return View();
    }

    public IActionResult CreateTicket(TicketsViewModel model)
    {
        repo.CreateTicket(model);
        ViewBag.Message = "Data Insert Successfully";
        Console.WriteLine("Succeeded");
        return View();
    }

    public IActionResult Edit(){
        return View();
    }

    public IActionResult EditTicket(TicketsViewModel model)
    {
        repo.CreateTicket(model);
        ViewBag.Message = "Data Insert Successfully";
        Console.WriteLine("Succeeded");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
