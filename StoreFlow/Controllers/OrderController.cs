using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using StoreFlow.Context;
using StoreFlow.Entities;
using StoreFlow.Models.LINQ_Siniflari;


//using System.Data.Entity;
using System.Threading.Tasks;

namespace StoreFlow.Controllers
{
    public class OrderController : Controller
    {
        private readonly StoreContext _storeContext;

        public OrderController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public IActionResult OrderListByStatus(string status)
        {
            var values = _storeContext.Orders.Where(x => x.Status.Contains(status)).ToList();
            if (!values.Any())
            {
                ViewBag.v = "Bu Status ile ilgili veri bulunamadı!";
            }
            return View(values);
        }
        public IActionResult AllStockSmallerThen5()
        {
            var orderStockCount = _storeContext.Orders.All(x => x.OrderCount <= 5);
            if (orderStockCount)
            {
                ViewBag.v = "Tüm Siparişler 5 adetten küçüktür!";
            }
            else
            {
                ViewBag.v = "Tüm Siparişler 5 adetten küçük değildir!";
            }
            return View();
        }
        public IActionResult OrderListSearch(string name, string filterType)
        {
            if(filterType == "start")
            {
                var values = _storeContext.Orders.Where(x => x.Status.StartsWith(name)).ToList();
            }
            else if(filterType == "end")
            {
                var values = _storeContext.Orders.Where(x => x.Status.EndsWith(name)).ToList();
            }
            var orderValues = _storeContext.Orders.ToList();
            return View(orderValues);
        }

        public IActionResult OrderListWithCustomerGroup()
        {
            var result = from customer in _storeContext.Customers
                         join order in _storeContext.Orders
                         on customer.CustomerId equals order.CustomerId
                         into orderGroup
                         select new CustomerOrderViewModel
                         {
                             CustomerName = customer.CustomerName + " " + customer.CustomerSurname,
                             Orders = orderGroup.ToList(),
                         };
            return View(result.ToList());
        }
        public IActionResult OrderListByCustomer()
        {
            var order = _storeContext.Orders
                .Where(o => o.CustomerId == 1)
                .Select(o => new
                {
                    o.OrderId,
                    o.OrderDate,
                    o.OrderCount
                }).ToList();
            return View(order);
        }
        public async Task<IActionResult> OrderList()
        {
            var orders = await _storeContext.Orders.Include(c => c.Product).Include(p => p.Customer).ToListAsync();
            return View(orders);
        }
        [HttpGet]
        public IActionResult CreateOrder()
        {
            var products = _storeContext.Products.Select( p=> new SelectListItem
            {
               Value = p.ProductId.ToString(),
               Text = p.ProductName
            }).ToList();
            var customer = _storeContext.Customers.Select(c => new SelectListItem
            {
                Value = c.CustomerId.ToString(),
                Text = c.CustomerName
            }).ToList();
            ViewBag.c = customer;
            ViewBag.p = products;
            return View();
        }
        [HttpPost]
        public IActionResult CreateOrder(Order p)
        {
            p.Status = "Sipariş Alındı";
            _storeContext.Orders.Add(p);
            _storeContext.SaveChanges();
            return RedirectToAction("OrderList","Order");
        }
        [HttpGet]
        public IActionResult UpdateOrder(int id)
        {

            var products = _storeContext.Products.Select(p => new SelectListItem
            {
                Value = p.ProductId.ToString(),
                Text = p.ProductName
            }).ToList();
            var customer = _storeContext.Customers.Select(c => new SelectListItem
            {
                Value = c.CustomerId.ToString(),
                Text = c.CustomerName
            }).ToList();
            ViewBag.c = customer;
            ViewBag.p = products;

            var values = _storeContext.Orders.Find(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateOrder(Order p)
        {
            _storeContext.Orders.Update(p);
            _storeContext.SaveChanges();
            return RedirectToAction("OrderList","Order");
        }
        public IActionResult DeleteOrder(int id)
        {
            var values = _storeContext.Orders.Find(id);
            _storeContext.Orders.Remove(values);
            _storeContext.SaveChanges();
            return RedirectToAction("OrderList", "Order");
        }
    }
}
