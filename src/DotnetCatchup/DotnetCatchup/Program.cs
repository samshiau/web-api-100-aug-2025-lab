//using DotnetCatchup;

//Console.WriteLine("Hello, World! From No top Level Statements");


//var myOrder = new PizzaOrder()
//{
//    Size = "lg",
//    Customer = new Customer { Name = "Phil" }
//};
////myOrder.Size = "lg";
////myOrder.Customer.Name = "Babs";
////myOrder.Size = "xl";
//myOrder.Toppings.Add("Cheese");
//myOrder.Toppings.Add("Onions");

//myOrder.Customer.Name.ToUpper();


//Console.WriteLine(myOrder.Size.ToUpper());
//Console.WriteLine($"You have {myOrder.Toppings.Count} toppings");


//var cust1 = new Customer { Name = "Steve" };
//var cust2 = new Customer { Name = "Steve" };

//var myPay = 12.95M;
//var yourPay = 12.96M;

//string myName = "Jeff";
//var yourName = "Jeff";

//Console.WriteLine($"Name is the same: {myName == yourName}");

//Console.WriteLine($"Pay is the same: {myPay == yourPay}");

//if(cust1 == cust2)
//{
//    Console.WriteLine("They are the same");
//    Console.WriteLine(cust1);
//} else
//{
//    Console.WriteLine("They are different");
//    Console.WriteLine(cust1);

//}


using System.Net.WebSockets;
using DotnetCatchup;

var c = new Customer("Bob", 125.23M);


var c2 = new Customer("Robert");

var c3 = c with { Name = "Bobby" };

Console.WriteLine(c);
Console.WriteLine(c3);
