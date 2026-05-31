using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents
{
    public class Last5ProductstDashboardComponents:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public Last5ProductstDashboardComponents(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var values = _storeContext.Products.OrderBy(x => x.ProductId).ToList().TakeLast(5).ToList();
            return View(values);
        }
    }
}
