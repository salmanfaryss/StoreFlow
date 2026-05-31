using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Entities;
using StoreFlow.Models;
using StoreFlow.Models.LINQ_Siniflari;

namespace StoreFlow.Controllers
{
    public class CategoryController : Controller
    {
        private readonly StoreContext _storeContext;

        public CategoryController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IActionResult CategoryList()
        {
           

            var values = _storeContext.Categories.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(Category p)
        {
            _storeContext.Categories.Add(p);
            _storeContext.SaveChanges();
            return RedirectToAction("CategoryList","Category");
        }
        public IActionResult DeleteCategory(int id)
        {
            var values = _storeContext.Categories.Find(id);
            _storeContext.Categories.Remove(values);
            _storeContext.SaveChanges();
            return RedirectToAction("CategoryList", "Category");
        }
        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var values = _storeContext.Categories.Find(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateCategory(Category p)
        {
           _storeContext.Categories.Update(p);
           p.CategoryStatus = true;
            _storeContext.SaveChanges();
            return RedirectToAction("CategoryList", "Category");
           
        }
        public IActionResult ProductCount()
        {
            var result = _storeContext.Products
                         .GroupBy(p => p.CategoryId)
                         .Select(g => new CategoryCountViewModel
                         {
                             CategoryName = _storeContext.Categories
                             .Where(c => c.CategoryId == g.Key)
                             .Select(c => c.CategoryName).FirstOrDefault(),
                             Count = g.Count()
                         }).ToList();
            return View(result);
           
        }
        public IActionResult CategoryStockValues()
        {
            var result = _storeContext.Products
                         .GroupBy(p => p.CategoryId)
                         .Select(g => new CategoryStockValues
                         {
                             CategoryName = _storeContext.Categories
                             .Where(c => c.CategoryId == g.Key)
                             .Select(c => c.CategoryName).FirstOrDefault(),
                             StockCount =(int) g.Sum(p => p.ProductPrice * p.ProductStock)
                         }).ToList();
            return View(result);
        }
    }
}
