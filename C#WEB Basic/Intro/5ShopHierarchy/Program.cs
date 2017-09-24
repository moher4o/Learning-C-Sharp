using System;
using System.Linq;
using ShopHierarchy5.Models;

namespace ShopHierarchy5
{
    public class Program
    {
        static void Main()
        {
            using (ShopContext context = new ShopContext())
            {
                ClearDatabase(context);
                FillSalesman(context);
                FillItems(context);
                ReadCommands(context);
                PrintCustomerDataProblem8(context);
                PrintCustomerOrdersProblem9(context);
                PrintCustomersOrdersAndReviews(context);
                PrintSalesmanWithCustomers(context);
                PrintCustomersWithOrdersAndReviewsCount(context);

            }
        }

        private static void PrintCustomerOrdersProblem9(ShopContext context)
        {
            int customerId = int.Parse(Console.ReadLine());

            var customerData = context.
                          Customers
                          .Where(c => c.Id == customerId)
                          .Select(c => new
                          {
                              OrdersCount = c.Orders.Count(o => o.Items.Count > 1)
                          }).FirstOrDefault();

            Console.WriteLine($"Orders: {customerData.OrdersCount}");
        }

        private static void PrintCustomerDataProblem8(ShopContext context)
        {
            int customerId = int.Parse(Console.ReadLine());

            var customerData = context.
                          Customers
                          .Where(c => c.Id == customerId)
                          .Select(c => new
                          {
                              Name = c.Name,
                              OrdersCount = c.Orders.Count,
                              ReviewsCount = c.Reviews.Count,
                              SalesmanName = c.Salesman.Name
                          }).FirstOrDefault();

            Console.WriteLine($"Customer: {customerData.Name}");
            Console.WriteLine($"Orders count:{customerData.OrdersCount}");
            Console.WriteLine($"Reviews count: {customerData.ReviewsCount}");
            Console.WriteLine($"Salesman: {customerData.SalesmanName}");
        }

        private static void PrintCustomersOrdersAndReviews(ShopContext context)
        {
            int customerId = int.Parse(Console.ReadLine());

            var customerData = context.
                Customers
                .Where(c => c.Id == customerId)
                .Select(c => new
                {
                    Orders = c.Orders.Select(o => new
                    {
                        Id = o.Id,
                        ItemsCount = o.Items.Count
                    }).OrderBy(o => o.Id),
                    Reviews = c.Reviews.Count
                }
                ).FirstOrDefault();
            
            foreach (var order in customerData.Orders)
            {
                Console.WriteLine($"order {order.Id}: {order.ItemsCount} items");
            }
            Console.WriteLine($"reviews: {customerData.Reviews}");
        }

        private static void FillItems(ShopContext context)
        {
            while (true)
            {
                string[] inputTokens = Console.ReadLine().Split(';');
                if (inputTokens[0] == "END")
                {
                    break;
                }

                string name = inputTokens[0];
                decimal price = decimal.Parse(inputTokens[1]);

                context.Add(new Item() { Name = name, Price = price });
                context.SaveChanges();

            }
 
        }

        private static void PrintCustomersWithOrdersAndReviewsCount(ShopContext context)
        {
            var orderedCustomers = context.Customers
                .Select(c => new
                {
                    c.Name,
                    OrdersCount = c.Orders.Count,
                    ReviewsCount = c.Reviews.Count
                }
                )
                .OrderByDescending(o => o.OrdersCount)
                .ThenByDescending(r => r.ReviewsCount);
            foreach (var customer in orderedCustomers)
            {
                Console.WriteLine($"{customer.Name}");
                Console.WriteLine($"Orders: {customer.OrdersCount}");
                Console.WriteLine($"Reviews: {customer.ReviewsCount}");
            }
        }

        private static void PrintSalesmanWithCustomers(ShopContext context)
        {
            var orderedSalesmans = context.Salesmans
                 .Select(s => new
                  {
                    s.Name,
                    CustomersCount = s.Customers.Count
                  }
                )
                .OrderByDescending(s => s.CustomersCount)
                .ThenBy(s => s.Name)
                .ToList();
            foreach (var salesman in orderedSalesmans)
            {
                Console.WriteLine($"{salesman.Name} - {salesman.CustomersCount} customers");
            }
        }

        private static void ReadCommands(ShopContext context)
        {
            string input = string.Empty;
            while ((input = Console.ReadLine())!="END")
            {
                var inputTokens = input.Split('-');
                var command = inputTokens[0];
                var arguments = inputTokens[1];
                switch (command)
                {
                    case "register":
                        RegisterCommand(context, arguments);
                        break;
                    case "order":
                        OrderCommand(context, arguments);
                        break;
                    case "review":
                        ReviewCommand(context, arguments);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void ReviewCommand(ShopContext context, string arguments)
        {
            var parts = arguments.Split(';');
            var customerId = int.Parse(parts[0]);
            var itemId = int.Parse(parts[1]);

            context.Add(new Review() { CustomerId = customerId, ItemId = itemId });

            context.SaveChanges();
        }

        private static void OrderCommand(ShopContext context, string arguments)
        {
            var tokens = arguments.Split(';').ToList();

            int customerId = int.Parse(tokens[0]);

            var orderItems = tokens.Skip(1).ToList();

            var newOrder = new Order() { CustomerId = customerId };
            foreach (var item in orderItems)
            {
                
                newOrder.Items.Add(new ItemsOrders() { ItemId = int.Parse(item)});
            }

            context.Add(newOrder);

            context.SaveChanges();
        }

        private static void RegisterCommand(ShopContext context, string arguments)
        {
            var inputTokens = arguments.Split(';');
            int salesmanId = int.Parse(inputTokens[1]);

            string customerName = inputTokens[0];

            //Customer customer = new Customer() { Name = customerName };
            //EntityEntry<Customer> currentCustomer = context.Customers.Add(customer);
            //context.Salesmans.FirstOrDefault(s => s.Id == salesmanId).Customers.Add(currentCustomer.Entity);

            context.Add(new Customer() { Name = customerName , SalesmanId = salesmanId});
            context.SaveChanges();

        }

        private static void FillSalesman(ShopContext context)
        {
            var inputTokens = Console.ReadLine().Split(';');
            foreach (var name in inputTokens)
            {
                context.Salesmans.Add(new Salesman() { Name = name });
                context.SaveChanges();
            }

        }

        private static void ClearDatabase(ShopContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
