using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using System.Data.Entity;

namespace StoreFlow.Controllers
{
    public class MessageController : Controller
    {
        private readonly StoreContext _storeContext;

        public MessageController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IActionResult MessageList()
        {
            var values = _storeContext.Messages.AsNoTracking().ToList();
            return View(values);
        }
    }
}
