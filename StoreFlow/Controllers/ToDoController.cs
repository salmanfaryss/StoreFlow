using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Entities;
using StoreFlow.Models.LINQ_Siniflari;

namespace StoreFlow.Controllers
{
    public class ToDoController : Controller
    {
        private readonly StoreContext _context;

        public ToDoController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateToDo()
        {
            var todos = new List<Todo>()
            {
                new Todo{ Description ="Mail Gönderme", Status =true, Priority ="Medium"},
                new Todo{Description ="Rapor Hazırla",Status =false,Priority="Low"},
                new Todo{Description ="Toplantıya Katıl",Status=true,Priority="Height"}
            };
            _context.Todos.AddRange(todos);
            _context.SaveChanges();
            return View();
        }
        public IActionResult TodoAggreatePriority()
        {
            var priorityFirstTodo = _context.Todos
                .Where(x => x.Priority =="Height").Select(y => y.Description).ToList();
            var result = priorityFirstTodo.Aggregate((acc, desc) => acc + ", " + desc);
            ViewBag.results = result;
            return View();
        }
        public IActionResult IncompletTask()
        {
            var values = _context.Todos.Where(x => !x.Status).Select(y => y.Description).ToList()
                .Prepend("Gün Başında tüm görevleri kontrol etmeyi unutmayın!").ToList();
            return View(values);
        }
        public IActionResult TodoChunk()
        {
            var values = _context.Todos.Where(x => !x.Status).ToList().Chunk(2).ToList();
            return View(values);
        }

        public IActionResult TodoConcate()
        {
            var values = _context.Todos.Where(x => x.Priority == "Height").ToList()
                .Concat(
                 _context.Todos.Where(y => y.Priority == "Medium").ToList()).ToList();
            return View(values);
        }
        public IActionResult ToDosByPriorytyCount()
        {
            var values = _context.Todos
                .GroupBy(t => t.Priority)
                .Select( g => new ToDoByPrioryteCount
                {
                    
                    Priority = g.Key,
                    Count = g.Count()
                }).ToList();
            return View(values);
        }
    }
}
