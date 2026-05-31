using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents
{
    public class Last5MessagesDashboardComponents:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public Last5MessagesDashboardComponents(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var values = _storeContext.Messages.Where(y => y.IsRead==false  ).OrderBy(x => x.MessageId).ToList().TakeLast(5).ToList();
            return View(values);
        }
    }
}
