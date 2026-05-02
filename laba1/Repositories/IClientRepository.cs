using laba1.Models;


namespace laba1.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAll();
        Client GetById(int id);
        void Add(Client client);
        void Update(Client client);
        void Delete(int id);
        IEnumerable<Client> GetClientsByDateRange(DateTime from, DateTime to);

        IEnumerable<Client> GetOldestClients(int count);

        IEnumerable<Client> SearchClients(string term);

        IEnumerable<IGrouping<bool, Client>> GetClientsGroupedByActivity();

        IEnumerable<Client> GetClientsWithPagination(int page, int pageSize);

        int GetTotalPages(int pageSize);
    }
}