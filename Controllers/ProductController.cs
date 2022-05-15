using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement
{
   public class ProductController : Controller
   {
      private IHostingEnvironment hosting;
      private readonly shopContext _context;

      public ProductController(shopContext context)
      {
         _context = context;
      }

      public async Task<IActionResult> Index()
      {
         return View(await _context.Products.Include(p => p.Category).ToListAsync());

         //var shopContext = _context.Products.Include(p => p.Category);
         //return View(await shopContext.ToListAsync());
      }

      public async Task<IActionResult> Details(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var product = await _context.Products
             .Include(p => p.Category)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (product == null)
         {
            return NotFound();
         }

         return View(product);
      }

      public IActionResult Create()
      {
         //ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Name");
         ViewData["CategoryID"] = _context.Categories.Select(m => new SelectListItem
         {
            Text = m.Name,
            Value = m.Id.ToString()
         });
         return View();
      }


      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create(Product product)
      {
         ViewData["CategoryID"] = _context.Categories.Select(m => new SelectListItem
         {
            Text = m.Name,
            Value = m.Id.ToString()
         });

         if (product.Photo != null)
         {
            string fPath = Path.Combine(hosting.WebRootPath, "Images\\Products");
            if (!Directory.Exists(fPath))
            {
               Directory.CreateDirectory(fPath);
            }
            string fExt = Path.GetExtension(product.Photo.FileName).ToLower();
            if (fExt == ".jpg" || fExt == ".jpeg" || fExt == ".png" || fExt == ".gif")
            {
               string fNameWithoutSpace = product.Name.Replace(" ", "_");
               string fName = fNameWithoutSpace + DateTime.Now.ToString("ddmmyyyy" +
               "") + fExt;
               string fileToSave = Path.Combine(fPath, fName);
               using (var fileStream = new FileStream(fileToSave, FileMode.Create))
               {
                  product.Photo.CopyTo(fileStream);
                  product.PhotoPath = "~/Images/Products/" + fName;
               }
            }
         }

         if (product.Photo != null)
         {
            string fPath = Path.Combine(hosting.WebRootPath, "Images\\Products");
            if (!Directory.Exists(fPath))
            {
               Directory.CreateDirectory(fPath);
            }
            string fExt = Path.GetExtension(product.Photo.FileName).ToLower();
            if (fExt == ".jpg" || fExt == ".jpeg" || fExt == ".png" || fExt == ".gif")
            {
               string fNameWithoutSpace = product.Name.Replace(" ", "_");
               string fName = fNameWithoutSpace + DateTime.Now.ToString("ddmmyyyy" +
               "") + fExt;
               string fileToSave = Path.Combine(fPath, fName);
               using (var fileStream = new FileStream(fileToSave, FileMode.Create))
               {
                  product.Photo.CopyTo(fileStream);
                  product.PhotoPath = "~/Images/Products/" + fName;
               }
            }
         }

         if (ModelState.IsValid)
         {
            //if (product.Photo != null)
            //{
            //   string ext = Path.GetExtension(product.Photo.FileName).ToLower();
            //   if (ext == ".jpg" || ext == ".png")
            //   {
            //      string fileName = Path.Combine(hosting.WebRootPath, "Images\\product", product.Photo.FileName);
            //      using (var fileStream = new FileStream(fileName, FileMode.Create))
            //      {
            //         product.Photo.CopyTo(fileStream);
            //         product.PhotoPath = "/Images/product/" + product.Photo.FileName;
            //      }
            //   }
            //}

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         return View(product);
      }


      public async Task<IActionResult> Edit(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }
         ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Name");
         var product = await _context.Products.FindAsync(id);
         if (product == null)
         {
            return NotFound();
         }
         //ViewBag.Category = _context.Categories.Select(m => new SelectListItem
         //{
         //   Text = m.Name,
         //   Value = m.Id.ToString()
         //});
         return View(product);
      }


      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, Product product)
      {
         ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryID);
         if (product.Photo != null)
         {
            string ext = Path.GetExtension(product.Photo.FileName).ToLower();
            if (ext == ".jpg" || ext == ".png")
            {
               string fileName = Path.Combine(hosting.WebRootPath, "Images\\product", product.Photo.FileName);
               using (var fileStream = new FileStream(fileName, FileMode.Create))
               {
                  product.Photo.CopyTo(fileStream);
                  product.PhotoPath = "/Images/product/" + product.Photo.FileName;
               }
            }
         }
         _context.Update(product);
         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));





         //if (ModelState.IsValid)
         //{
         //   string Images = Path.Combine(hosting.WebRootPath, "Images");
         //   //if (product.Photo.Length > 0)
         //   if (product.Photo != null)
         //   {
         //      string filePath = Path.Combine(Images, product.Photo.FileName);
         //      using (Stream fileStream = new FileStream(filePath, FileMode.Create))
         //      {
         //         await product.Photo.CopyToAsync(fileStream);
         //         product.Photo = product.Photo.FileName;
         //      }
         //   }

         //   //if (product.Photo != null)
         //   //{
         //   //   string ext = Path.GetExtension(product.Photo.FileName).ToLower();
         //   //   if (ext == ".jpg" || ext == ".png")
         //   //   {
         //   //      string fileName = Path.Combine(hosting.WebRootPath, "Images\\product", product.Photo.FileName);
         //   //      using (var fileStream = new FileStream(fileName, FileMode.Create))
         //   //      {
         //   //         product.Photo.CopyTo(fileStream);
         //   //         product.Photo = "/Images/product/" + product.Photo.FileName;
         //   //      }
         //   //   }
         //   //}

         //   try
         //   {
         //      _context.Update(product);
         //      await _context.SaveChangesAsync();
         //   }
         //   catch (DbUpdateConcurrencyException)
         //   {
         //      if (!ProductExists(product.Id))
         //      {
         //         return NotFound();
         //      }
         //      else
         //      {
         //         throw;
         //      }
         //   }
         //   return RedirectToAction(nameof(Index));
         //}
         //return View(product);
      }

      public async Task<IActionResult> Delete(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var product = await _context.Products
             .Include(p => p.Category)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (product == null)
         {
            return NotFound();
         }

         return View(product);
      }

      // POST: Product/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         var product = await _context.Products.FindAsync(id);
         _context.Products.Remove(product);
         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool ProductExists(int id)
      {
         return _context.Products.Any(e => e.Id == id);
      }
   }
}
