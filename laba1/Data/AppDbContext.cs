using laba1.Models;
using Microsoft.EntityFrameworkCore;
namespace laba1.Data
{
    public class AppDbContext : DbContext
    {
        // Конструктор, принимающий параметры подключения
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        // DbSet представляет таблицу Products в базе данных
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        // Дополнительная настройка модели (опционально)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Настройка таблицы Products
            modelBuilder.Entity<Product>(entity =>
            {
                // Первичный ключ
                entity.HasKey(p => p.Id);
                // Настройка поля Name
                entity.Property(p => p.Name)
                .IsRequired() // NOT NULL
               .HasMaxLength(100); // VARCHAR(100)
                                   // Настройка поля Price (точность для денежных сумм)
                entity.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
                // Индекс для быстрого поиска по категории
                entity.HasIndex(p => p.Category)
                .HasDatabaseName("IX_Products_Category");
            });
        }
    }
}