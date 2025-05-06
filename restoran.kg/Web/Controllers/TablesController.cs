using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure.Data; 
using System.Threading.Tasks;
using System.Linq;

namespace YourNamespace.Controllers
{
    public class TablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tables
        public async Task<IActionResult> Index()
        {
            var tables = _context.Tables.Include(t => t.Restaurant);
            return View(await tables.ToListAsync());
        }

        // GET: Tables/Create
        public IActionResult Create()
        {
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Название");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Table table)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(table);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
                    ModelState.AddModelError("", "Произошла ошибка при сохранении данных.");
                }
            }

            // Повторная подгрузка списка ресторанов
            var restaurants = _context.Restaurants.ToList();
            ViewData["RestaurantId"] = new SelectList(restaurants, "Id", "Название");

            return View(table);
        }



    }


}
