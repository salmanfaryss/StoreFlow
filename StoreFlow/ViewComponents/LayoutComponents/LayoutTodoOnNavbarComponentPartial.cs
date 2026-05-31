using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.LayoutComponents
{
    public class LayoutTodoOnNavbarComponentPartial:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public LayoutTodoOnNavbarComponentPartial(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var values = _storeContext.Todos.Where(x => x.Status ==false).OrderByDescending(y =>y.TodoId).Take(5).ToList();
            ViewBag.todoTotalCount = values.Count;
            return View(values);
        }
    }
}
