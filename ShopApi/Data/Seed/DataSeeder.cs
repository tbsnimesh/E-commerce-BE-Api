using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ShopApi.Domain.Entities;

namespace ShopApi.Data.Seed;

public static class DbSeeder 
{
    public static async Task SeedAsync(AppDbContext db)
    {
        // Idempotency check: if Categories already exist, do nothing.
        if (await db.Categories.AnyAsync())
        {
            return;
        }
        var electronics = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Electronics",
            Slug = "electronics"
        };

        var books = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Books",
            Slug = "books"
        };

        var clothing = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Clothing",
            Slug = "clothing"
        };

        db.Categories.AddRange(electronics, books, clothing);

        var product = new[]
        {
         new Product
        {
                Id = Guid.NewGuid(),
                Name = "Wireless Headphones",
                Description = "Over-ear, noise-cancelling.",
                Price = 7999.00m,
                StockQuantity = 25,
                ImageUrl = "https://example.com/headphones.jpg",
                CategoryId = electronics.Id,
                CreatedAt = DateTime.UtcNow
        },
          new Product
            {
                Id = Guid.NewGuid(),
                Name = "Mechanical Keyboard",
                Description = "Tactile switches, RGB backlight.",
                Price = 5499.00m,
                StockQuantity = 40,
                ImageUrl = "https://example.com/keyboard.jpg",
                CategoryId = electronics.Id,
                CreatedAt = DateTime.UtcNow
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Clean Code",
                Description = "A handbook of agile software craftsmanship.",
                Price = 650.00m,
                StockQuantity = 100,
                ImageUrl = "https://example.com/cleancode.jpg",
                CategoryId = books.Id,
                CreatedAt = DateTime.UtcNow
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "The Pragmatic Programmer",
                Description = "Your journey to mastery.",
                Price = 720.00m,
                StockQuantity = 80,
                ImageUrl = "https://example.com/pragprog.jpg",
                CategoryId = books.Id,
                CreatedAt = DateTime.UtcNow
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Cotton T-Shirt",
                Description = "Plain, unisex, machine washable.",
                Price = 499.00m,
                StockQuantity = 200,
                ImageUrl = "https://example.com/tshirt.jpg",
                CategoryId = clothing.Id,
                CreatedAt = DateTime.UtcNow
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Denim Jeans",
                Description = "Slim fit, mid-rise.",
                Price = 1899.00m,
                StockQuantity = 60,
                ImageUrl = "https://example.com/jeans.jpg",
                CategoryId = clothing.Id,
                CreatedAt = DateTime.UtcNow
            },
            new Product {
            Id = Guid.NewGuid(),
            Name = "Smartphone",
            Description = "Latest model with high-resolution display.",
            Price = 29999.00m,
            StockQuantity = 15,
            ImageUrl = "https://example.com/smartphone.jpg",
            CategoryId = electronics.Id,
            CreatedAt = DateTime.UtcNow
            },
            
            new Product {
                Id = Guid.NewGuid(),
                Name = "E-Reader",
                Description = "Compact device for reading e-books.",
                Price = 8999.00m,
                StockQuantity = 30,
                ImageUrl = "https://example.com/ereader.jpg",
                CategoryId = electronics.Id,
                CreatedAt = DateTime.UtcNow
                },
                
            new Product {
                    Id = Guid.NewGuid(),
                    Name = "Bluetooth Speaker",
                    Description = "Portable speaker with deep bass.",
                    Price = 3999.00m,
                    StockQuantity = 50,
                    ImageUrl = "https://example.com/speaker.jpg",
                    CategoryId = electronics.Id,
                    CreatedAt = DateTime.UtcNow
                    },
                
            new Product {
                    Id = Guid.NewGuid(),
                    Name = "Gaming Mouse",
                    Description = "Ergonomic design with customizable buttons.",
                    Price = 2499.00m,
                    StockQuantity = 70,
                    ImageUrl = "https://example.com/mouse.jpg",
                    CategoryId = electronics.Id,
                    CreatedAt = DateTime.UtcNow
            },
            new Product {
                    Id = Guid.NewGuid(),
                     Name = "USB-C Hub",
                    Description = "Multi-port adapter for laptops.",
                    Price = 1999.00m,
                    StockQuantity = 100,
                    ImageUrl = "https://example.com/hub.jpg",
                    CategoryId = electronics.Id,
                    CreatedAt = DateTime.UtcNow
                    },
                new Product {
                    Id = Guid.NewGuid(),
                    Name = "Noise-Cancelling Earbuds",
                    Description = "In-ear design with active noise cancellation.",
                    Price = 4999.00m,
                    StockQuantity = 40,
                    ImageUrl = "https://example.com/earbuds.jpg",
                    CategoryId = electronics.Id,
                    CreatedAt = DateTime.UtcNow
                },
                new Product { 
                    Id = Guid.NewGuid(),
                    Name = "4K Monitor",
                    Description = "27-inch display with ultra-high resolution.",
                    Price = 19999.00m,
                    StockQuantity = 20,
                    ImageUrl = "https://example.com/monitor.jpg",
                    CategoryId = electronics.Id,
                    CreatedAt = DateTime.UtcNow
                    },
                    new Product {
                        Id = Guid.NewGuid(),
                        Name = "External SSD",
                        Description = "Portable storage with fast read/write speeds.",
                        Price = 7999.00m,
                        StockQuantity = 60,
                        ImageUrl = "https://example.com/ssd.jpg",
                        CategoryId = electronics.Id,
                        CreatedAt = DateTime.UtcNow
                        },
                    new Product {
                        Id = Guid.NewGuid(),
                        Name = "Wireless Charger",
                        Description = "Fast charging pad for compatible devices.",
                        Price = 1499.00m,
                        StockQuantity = 80,
                        ImageUrl = "https://example.com/charger.jpg",
                        CategoryId = electronics.Id,
                        CreatedAt = DateTime.UtcNow
                        },
                    new Product { 
                        Id = Guid.NewGuid(),
                        Name = "Action Camera",
                        Description = "Compact camera for capturing adventures.",
                        Price = 8999.00m,
                        StockQuantity = 25,
                        ImageUrl = "https://example.com/actioncamera.jpg",
                        CategoryId = electronics.Id,
                        CreatedAt = DateTime.UtcNow
                        },  
                    new Product {
                        Id = Guid.NewGuid(),
                        Name = "Smartwatch",
                        Description = "Wearable device with fitness tracking features.",
                        Price = 12999.00m,
                        StockQuantity = 35,
                        ImageUrl = "https://example.com/smartwatch.jpg",
                        CategoryId = electronics.Id,
                        CreatedAt = DateTime.UtcNow
                        },
                    new Product {
                        Id = Guid.NewGuid(),
                        Name = "Portable Projector",
                        Description = "Compact projector for movies and presentations.",
                        Price = 14999.00m,
                        StockQuantity = 10,
                        ImageUrl = "https://example.com/projector.jpg",
                        CategoryId = electronics.Id,
                        CreatedAt = DateTime.UtcNow
                        }




        };
        db.Products.AddRange(product);
        await db.SaveChangesAsync();
    }
}