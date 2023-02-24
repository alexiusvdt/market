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
        private readonly string _apikey;   
        private readonly MarketContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager, MarketContext db, IConfiguration configuration)
        {
        _apikey = configuration["TMDB"];
        _userManager = userManager;
        _db = db;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
        return View(Movie.GetMovies(_apikey));
        }

        // public IActionResult Index()
        // {
        //     return View(Movie.GetMovies(_apikey));
        // }

        [HttpGet("/privacy")]
        public async Task<ActionResult> Privacy()
        {
        return View();
        }
    }
}