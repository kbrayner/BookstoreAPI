using AutoMapper;
using BookstoreSystem.DTOs;
using BookstoreSystem.Models;
using BookstoreSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BookstoreSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetCategories(string? name)
        {
            List<Category> categories;
            if (name == null)
            {
                categories = await _categoryRepository.GetAll();
            }
            else
            {
                categories = await _categoryRepository.ListByName(name);
            }

            return Ok(_mapper.Map<IEnumerable<CategoryDTO>>(categories));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            Category? category = await _categoryRepository.GetById(id);
            if (category == null) 
            { 
                return NotFound();
            }
            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Add([FromBody] CategoryDTO categoryDTO)
        {
            Category category = _mapper.Map<Category>(categoryDTO);

            try
            {
                Category savedCategory = await _categoryRepository.Add(category);
                return Ok(_mapper.Map<CategoryDTO>(savedCategory));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException is PostgresException pgEx)
                {
                    if (pgEx.SqlState == "23505") // Unique violation code
                    {
                        return BadRequest("Could not create the Category because it already exists a category with this name");
                    }
                }
                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create a Category: {ex.Message}");
                throw new Exception("Could not create the Category. Please try it later.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDTO>> Update([FromBody] CategoryDTO categoryDTO, int id)
        {
            categoryDTO.Id = id;
            Category category = _mapper.Map<Category>(categoryDTO);

            bool foundId = _categoryRepository.IdExists(id);

            if (!foundId)
            {
                return NotFound();
            }

            try
            {
                Category savedCategory = await _categoryRepository.Update(category, id);
                return Ok(_mapper.Map<CategoryDTO>(savedCategory));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException is PostgresException pgEx)
                {
                    if (pgEx.SqlState == "23505") // Unique violation code
                    {
                        return BadRequest("Could not update the Category because it already exists a category with this name");
                    }
                }
                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not update a Category: {ex.Message}");
                throw new Exception("Could not update the Category. Please try it later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteById(int id)
        {
            int result = await _categoryRepository.Delete(id);
            return Ok(result);
        }
    }
}
