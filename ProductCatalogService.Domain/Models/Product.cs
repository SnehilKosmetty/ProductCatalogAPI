using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Domain.Models
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public bool IsAvailable { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Private constructor for EF Core
        private Product() { }

        public Product(string name, string description, decimal price, int stockQuantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty", nameof(name));

            if (price < 0)
                throw new ArgumentException("Price cannot be negative", nameof(price));

            if (stockQuantity < 0)
                throw new ArgumentException("Stock quantity cannot be negative", nameof(stockQuantity));

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            IsAvailable = stockQuantity > 0;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(string name, string description, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty", nameof(name));

            if (price < 0)
                throw new ArgumentException("Price cannot be negative", nameof(price));

            Name = name;
            Description = description;
            Price = price;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateStock(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("Stock quantity cannot be negative", nameof(quantity));

            StockQuantity = quantity;
            IsAvailable = quantity > 0;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
