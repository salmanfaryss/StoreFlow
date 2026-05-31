using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents
{
    public class DailySalesDashboardComponents:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public DailySalesDashboardComponents(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var data = _storeContext.Todos
                .GroupBy(t => t.Priority)
                .Select(g => new TodoStatusChartViewModel
                {
                    Status = g.Key,
                    Count = g.Count()
                }).ToList();
            return View(data);
        }
    }
}
