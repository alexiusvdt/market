using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using Market.Models;

namespace Market.Controllers
{
    public class HomeController : Controller
    {
        private readonly MarketContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager, MarketContext db, IConfiguration configuration)
        {
        _userManager = userManager;
        _db = db;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
        return View(Product.GetProducts());
        }

        [HttpGet("/privacy")]
        public async Task<ActionResult> Privacy()
        {
        return View();
        }
    }
}