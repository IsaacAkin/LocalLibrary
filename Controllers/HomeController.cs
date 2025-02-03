using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LocalLibrary.Models;

namespace LocalLibrary.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AddNewBook()
    {
        return View();
    }

    public IActionResult NewBookConfirmation(string name, string author, int copies)
    {
        Database.Instance.SetConnectionString(_configuration.GetValue<string>("ConnectionString"));
        Database.Instance.AddBook(name, author, copies);
        return View(Database.Instance.AddBookConfirmation(name, author));
    }

    public IActionResult DeleteBook()
    {
        return View();
    }
    public IActionResult DeleteBookConfirmation(string name, int isbn)
    {
        Database.Instance.SetConnectionString(_configuration.GetValue<string>("ConnectionString"));
        Database.Instance.DeleteBook(name, isbn);
        return View(Database.Instance.DeleteBookConfirmation(name, isbn));
    }

    public IActionResult AllBooks()
    {
        Database.Instance.SetConnectionString(_configuration.GetValue<string>("ConnectionString"));
        return View(Database.Instance.GetAllBooks());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
