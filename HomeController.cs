using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Dish.Models;
using Microsoft.EntityFrameworkCore;



namespace Dish.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyContext _context;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
             List<Dish.Models.Chef> allChefs = _context.Chefs.Include(chef => chef.AllDishes).ToList();
            return View("Index", allChefs);
        }

        [HttpGet("dishes/new")]
        public IActionResult NewDish()
        {
            
          List<Dish.Models.Chef> allChefs = _context.Chefs.ToList();
          ViewBag.allChefs = allChefs;
            return View("~/Views/Home/New.cshtml" );
        }

         [HttpGet("dishes")]
        public IActionResult AllDishes()
        {
            List<Dish.Models.Dish> allDishes = _context.Dishes.Include(dish => dish.Creator).ToList();
            return View("Dishes",allDishes);
        }

        [HttpPost("")]
        public IActionResult CreateDish(Dish.Models.Dish newDish)
        {
            if (!ModelState.IsValid)
            {
                return View("New", newDish);
            }
     

            _context.Dishes.Add(newDish);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }



        [HttpGet("dishes/{id}")]
        public IActionResult ViewDish(int id)
        {
            Dish.Models.Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == id);

            if(dish == null)
            {
                return RedirectToAction("Index");
            }

            return View("Details", dish);
        }



        [HttpPost("dishes/{dishId}/delete")]
        public IActionResult DeleteDish(int dishId)
        {
            Dish.Models.Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
            if(dish != null)
            {
                _context.Dishes.Remove(dish);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }



        [HttpGet("dishes/{dishId}/edit")]
        public IActionResult EditDish(int dishId)
        {
            Dish.Models.Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
            if(dish == null)
            {
                return RedirectToAction("Index");
            }
            return View("Edit", dish);
        }



        [HttpPost("dishes/{dishId}/edit")]
        public IActionResult UpdateDish(int dishId, Dish.Models.Dish updatedDish)
        {
            if(!ModelState.IsValid)
            {
                // Dish.Models.Dish? originalDish = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
                // return View("Edit", originalDish);
                return EditDish(dishId);
            }
            Dish.Models.Dish? dbDish = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
            if(dbDish == null)
            {
                return RedirectToAction("Index");
            }

            dbDish.Name = updatedDish.Name;
       
          
            dbDish.Calories = updatedDish.Calories;
            dbDish.Tastiness = updatedDish.Tastiness;
            dbDish.UpdatedAt = DateTime.Now;


            _context.Dishes.Update(dbDish);

            _context.SaveChanges();

            return RedirectToAction("ViewDish", new {id = dbDish.DishId});

            
        }

        [HttpPost("chefs/new")]
        public IActionResult CreateChef(Chef newChef)
        {
            if (!ModelState.IsValid)
            {
                return View("NewChef");
            }

            _context.Chefs.Add(newChef);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

[HttpGet("chefs/new")]
        public IActionResult NewChef()
        {
            return View("NewChef");
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
}
