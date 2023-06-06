using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineEcommerceShop.Data;
using OnlineEcommerceShop.Models;
using Microsoft.AspNetCore.Hosting;

namespace OnlineEcommerceShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        [Obsolete]
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        [Obsolete]
        public ProductsController(ApplicationDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            return View(_context.Products.Include(c => c.ProductTypes).ToList());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.ProductTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes.ToList(), "Id", "ProductType");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> Create(Products products, IFormFile image)
        {

            var searchProduct = _context.Products.FirstOrDefault(x => x.Name == products.Name);

            if (searchProduct != null)
            {
                ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "ProductType", products.ProductTypeId);
                ViewBag.message = "This name of Product has been already exits!";

                return View(products);
            }

            //if (ModelState.IsValid)
            //{
            if (image != null)
            {
                var name = Path.Combine(_environment.WebRootPath + "/images", Path.GetFileName(image.FileName));
                await image.CopyToAsync(new FileStream(name, FileMode.Create));
                products.Image = "Images/" + image.FileName;
            }
            else
            {
                products.Image = "Images/notNull";
            }
            _context.Products.Add(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            //ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "ProductType", products.ProductTypeId);
            //return View(products);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "ProductType", products.ProductTypeId);
            return View(products);
            //ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes.ToList(), "Id", "ProductType");
            //return View();
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> Edit(Products products, IFormFile image)
        {
            //if (id != products.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(products);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ProductsExists(products.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "ProductType", products.ProductTypeId);
            //return View(products);

            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "ProductType", products.ProductTypeId);

            if (image != null)
            {
                var name = Path.Combine(_environment.WebRootPath + "/images", Path.GetFileName(image.FileName));
                await image.CopyToAsync(new FileStream(name, FileMode.Create));
                products.Image = "Images/" + image.FileName;
            }
            _context.Products.Update(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.ProductTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
                _context.Products.Remove(products);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
