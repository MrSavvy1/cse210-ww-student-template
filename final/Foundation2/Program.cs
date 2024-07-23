using System;
using System.Collections.Generic;
using System.Text;

public class Address
{
		private string street;
		private string city;
		private string state;
		private string country;

		public Address(string street, string city, string state, string country)
		{
				this.street = street;
				this.city = city;
				this.state = state;
				this.country = country;
		}

		public bool IsInUSA()
		{
				return country.ToLower() == "usa";
		}

		public override string ToString()
		{
				return $"{street}, {city}, {state}, {country}";
		}
}

public class Customer
{
		private string name;
		private Address address;

		public Customer(string name, Address address)
		{
				this.name = name;
				this.address = address;
		}

		public string Name => name;
		public Address Address => address;

		public bool IsInUSA()
		{
				return address.IsInUSA();
		}
}

public class Product
{
		private string name;
		private string productID;
		private float price;
		private int quantity;

		public Product(string name, string productID, float price, int quantity)
		{
				this.name = name;
				this.productID = productID;
				this.price = price;
				this.quantity = quantity;
		}

		public string Name => name;
		public string ProductID => productID;
		public float Price => price;
		public int Quantity => quantity;

		public float GetTotalCost()
		{
				return price * quantity;
		}
}

public class Order
{
		private List<Product> products;
		private Customer customer;

		public Order(Customer customer)
		{
				this.customer = customer;
				this.products = new List<Product>();
		}

		public void AddProduct(Product product)
		{
				products.Add(product);
		}

		public float CalculateTotalCost()
		{
				float totalCost = 0;
				foreach (var product in products)
				{
						totalCost += product.GetTotalCost();
				}

				return totalCost;
		}

		public string GetPackingLabel()
		{
				StringBuilder packingLabel = new StringBuilder();
				packingLabel.AppendLine("Packing Label:");
				foreach (var product in products)
				{
						packingLabel.AppendLine($"{product.Quantity} x {product.Name}");
				}
				return packingLabel.ToString();
		}

		public string GetShippingLabel()
		{
				return $"Shipping Label:\n{customer.Name}\n{customer.Address}";
		}
}

class Program
{
		static void Main(string[] args)
		{
				Address address = new Address("123 Main St", "Springfield", "IL", "USA");
				Customer customer = new Customer("John Doe", address);

				Order order = new Order(customer);

				Product product1 = new Product("Laptop", "L001", 999.99f, 1);
				Product product2 = new Product("Mouse", "M001", 19.99f, 2);

				order.AddProduct(product1);
				order.AddProduct(product2);

				Console.WriteLine(order.GetPackingLabel());
				Console.WriteLine(order.GetShippingLabel());
				Console.WriteLine($"Total Cost: ${order.CalculateTotalCost()}");
		}
}
