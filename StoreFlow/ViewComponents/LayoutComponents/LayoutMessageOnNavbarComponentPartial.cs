using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.LayoutComponents
{
    public class LayoutMessageOnNavbarComponentPartial:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public LayoutMessageOnNavbarComponentPartial(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var values = _storeContext.Messages.Where(x => x.IsRead == false).OrderByDescending(y => y.MessageId).Take(4).ToList();
            @ViewBag.messageCount = values.Count;
            return View(values);
        }
    }
}
