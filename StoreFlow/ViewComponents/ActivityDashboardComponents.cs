using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents
{
    public class ActivityDashboardComponents:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public ActivityDashboardComponents(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var values = _storeContext.Activities.ToList();
            return View(values);
        }
    }
}
