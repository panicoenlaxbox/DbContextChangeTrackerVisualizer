using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContextChangeTrackerVisualizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new ShopContextInitializer());
            using (var context = new ShopContext())
            {
                var customer = context.Customers.Find(1);
                customer.Name = "Customer 1 modified";
                customer = context.Customers.Find(2);
                context.Customers.Remove(customer);
                context.Products.Find(1);
                context.Customers.Add(new Customer() {CustomerId = 3, Name = "Customer 3"});
                // How can I see the state of all my entities?
                var html = context.DumpAsHtml();
                Debug.WriteLine(html);
            }
        }
    }

    class ShopContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
    }

    class ShopContextInitializer : DropCreateDatabaseAlways<ShopContext>
    {
        protected override void Seed(ShopContext context)
        {
            context.Customers.Add(new Customer() { CustomerId = 1, Name = "Customer 1" });
            context.Customers.Add(new Customer() {CustomerId = 2, Name = "Customer 2"});
            context.Products.Add(new Product() { ProductId = 1, Name = "Product 1" });
        }
    }

    class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
    }

    class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
    }
}
