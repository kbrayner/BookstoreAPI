using BookstoreSystem.Data;
using BookstoreSystem.Models;
using BookstoreSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookstoreSystem.Repositories
{
    public class BookRepositorie : IBookRepositorie
    {
        private readonly BookstoreSystemDBContext _dbContext;
        public BookRepositorie(BookstoreSystemDBContext bookstoreSystemDBContext)
        {
            _dbContext = bookstoreSystemDBContext;
        }

        public async Task<Book> GetById(int id)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Book>> GetByTitle(string title)
        {
            return await _dbContext.Books.Where(x => x.Title.Contains(title)).ToListAsync();
        }

        public async Task<List<Book>> GetAll()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book> Add(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();

            return book;
        }

        public bool BookExists(int id)
        {
            return _dbContext.Books.Any(x => x.Id == id);
        }

        public async Task<Book> Update(Book book, int id)
        {
            bool bookExists = BookExists(id);


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
    }
}
