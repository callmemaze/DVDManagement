using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DVDManagement.Controllers;

public class HomePage : Controller
{
    // GET
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }
}