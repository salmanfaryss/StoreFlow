using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents
{
    public class HeadDashboardViewComponents:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
