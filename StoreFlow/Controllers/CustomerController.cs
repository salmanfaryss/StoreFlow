using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Entities;
using StoreFlow.Models.LINQ_Siniflari;

namespace StoreFlow.Controllers
{
    public class CustomerController : Controller
    {
        private readonly StoreContext _storeContext;

        public CustomerController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IActionResult CustomerListOrderByCustomerName()
        {
            var values = _storeContext.Customers.OrderBy(x => x.CustomerName).ToList();
            return View(values);
        }
        public IActionResult CustomerListOrderByDescBalance()
        {
            var values = _storeContext.Customers.OrderByDescending(x => x.CustomerBalance).ToList();
            return View(values);
        }
        public IActionResult CustomerGetByCity(string city)
        {
            var exist = _storeContext.Customers.Any(x => x.CustomerCity == city);
            if (exist)
            {
                ViewBag.message = $"{city} şehrinde en az 1 tane müşteri var";
            }
            else
            {
                ViewBag.message = $"{city} şehrinde hiç müşteri yok";
            }

            return View();
           
        }
        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer p)
        {
            _storeContext.Customers.Add(p);
            _storeContext.SaveChanges();
            return RedirectToAction("CustomerListOrderByCustomerName","Customer");
        }
        public IActionResult CustomerCountyCity()
        {
            var result = _storeContext.Customers
                .GroupBy(c => c.CustomerCity)
                .Select(g => new CustomerCountByCityViewModel
                {
                    City = g.Key,
                    CustomerCount = g.Count()
                }).ToList();
            return View(result);
        }
        public IActionResult CustomerListExcepCityIstanbul()
        {
            var allCustomers = _storeContext.Customers.ToList();
            var CustomerListInIstanbul = _storeContext.Customers.Where(c => c.CustomerCity == "İstanbul").Select(c => c.CustomerCity).ToList();
            var result = allCustomers.ExceptBy(CustomerListInIstanbul,c => c.CustomerCity).ToList();
            return View(result);
        }
        public IActionResult ParallelCustomer()
        {
            var customers = _storeContext.Customers.ToList();
            var result = customers.AsParallel().Where(c => c.CustomerCity.StartsWith("A",StringComparison.OrdinalIgnoreCase)).ToList();
            return View(result);
        }

        public IActionResult CustomerListWithDefaultEmpty()
        {
            var customer =_storeContext.Customers.Where(x => x.CustomerCity =="Ankara").ToList()
                .DefaultIfEmpty(new Customer
                {
                    CustomerId =0,
                    CustomerName ="Kayıd Yok",
                    CustomerSurname ="--------",
                    CustomerCity ="Ankara"
                }).ToList();
            return View(customer);
        }
        public IActionResult CustomerIntersectByCity()
        {
            var cityValues1 = _storeContext.Customers.Where(c => c.CustomerCity =="İstanbul").Select(x => x.CustomerName + " " +  x.CustomerSurname).ToList();
            var cityValues2 = _storeContext.Customers.Where(c => c.CustomerCity == "Ankara").Select(x => x.CustomerName + " " + x.CustomerSurname).ToList();

            var intersectValues = cityValues1.Intersect(cityValues2).ToList();
            return View(intersectValues);
        }
        public IActionResult CustomerCastExample()
        {
            var values = _storeContext.Customers.ToList();
            ViewBag.v = values;
            return View();
        }
        public IActionResult CustomerListWithIndex()
        {
            var customers = _storeContext.Customers.ToList()
                .Select((c,index) => new
                {
                    siraNo = index,
                    c.CustomerCity,
                    c.CustomerSurname,
                    c.CustomerName
                }).ToList();
            return View(customers);
        }
    }
}
