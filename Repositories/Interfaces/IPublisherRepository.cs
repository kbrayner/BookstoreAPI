using BookstoreSystem.Models;

namespace BookstoreSystem.Repositories.Interfaces
{
    public interface IPublisherRepository
    {
        Task<Publisher> GetById(int id);
        Task<List<Publisher>> GetByName(string name);
        Task<List<Publisher>> GetAll();
        Task<Publisher> Add(Publisher publisher);
        bool IdExists(int id);
        Task<Publisher> Update(Publisher publisher, int id);
        Task<int> Delete(int id);
    }
}
