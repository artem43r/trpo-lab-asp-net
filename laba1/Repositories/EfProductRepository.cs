using laba1.Data;
using laba1.Models;
using Microsoft.EntityFrameworkCore;

namespace laba1.Repositories
{
    public class EfProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public EfProductRepository(AppDbContext context)
        {
            _context = context;
        }

        // ===== ОСНОВНЫЕ МЕТОДЫ =====

        public IEnumerable<Product> GetAll() =>
            _context.Products.ToList();

        public Product? GetById(int id) =>
            _context.Products.Find(id);

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Product> GetByCategory(string category) =>
            _context.Products
                .Where(p => p.Category == category)
                .ToList();

        public IEnumerable<Product> GetInStock() =>
            _context.Products
                .Where(p => p.InStock)
                .ToList();

        // ===== LINQ МЕТОДЫ =====

        public IEnumerable<Product> GetProductsByPriceRange(decimal minPrice, decimal maxPrice) =>
            _context.Products
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .OrderBy(p => p.Price)
                .ToList();

        public IEnumerable<Product> GetTopExpensiveProducts(int count) =>
            _context.Products
                .OrderByDescending(p => p.Price)
                .Take(count)
                .ToList();

        public IEnumerable<Product> SearchProducts(string searchTerm) =>
            _context.Products
                .Where(p => p.Name.Contains(searchTerm) ||
                            p.Description.Contains(searchTerm) ||
                            p.Category.Contains(searchTerm))
                .OrderBy(p => p.Name)
                .ToList();

        public decimal GetAveragePrice() =>
            _context.Products.Average(p => p.Price);

        public int GetTotalCount() =>
            _context.Products.Count();

        public (decimal MinPrice, decimal MaxPrice) GetPriceRange() =>
            (
                MinPrice: _context.Products.Min(p => p.Price),
                MaxPrice: _context.Products.Max(p => p.Price)
            );

        public bool AnyInCategory(string category) =>
            _context.Products.Any(p => p.Category == category);

        public IEnumerable<IGrouping<string, Product>> GetProductsGroupedByCategory() =>
            _context.Products
                .GroupBy(p => p.Category)
                .OrderBy(g => g.Key)
                .ToList();

        public IEnumerable<Product> GetProductsWithPagination(int page, int pageSize) =>
            _context.Products
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

        public int GetTotalPages(int pageSize)
        {
            var totalCount = GetTotalCount();
            return (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        // ===== ASYNC МЕТОДЫ =====

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _context.Products.ToListAsync();

        public async Task<Product?> GetByIdAsync(int id) =>
            await _context.Products.FindAsync(id);

        public async Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice) =>
            await _context.Products
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .OrderBy(p => p.Price)
                .ToListAsync();

        public async Task<decimal> GetAveragePriceAsync() =>
            await _context.Products.AverageAsync(p => p.Price);

        public async Task<int> GetTotalCountAsync() =>
            await _context.Products.CountAsync();

        public async Task<IEnumerable<IGrouping<string, Product>>> GetProductsGroupedByCategoryAsync() =>
            await _context.Products
                .GroupBy(p => p.Category)
                .OrderBy(g => g.Key)
                .ToListAsync();
    }
}