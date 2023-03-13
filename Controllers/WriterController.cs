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
    public class WriterController : ControllerBase
    {
        private readonly IWriterRepository _writerRepository;
        private readonly IMapper _mapper;
        public WriterController(IWriterRepository writerRepository, IMapper mapper)
        {
            _mapper = mapper;
            _writerRepository = writerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<WriterDTO>>> GetWriters(string? name)
        {
            List<Writer> writers;
            if (name == null)
            {
                writers = await _writerRepository.GetAll();
            }
            else
            {
                writers = await _writerRepository.ListByName(name);
            }

            return Ok(_mapper.Map<IEnumerable<WriterDTO>>(writers));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WriterDTO>> GetById(int id)
        {
            Writer? writer = await _writerRepository.GetById(id);
            if (writer == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WriterDTO>(writer));
        }

        [HttpPost]
        public async Task<ActionResult<WriterDTO>> Add([FromBody] WriterDTO writerDTO)
        {
            Writer writer = _mapper.Map<Writer>(writerDTO);

            try
            {
                Writer savedWriter = await _writerRepository.Add(writer);
                return Ok(_mapper.Map<WriterDTO>(savedWriter));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException is PostgresException pgEx)
                {
                    if (pgEx.SqlState == "23505") // Unique violation code
                    {
                        return BadRequest("Could not update the Writer because it already exists a writer with this name");
                    }
                }
                throw ex;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Could not update a Writer: {ex.Message}");
                throw new Exception("Could not update the Writer. Please try it later.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WriterDTO>> Update([FromBody] WriterDTO writerDTO, int id)
        {
            writerDTO.Id = id;
            Writer writer = _mapper.Map<Writer>(writerDTO);

            bool foundId = _writerRepository.IdExists(id);

            if (!foundId)
            {
                return NotFound();
            }

            try
            {
                Writer savedWriter = await _writerRepository.Update(writer, id);

                return Ok(_mapper.Map<WriterDTO>(savedWriter));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException is PostgresException pgEx)
                {
                    if (pgEx.SqlState == "23505") // Unique violation code
                    {
                        return BadRequest("Could not add the Writer because it already exists a writer with this name");
                    }
                }
                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not add a Writer: {ex.Message}");
                throw new Exception("Could not add the Writer. Please try it later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteById(int id)
        {
            int result = await _writerRepository.Delete(id);
            return Ok(result);
        }
    }
}
