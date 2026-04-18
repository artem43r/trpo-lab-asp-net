using laba1.Data;
using laba1.Models;
using laba1.Repositories;
using Microsoft.EntityFrameworkCore;
namespace laba1.Repositories
{
    public class EfProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        // Внедрение контекста через конструктор
        public EfProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public Product? GetById(int id)
        {
            return _context.Products.Find(id);
        }
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
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
        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
        public IEnumerable<Product> GetByCategory(string category)
        {
            return _context.Products
            .Where(p => p.Category == category)
            .ToList();
        }
        public IEnumerable<Product> GetInStock()
        {
            return _context.Products
            .Where(p => p.InStock)
            .ToList();
        }
    }
}
