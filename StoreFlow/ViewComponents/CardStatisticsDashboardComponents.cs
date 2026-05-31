using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents
{
    public class CardStatisticsDashboardComponents:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public CardStatisticsDashboardComponents(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.totalCustomerCount = _storeContext.Customers.Count();
            ViewBag.totalCategoryCount = _storeContext.Categories.Count();
            ViewBag.totalProductCount = _storeContext.Products.Count();
            ViewBag.totalOrderCount = _storeContext.Orders.Count();
            ViewBag.avgCustomerBalance = _storeContext.Customers.Average(x => x.CustomerBalance);
            ViewBag.sumOrderProductCount = _storeContext.Orders.Sum(x => x.OrderCount);




            return View();
        }
    }
}
