using System.Diagnostics;
using System.Text.Unicode;
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
        var items = repo.GetTickets().OrderBy(ticket => ticket.Ticket_ID).ToList();
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(TicketsViewModel model)
    {
        if(!ModelState.IsValid)
        {
            ViewBag.Message = "There is an error with the data entered.";
            return View();
        }

        string msg = repo.CreateTicket(model);
        ViewBag.Message = msg;
        return View();
    }

    [HttpGet]
    public ActionResult Edit(int ID){
        var ticket = repo.GetTicketFromID(ID);
        return View(ticket);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(TicketsViewModel model)
    {
        string msg = repo.EditTicket(model);
        ViewBag.Message = msg;
        return View(model);
    }

    [HttpPost]
    public ActionResult Delete(int ID){
        string msg = repo.DeleteTicket(ID);
        ViewBag.Message = msg;
        return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult Resolve(int ID){
        string msg = repo.ResolveTicket(ID);
        ViewBag.Message = msg;
        return RedirectToAction("Index");
    }

    public IActionResult Login(){
        return View();
    }

    [HttpPost]
    public ActionResult Login(User user){
        if(ModelState.IsValid){
            var checkuser = repo.Login(user);
            ViewBag.Message = $"'{checkuser.Name}' have been logged in successfully";
            if(checkuser.EMP_ID != 0 || checkuser.Name != null || checkuser.Password != null || checkuser.Emp_Type != null){
                HttpContext.Session.SetString("UserName", checkuser.Name);
                HttpContext.Session.SetString("UserType", checkuser.Emp_Type);
            }

            if(checkuser.Emp_Type == null) {
                ViewBag.Message = "Login has failed";
                return View();
            }

            return RedirectToAction("Index");
        }

        return View();
    }
    #endregion

    #region Errors
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    #endregion
}
