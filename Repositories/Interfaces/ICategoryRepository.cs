using BookstoreSystem.Models;

namespace BookstoreSystem.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetById(int id);
        Task<List<Category>> ListByName(string name);
        Task<List<Category>> GetAll();
        Task<Category> Add(Category category);
        bool IdExists(int id);
        Task<Category> Update(Category category, int id);
        Task<int> Delete(int id);
    }
}
