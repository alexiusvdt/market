using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Market.Models;
using Market.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
  [Authorize]
  public class ProductsController : Controller
  {
    private readonly MarketContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ProductsController(UserManager<ApplicationUser> userManager, MarketContext db, IConfiguration configuration)
    {
      _userManager = userManager;
      _db = db;
    }
    
    public IActionResult Index()
    {
        return View(Product.GetProducts());
    }

    public IActionResult Details(int id)
    {
      Product product = Product.GetDetails(id);

      if ((_db.Products.FirstOrDefault(entry => product.Id == entry.Id)) == null)
      {
        _db.Products.Add(product);
        _db.SaveChanges();
      }

      // any viewbag changes go here

      return View(movie);
    }

    [HttpPost]
    public async Task<ActionResult> AddToUser (int inputId)
    { 
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);

      User thisUser = _db.Users.FirstOrDefault(entry => entry.UserAccount.Id == currentUser.Id);

      #nullable enable
      UserMovie? joinEntity = _db.UserMovies.FirstOrDefault(entry => (entry.MovieId == inputId) && (entry.UserId == thisUser.UserId));
      #nullable disable

      if (joinEntity == null && thisUser.UserId != 0)
      {
        _db.UserMovies.Add(new UserMovie() { MovieId = inputId, UserId = thisUser.UserId});
        _db.SaveChanges();
      }

      return RedirectToAction("Details", new { id = inputId });
    }

    public IActionResult Search(string query)
    {
      if (query != null)
      {
        return View(Movie.GetBasicSearch(query, _apikey));
      }
      else
      {
        //add error message
        return RedirectToAction("Index");
      }
    } 
    
    [HttpGet, ActionName("AdvSearch")]
    public IActionResult AdvSearch(string param, string query)
    {
      return View(Movie.GetAdvSearch(param, query, _apikey));
    } 

    //new
    public async Task<ActionResult> MyMovies()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      User thisUser = _db.Users.Include(join => join.JoinEntities).ThenInclude(join => join.Movie).FirstOrDefault(entry => entry.UserAccount.Id == currentUser.Id);
  
      ViewBag.UserReviews = _db.Reviews.Include(review => review.Movie).Where(entry => entry.UserId == thisUser.UserId).ToList();
      //Movie movie = Movie.GetDetails(id, _apikey);
      // .Include(join => join.JoinEntities).ThenIncude(join => join.)


      // List<Item> model = _db.Items
      //                       .Include(item => item.Category)
      //                       .ToList();
      return View(thisUser);
    }

    public ActionResult CreateReview (int inputId)
    {
      
      Movie movie = _db.Movies.FirstOrDefault(movie => movie.Id == inputId);
      ViewBag.MovieId = movie.Id;
      ViewBag.MovieTitle = movie.Title;

      return View();
      
    }

    [HttpPost]
    public async Task<ActionResult> CreateReview (Review review, int MovieId)
    {
      if (!ModelState.IsValid)
      {
        Movie movie = _db.Movies.FirstOrDefault(movie => movie.Id == MovieId);
        ViewBag.MovieId = movie.Id;
        ViewBag.MovieTitle = movie.Title;
        return View(review);
      }
      else
      {
      Movie movie = _db.Movies.FirstOrDefault(movie => movie.Id == review.MovieId);

      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      User thisUser = _db.Users.FirstOrDefault(entry => entry.UserAccount.Id == currentUser.Id);
      review.UserId = thisUser.UserId;

      _db.Reviews.Add(review);

      movie.NumberOfRatings = movie.NumberOfRatings + 1;
      movie.Rating = (movie.Rating + review.Rating) / movie.NumberOfRatings;

      _db.Movies.Update(movie);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = movie.Id });
      }
    }

    [HttpPost]
    public ActionResult RemoveMovieFromUser (int joinId)
    { 
      UserMovie joinEntry = _db.UserMovies.FirstOrDefault(entry => entry.UserMovieId == joinId);
      _db.UserMovies.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("MyMovies");
    }

    [HttpPost]
    public ActionResult RemoveReviewFromUser (int reviewId)
    { 
      Review review = _db.Reviews.FirstOrDefault(entry => entry.ReviewId == reviewId);
      _db.Reviews.Remove(review);
      _db.SaveChanges();
      return RedirectToAction("MyMovies");
    }
  }
}

    