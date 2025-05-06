using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure.Data;

namespace Web.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Restaurants.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == id);
            if (restaurant == null) return NotFound();

            return View(restaurant);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(restaurant);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Вывод ошибки в консоль (для отладки)
                    Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
                    ModelState.AddModelError("", "Произошла ошибка при сохранении данных.");
                }
            }

            // В случае ошибки возвращаем форму с ошибками
            return View(restaurant);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return NotFound();

            return View(restaurant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Restaurant restaurant)
        {
            if (id != restaurant.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == id);
            if (restaurant == null) return NotFound();

            return View(restaurant);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
