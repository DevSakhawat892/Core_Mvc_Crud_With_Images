using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement
{
   public class CategoriesController : Controller
   {
      private readonly shopContext _context;

      public CategoriesController(shopContext context)
      {
         _context = context;
      }

      // GET: Categories
      public async Task<IActionResult> Index()
      {
         //var shopContext = _context.Categories.na
         //return View(await shopContext.ToListAsync());
         return View(await _context.Categories.ToListAsync());
      }
      public async Task<IActionResult> Details(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var category = await _context.Categories
             .FirstOrDefaultAsync(m => m.Id == id);
         if (category == null)
         {
            return NotFound();
         }

         //return View(category);
         return PartialView("_CategoryDetails", category);
      }

      public IActionResult Create()
      {
         return View();
      }


      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
      {
         if (ModelState.IsValid)
         {
            _context.Add(category);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Index));
         }
         //return View(category);
         return RedirectToAction(nameof(Index));
      }

      // GET: Categories/Edit/5
      public async Task<IActionResult> Edit(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var category = await _context.Categories.FindAsync(id);
         if (category == null)
         {
            return NotFound();
         }
         return View(category);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
      {
         if (id != category.Id)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            try
            {
               _context.Update(category);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!CategoryExists(category.Id))
               {
                  return NotFound();
               }
               else
               {
                  throw;
               }
            }
            return RedirectToAction(nameof(Index));
         }
         return View(category);
      }

      // GET: Categories/Delete/5
      public async Task<IActionResult> Delete(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var category = await _context.Categories
             .FirstOrDefaultAsync(m => m.Id == id);
         if (category == null)
         {
            return NotFound();
         }

         return View(category);
      }

      // POST: Categories/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         var category = await _context.Categories.FindAsync(id);
         _context.Categories.Remove(category);
         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool CategoryExists(int id)
      {
         return _context.Categories.Any(e => e.Id == id);
      }
   }
}
