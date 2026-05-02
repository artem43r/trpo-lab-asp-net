using laba1.Data;
using laba1.Models;
using Microsoft.EntityFrameworkCore;

namespace laba1.Repositories
{
    public class EfClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public EfClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Client> GetAll()
        {
            return _context.Clients.ToList();
        }

        public Client GetById(int id)
        {
            return _context.Clients.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public void Update(Client client)
        {
            _context.Clients.Update(client);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var client = GetById(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
            }
        }
        // ===== LINQ МЕТОДЫ =====

        // По дате регистрации
        public IEnumerable<Client> GetClientsByDateRange(DateTime from, DateTime to) =>
            _context.Clients
                .Where(c => c.RegistrationDate >= from && c.RegistrationDate <= to)
                .OrderBy(c => c.RegistrationDate)
                .ToList();

        // Топ старых клиентов (дольше всего зарегистрированы)
        public IEnumerable<Client> GetOldestClients(int count) =>
            _context.Clients
                .OrderBy(c => c.RegistrationDate)
                .Take(count)
                .ToList();

        // Поиск
        public IEnumerable<Client> SearchClients(string term) =>
            _context.Clients
                .Where(c =>
                    (c.FullName != null && c.FullName.Contains(term)) ||
                    (c.Company != null && c.Company.Contains(term)) ||
                    (c.Email != null && c.Email.Contains(term)))
                .OrderBy(c => c.FullName)
                .ToList();

        // Группировка по активности (true / false)
        public IEnumerable<IGrouping<bool, Client>> GetClientsGroupedByActivity() =>
            _context.Clients
                .GroupBy(c => c.IsActive)
                .ToList();

        // Пагинация
        public IEnumerable<Client> GetClientsWithPagination(int page, int pageSize) =>
            _context.Clients
                .OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

        // Количество страниц
        public int GetTotalPages(int pageSize)
        {
            var total = _context.Clients.Count();
            return (int)Math.Ceiling(total / (double)pageSize);
        }
    }
}