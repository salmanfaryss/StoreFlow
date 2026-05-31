using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents.DashboardChartComponents
{
    public class DashboardOrderStatusChartComponents:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public DashboardOrderStatusChartComponents(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var result = _storeContext.Orders
               .GroupBy(o => o.Status)
               .Select(g => new OrderStatusChartViewModel
               {
                   Status = g.Key,
                   Count = g.Count()
               })
               .ToList();
            return View(result);
            
        }
    }
}
