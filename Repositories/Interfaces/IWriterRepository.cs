using BookstoreSystem.Models;

namespace BookstoreSystem.Repositories.Interfaces
{
    public interface IWriterRepository
    {
        Task<Writer> GetById(int id);
        Task<List<Writer>> GetByName(string name);
        Task<List<Writer>> GetAll();
        Task<Writer> Add(Writer writer);
        bool IdExists(int id);
        Task<Writer> Update(Writer writer, int id);
        Task<int> Delete(int id);
    }
}
