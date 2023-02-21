using BookstoreSystem.Models;

namespace BookstoreSystem.Repositories.Interfaces
{
    public interface IWriterRepositorie
    {
        Task<Writer> GetById(int id);
        Task<List<Writer>> GetByName(string name);
        Task<List<Writer>> GetAll();
        Task<Writer> Add(Writer writer);
        Task<bool> WriterExists(int id);
        Task<Writer> Update(Writer writer, int id);
        Task<int> Delete(int id);
    }
}
