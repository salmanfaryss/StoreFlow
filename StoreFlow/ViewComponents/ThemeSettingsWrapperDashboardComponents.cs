using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents
{
    public class ThemeSettingsWrapperDashboardComponents:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
