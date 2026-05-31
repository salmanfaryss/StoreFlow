using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents
{
    public class SalesDataDashboardComponents:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public SalesDataDashboardComponents(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var values = _storeContext.Orders.OrderByDescending(z => z.OrderId).Include(x => x.Customer).Include(y => y.Product).Take(5).ToList();
            return View(values);
           
        }
    }
}
