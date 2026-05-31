using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents
{
    public class RightSidebarDashboardComponents:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
