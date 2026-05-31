using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents
{
    public class ChartDashboardComponents:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
