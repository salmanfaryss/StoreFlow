using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents
{
    public class SidebarDashboardComponents:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
