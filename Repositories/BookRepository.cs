using BookstoreSystem.Data;
using BookstoreSystem.Models;
using BookstoreSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookstoreSystem.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookstoreSystemDBContext _dbContext;
        public BookRepository(BookstoreSystemDBContext bookstoreSystemDBContext)
        {
            _dbContext = bookstoreSystemDBContext;
        }

        public async Task<Book?> GetById(int id)
        {
            return await _dbContext.Books
                .Include(book => book.Category)
                .Include(book => book.Publisher)
                .Include(book => book.Writers)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Book>> ListByTitle(string title)
        {
            return await _dbContext.Books
                .Include(book => book.Category)
                .Include(book => book.Publisher)
                .Include(book => book.Writers)
                .Where(x => EF.Functions.ILike(x.Title, $"%{title}%")).ToListAsync();
        }
        public async Task<List<Book>> GetAll()
        {
            return await _dbContext.Books
                .Include(book => book.Category)
                .Include(book => book.Publisher)
                .Include(book => book.Writers)
                .ToListAsync();
        }
        public async Task<Book> Add(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();

            return book;
        }
        public bool IdExists(int id)
        {
            return _dbContext.Books.Any(x => x.Id == id);
        }
        public async Task<Book> Update(Book book, int id)
        {
            bool bookExists = IdExists(id);


            if (!bookExists)
            {
                throw new Exception($"Livro para o ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();

            return book;
        }
        public async Task<int> Delete(int id)
        {
            return await _dbContext.Books.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public bool BookTitleExists(string title)
        {
            return  _dbContext.Books.Any(x => x.Title == title);
        }
    }
}
