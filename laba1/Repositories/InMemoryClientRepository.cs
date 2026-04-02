using laba1.Models;


namespace laba1.Repositories
{
    public class InMemoryClientRepository : IClientRepository
    {
        private List<Client> _clients = new List<Client>();
        private int _nextId = 1;

        public InMemoryClientRepository()
        {
            SeedData();
        }

        private void SeedData()
        {
            Add(new Client
            {
                FullName = "Иван Иванов",
                Company = "ООО Ромашка",
                INN = "1234567890",
                Phone = "+79991234567",
                Email = "ivan@mail.ru",
                RegistrationDate = DateTime.Now.AddYears(-3),
                IsActive = true
            });

            Add(new Client
            {
                FullName = "Петр Петров",
                Company = "ООО Тест",
                INN = "123456789012",
                Phone = "+79997654321",
                Email = "petr@mail.ru",
                RegistrationDate = DateTime.Now.AddYears(-1),
                IsActive = true
            });

            Add(new Client
            {
                FullName = "Анна Смирнова",
                Company = "ИП Смирнова",
                INN = "9876543210",
                Phone = "+79990001122",
                Email = "anna@mail.ru",
                RegistrationDate = DateTime.Now.AddYears(-2),
                IsActive = false
            });
        }

        public IEnumerable<Client> GetAll() => _clients;

        public Client GetById(int id) =>
            _clients.FirstOrDefault(c => c.Id == id);

        public void Add(Client client)
        {
            client.Id = _nextId++;
            _clients.Add(client);
        }

        public void Update(Client client)
        {
            var existing = GetById(client.Id);
            if (existing != null)
            {
                existing.FullName = client.FullName;
                existing.Company = client.Company;
                existing.INN = client.INN;
                existing.Phone = client.Phone;
                existing.Email = client.Email;
                existing.IsActive = client.IsActive;
            }
        }

        public void Delete(int id)
        {
            var client = GetById(id);
            if (client != null)
                _clients.Remove(client);
        }
    }
}