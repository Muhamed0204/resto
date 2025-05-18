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

        // GET: Tables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var table = await _context.Tables
                .Include(t => t.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (table == null) return NotFound();

            return View(table);
        }


        // GET: Tables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var table = await _context.Tables.FindAsync(id);
            if (table == null) return NotFound();

            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Название", table.RestaurantId);
            return View(table);
        }

        // POST: Tables/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Table table)
        {
            if (id != table.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(table);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tables.Any(e => e.Id == table.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Название", table.RestaurantId);
            return View(table);
        }




        // GET: Tables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var table = await _context.Tables
                .Include(t => t.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (table == null) return NotFound();

            return View(table);
        }

        // POST: Tables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table != null)
            {
                _context.Tables.Remove(table);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


    }


}
