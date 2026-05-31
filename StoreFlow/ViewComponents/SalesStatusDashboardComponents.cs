using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents
{
    public class SalesStatusDashboardComponents:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public SalesStatusDashboardComponents(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var data = _storeContext.Customers
                .GroupBy(x => x.CustomerCity)
                .Select(g => new CustomerCityChartViewModel
                {
                    City = g.Key,
                    Count = g.Count()
                }).ToList();
            return View(data);
        }
    }
}
