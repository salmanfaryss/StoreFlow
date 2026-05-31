using StoreFlow.Entities;

namespace StoreFlow.Models.LINQ_Siniflari
{
    public class CustomerOrderViewModel
    {
        public string CustomerName { get; set; }
        public List<Order> Orders { get; set; }
    }
}
