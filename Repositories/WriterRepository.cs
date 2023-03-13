using BookstoreSystem.Data;
using BookstoreSystem.Models;
using BookstoreSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookstoreSystem.Repositories
{
    public class WriterRepository : IWriterRepository
    {
        private readonly BookstoreSystemDBContext _dbContext;

        public WriterRepository(BookstoreSystemDBContext bookstoreSystemDBContext)
        {
            _dbContext = bookstoreSystemDBContext;
        }

        public async Task<Writer?> GetById(int id)
        {
            return await _dbContext.Writers.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Writer>> ListByName(string name)
        {
            return await _dbContext.Writers.Where(x => EF.Functions.ILike(x.Name, $"%{name}%")).ToListAsync();
        }
        public async Task<List<Writer>> GetAll()
        {
            return await _dbContext.Writers.ToListAsync();
        }
        public async Task<Writer> Add(Writer writer)
        {
            await _dbContext.Writers.AddAsync(writer);
            await _dbContext.SaveChangesAsync();

            return writer;
        }
        public bool IdExists(int id)
        {
            return _dbContext.Writers.Any(x => x.Id == id);
        }
        public async Task<Writer> Update(Writer writer, int id)
        {
            bool writerExists = IdExists(id);


            if (!writerExists)
            {
                throw new Exception($"Escritor para o ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Writers.Update(writer);
            await _dbContext.SaveChangesAsync();

            return writer;
        }
        public async Task<int> Delete(int id)
        {
            return await _dbContext.Writers.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
    }
}
