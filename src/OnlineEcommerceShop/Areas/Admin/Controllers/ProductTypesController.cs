using Microsoft.AspNetCore.Mvc;
using OnlineEcommerceShop.Data;
using OnlineEcommerceShop.Models;

namespace OnlineEcommerceShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        ApplicationDbContext _db;

        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.ProductTypes.ToList());
        }

        // Create Get Method
        public IActionResult Create()
        {
            return View();
        }

        // Create Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes products)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTypes.Add(products);
                await _db.SaveChangesAsync();

                TempData["save"] = "Save Successfully";
                return RedirectToAction("Index");
            }

            return View(products);
        }

        // Edit Get Method
        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            else
            {
                var product = _db.ProductTypes.Find(Id);
                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(product);
                }
            }
        }

        // Edit Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes products)
        {
            if (ModelState.IsValid)
            {
                _db.Update(products);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(products);
        }

        // Details Get Method
        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            else
            {
                var product = _db.ProductTypes.Find(Id);
                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(product);
                }
            }
        }

        // Details Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes products)
        {
            return RedirectToAction("Index");
        }

        // Delete Get Method
        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            else
            {
                var product = _db.ProductTypes.Find(Id);
                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(product);
                }
            }
        }

        // Delete Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? Id, ProductTypes products)
        {
            if (Id == null)
            {
                return NotFound("Error");
            }
            if (Id != products.Id)
            {
                return NotFound();
            }
            var product = _db.ProductTypes.Find(Id);
            if (product == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(product);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(products);
        }
    }
}
