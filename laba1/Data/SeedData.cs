using laba1.Models;

namespace laba1.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            // ===== PRODUCTS =====
            if (!context.Products.Any())
            {
                var products = new Product[]
                {
                    new Product
                    {
                        Name = "Ноутбук ASUS",
                        Price = 75000,
                        Category = "Электроника",
                        Description = "Игровой ноутбук с RTX 3060",
                        CreatedDate = DateTime.Now.AddDays(-30),
                        InStock = true
                    },
                    new Product
                    {
                        Name = "Смартфон Samsung",
                        Price = 45000,
                        Category = "Электроника",
                        Description = "Galaxy S23 256GB",
                        CreatedDate = DateTime.Now.AddDays(-15),
                        InStock = true
                    },
                    new Product
                    {
                        Name = "Книга 'Изучаем C'",
                        Price = 1200,
                        Category = "Книги",
                        Description = "Подробное руководство по C",
                        CreatedDate = DateTime.Now.AddDays(-5),
                        InStock = false
                    }
                };

                await context.Products.AddRangeAsync(products);
            }

            // ===== CLIENTS =====
            if (!context.Clients.Any())
            {
                var clients = new Client[]
                {
                    new Client
                    {
                        FullName = "Иван Иванов",
                        Company = "ООО Ромашка",
                        INN = "1234567890",
                        Phone = "+79991234567",
                        Email = "ivan@mail.ru",
                        RegistrationDate = DateTime.Now.AddYears(-3),
                        IsActive = true
                    },
                    new Client
                    {
                        FullName = "Петр Петров",
                        Company = "ООО Тест",
                        INN = "123456789012",
                        Phone = "+79997654321",
                        Email = "petr@mail.ru",
                        RegistrationDate = DateTime.Now.AddYears(-1),
                        IsActive = true
                    },
                    new Client
                    {
                        FullName = "Анна Смирнова",
                        Company = "ИП Смирнова",
                        INN = "9876543210",
                        Phone = "+79990001122",
                        Email = "anna@mail.ru",
                        RegistrationDate = DateTime.Now.AddYears(-2),
                        IsActive = false
                    }
                };

                await context.Clients.AddRangeAsync(clients);
            }

            await context.SaveChangesAsync();
        }
    }
}