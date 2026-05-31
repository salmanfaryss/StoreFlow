using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents.DashboardChartComponents
{
    public class DashboardOrderDateChartComponents:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public DashboardOrderDateChartComponents(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var data = _storeContext.Orders
               .GroupBy(o => o.OrderDate.Date)
               .Select(g => new
               {
                   RawDate = g.Key,
                   Count = g.Count()
               })
               .OrderBy(x => x.RawDate)
               .ToList()
               .Select(x => new OrderDateViewModel
               {
                   Date = x.RawDate.ToString("yyyy-MM-dd"),
                   Count = x.Count
               }).ToList();
            return View(data);
           
        }
    }
}
