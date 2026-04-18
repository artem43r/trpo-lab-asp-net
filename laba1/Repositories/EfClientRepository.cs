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
    }
}