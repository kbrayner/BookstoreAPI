using BookstoreSystem.Models;

namespace BookstoreSystem.Repositories.Interfaces
{
    public interface IBookRepositorie
    {
        Task<Book> GetById(int id);
        Task<List<Book>> GetByTitle(string title);
        Task<List<Book>> GetAll();
        Task<Book> Add(Book book);
        bool BookExists(int id);
        Task<Book> Update(Book book, int id);
        Task<int> Delete(int id);
    
    }
}
