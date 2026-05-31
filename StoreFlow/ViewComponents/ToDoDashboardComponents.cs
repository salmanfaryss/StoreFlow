using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents
{
    public class ToDoDashboardComponents:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public ToDoDashboardComponents(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            var values = _storeContext.Todos.OrderByDescending(x => x.TodoId).Take(6).ToList();
            return View(values);
        }
    }
}
