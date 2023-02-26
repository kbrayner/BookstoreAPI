using AutoMapper;
using BookstoreSystem.DTOs;
using BookstoreSystem.Models;
using BookstoreSystem.Repositories;
using BookstoreSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                categories = await _categoryRepository.GetByName(name);
            }

            return Ok(_mapper.Map<IEnumerable<CategoryDTO>>(categories));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            Category category = await _categoryRepository.GetById(id);
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

            Category savedCategory = await _categoryRepository.Add(category);

            return Ok(_mapper.Map<CategoryDTO>(savedCategory));
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

            Category savedCategory = await _categoryRepository.Update(category, id);

            return Ok(_mapper.Map<CategoryDTO>(savedCategory));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteById(int id)
        {
            int result = await _categoryRepository.Delete(id);
            return Ok(result);
        }
    }
}
