using BookstoreSystem.Models;

namespace BookstoreSystem.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<Book?> GetById(int id);
        bool BookTitleExists(string title);
        Task<List<Book>> GetByTitle(string title);
        Task<List<Book>> GetAll();
        Task<Book> Add(Book book);
        bool IdExists(int id);
        Task<Book> Update(Book book, int id);
        Task<int> Delete(int id);
    }
}
