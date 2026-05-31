using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.RightSideBarComponents
{
    public class RightSidebarToDoListComponentPartial:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public RightSidebarToDoListComponentPartial(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var values = _storeContext.Todos.OrderBy(x => x.TodoId).ToList().TakeLast(15).ToList();
            return View(values);
        }
    }
}
