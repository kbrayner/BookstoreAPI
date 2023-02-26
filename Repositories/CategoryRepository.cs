using BookstoreSystem.Data;
using BookstoreSystem.Models;
using BookstoreSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookstoreSystem.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookstoreSystemDBContext _dbContext;

        public CategoryRepository(BookstoreSystemDBContext bookstoreSystemDBContext)
        {
            _dbContext = bookstoreSystemDBContext;
        }

        public async Task<Category> GetById(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Category>> GetByName(string name)
        {
            return await _dbContext.Categories.Where(x => EF.Functions.ILike(x.Name, $"%{name}%")).ToListAsync();
        }
        public async Task<List<Category>> GetAll()
        {
            return await _dbContext.Categories.ToListAsync();
        }
        public async Task<Category> Add(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }
        public bool IdExists(int id)
        {
            return _dbContext.Categories.Any(x => x.Id == id);
        }
        public async Task<Category> Update(Category category, int id)
        {
            bool categoryExists = IdExists(id);


            if (!categoryExists)
            {
                throw new Exception($"Categoria para o ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }
        public async Task<int> Delete(int id)
        {
            return await _dbContext.Categories.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
    }
}
