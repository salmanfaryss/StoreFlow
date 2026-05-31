using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.RightSideBarComponents
{
    public class RightSidebarMessageComponentPartial:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public RightSidebarMessageComponentPartial(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var values = _storeContext.Messages.Where(x => x.IsRead ==false).Take(5).ToList();
            
            return View(values);
        }
    }
}
