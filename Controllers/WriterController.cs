using AutoMapper;
using BookstoreSystem.DTOs;
using BookstoreSystem.Models;
using BookstoreSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                writers = await _writerRepository.GetByName(name);
            }

            return Ok(_mapper.Map<IEnumerable<WriterDTO>>(writers));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WriterDTO>> GetById(int id)
        {
            Writer writer = await _writerRepository.GetById(id);
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

            Writer savedWriter = await _writerRepository.Add(writer);

            return Ok(_mapper.Map<WriterDTO>(savedWriter));
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

            Writer savedWriter = await _writerRepository.Update(writer, id);

            return Ok(_mapper.Map<WriterDTO>(savedWriter));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteById(int id)
        {
            int result = await _writerRepository.Delete(id);
            return Ok(result);
        }
    }
}
