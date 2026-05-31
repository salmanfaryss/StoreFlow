using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;
using StoreFlow.Entities;
using StoreFlow.Models;
using StoreFlow.Models.LINQ_Siniflari;



namespace StoreFlow.Controllers
{
    public class ProductController : Controller
    {
        private readonly StoreContext _storeContext;

        public ProductController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

       
        public IActionResult ProductList()
        {
            var values = _storeContext.Products.Include(x => x.Category).ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateProduct()
        {
            List<SelectListItem>category =(from k in _storeContext.Categories
                                           select new SelectListItem
                                           {
                                               Text = k.CategoryName,
                                               Value = k.CategoryId.ToString()
                                           }).ToList();
            ViewBag.Category = category;
            return View();
        }
        public IActionResult CreateProduct(Product p)
        {
            _storeContext.Products.Add(p);
            _storeContext.SaveChanges();
            return RedirectToAction("ProductList", "Product");
        }
        public IActionResult DeleteProduct(int id)
        {
            var values = _storeContext.Products.Find(id);
            _storeContext.Products.Remove(values);
            _storeContext.SaveChanges();
            return RedirectToAction("ProductList", "Product");
        }
        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {

            var categories = _storeContext.Categories
                                .Select(c => new SelectListItem
                                {
                                    Value = c.CategoryId.ToString(),
                                    Text = c.CategoryName
                                }).ToList();
            ViewBag.categories = categories;

            var values = _storeContext.Products.Find(id);
            return View(values);

          

        }
        [HttpPost]
        public IActionResult UpdateProduct(Product p)
        {
            _storeContext.Products.Update(p);
            _storeContext.SaveChanges();
            return RedirectToAction("ProductList", "Product");
        }
        public IActionResult First5ProductList()
        {
            var values = _storeContext.Products.Include(x => x.Category).Take(5).ToList();
            return View(values);
 
        }
        public IActionResult Skip4ProductList()
        {
            var values = _storeContext.Products.Include(x => x.Category).Skip(4).ToList();
            return View(values);
        }

        [HttpGet]
       
        public IActionResult CreateProductWithAttach()
        {
            return View();
        }
        //[HttpPost]
        [HttpPost]
        public IActionResult CreateProductWithAttach(Product product)
        {
            var category = new Category { CategoryId = 1 };
            _storeContext.Categories.Attach(category);

            var productValue = new Product
            {
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductStock = product.ProductStock,
                Category = category,
            };
            _storeContext.Products.Add(productValue);
            _storeContext.SaveChanges();
            return RedirectToAction("ProductList", "Product");

        }
        public IActionResult ProductCount()
        {
            var values = _storeContext.Products.LongCount();

            var lastProduct = _storeContext.Products.OrderBy(x => x.ProductId).Last();

            ViewBag.v2 = lastProduct.ProductName;
            ViewBag.v = values;

            return View();
        }
        public IActionResult ProductListWithCategory()
        {
            var result = from c in _storeContext.Categories
                         join p in _storeContext.Products
                         on c.CategoryId equals p.CategoryId
                         select new ProductListWithCategoryViewModel
                         {
                             ProductName = p.ProductName,
                             ProductPrice = p.ProductPrice,
                             ProductStock = p.ProductStock,
                             CategoryName = c.CategoryName
                         };
            return View(result.ToList());
        }
        public IActionResult MostExpensiveProduct()
        {
            var product = _storeContext.Products
                           .OrderByDescending(x => x.ProductPrice)
                           .Select(p => new MostExpensiveProduct
                           {
                               ProductName = p.ProductName,
                               CategoryName = p.Category.CategoryName
                           }).FirstOrDefault();

            ViewBag.Product = product;

            return View(product);
        }
        public IActionResult CategoryWithMostProducts()
        {
            var result = _storeContext.Products
                .GroupBy(p => p.CategoryId)
                .Select(g => new CategoryWithMostProducts
                {
                    CategoryName = _storeContext.Categories
                    .Where(c => c.CategoryId == g.Key)
                    .Select(c => c.CategoryName).FirstOrDefault(),
                    ProductCount = g.Count()
                }).OrderByDescending(x => x.ProductCount).FirstOrDefault();
            return View(result);
        }


    }
}
