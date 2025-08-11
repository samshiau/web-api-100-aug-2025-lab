
namespace DotnetCatchup;


public class PizzaOrder
{
    public required string Size { get; init; } 
    public List<string> Toppings { get; set; } = new List<string>();
    public Customer? Customer { get; init; }
}


public record Customer(string Name, decimal TotalPastOrders = 0);
/*{
    //public  string Name { get; init; } = string.Empty;
    //public decimal TotalPastOrders { get; init; }
}*/