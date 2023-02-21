using BookstoreSystem.Data;
using BookstoreSystem.Models;
using BookstoreSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookstoreSystem.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly BookstoreSystemDBContext _dbContext;

        public PublisherRepository(BookstoreSystemDBContext bookstoreSystemDBContext)
        {
            _dbContext = bookstoreSystemDBContext;
        }

        public async Task<Publisher> GetById(int id)
        {
            return await _dbContext.Publishers.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Publisher>> GetByName(string name)
        {
            return await _dbContext.Publishers.Where(x => x.Name.Contains(name)).ToListAsync();
        }
        public async Task<List<Publisher>> GetAll()
        {
            return await _dbContext.Publishers.ToListAsync();
        }
        public async Task<Publisher> Add(Publisher publisher)
        {
            await _dbContext.Publishers.AddAsync(publisher);
            await _dbContext.SaveChangesAsync();

            return publisher;
        }
        public bool IdExists(int id)
        {
            return _dbContext.Publishers.Any(x => x.Id == id);
        }
        public async Task<Publisher> Update(Publisher publisher, int id)
        {
            bool publisherExists = IdExists(id);


            if (!publisherExists)
            {
                throw new Exception($"Editora para o ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Publishers.Update(publisher);
            await _dbContext.SaveChangesAsync();

            return publisher;
        }
        public async Task<int> Delete(int id)
        {
            return await _dbContext.Publishers.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
    }
}
