using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents
{
    public class NavbarDashboardViewComponents:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
