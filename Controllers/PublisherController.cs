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
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IMapper _mapper;
        public PublisherController(IPublisherRepository publisherRepository, IMapper mapper)
        {
            _mapper = mapper;
            _publisherRepository = publisherRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<PublisherDTO>>> GetPublishers(string? name)
        {
            List<Publisher> publishers;
            if (name == null)
            {
                publishers = await _publisherRepository.GetAll();
            }
            else
            {
                publishers = await _publisherRepository.ListByName(name);
            }

            return Ok(_mapper.Map<IEnumerable<PublisherDTO>>(publishers));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherDTO>> GetById(int id)
        {
            Publisher? publisher = await _publisherRepository.GetById(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PublisherDTO>(publisher));
        }

        [HttpPost]
        public async Task<ActionResult<PublisherDTO>> Add([FromBody] PublisherDTO publisherDTO)
        {
            Publisher publisher = _mapper.Map<Publisher>(publisherDTO);

            try
            {
                Publisher savedPublisher = await _publisherRepository.Add(publisher);
                return Ok(_mapper.Map<PublisherDTO>(savedPublisher));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException is PostgresException pgEx)
                {
                    if (pgEx.SqlState == "23505") // Unique violation code
                    {
                        return BadRequest("Could not create the Publisher because it already exists a publisher with this name");
                    }
                }
                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create a Publisher: {ex.Message}");
                throw new Exception("Could not create the Publisher. Please try it later.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PublisherDTO>> Update([FromBody] PublisherDTO publisherDTO, int id)
        {
            publisherDTO.Id = id;
            Publisher publisher = _mapper.Map<Publisher>(publisherDTO);

            bool foundId = _publisherRepository.IdExists(id);

            if (!foundId)
            {
                return NotFound();
            }

            try
            {
                Publisher savedPublisher = await _publisherRepository.Update(publisher, id);
                return Ok(_mapper.Map<PublisherDTO>(savedPublisher));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException is PostgresException pgEx)
                {
                    if (pgEx.SqlState == "23505") // Unique violation code
                    {
                        return BadRequest("Could not update the Publisher because it already exists a publisher with this name");
                    }
                }
                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not update a Publisher: {ex.Message}");
                throw new Exception("Could not update the Publisher. Please try it later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteById(int id)
        {
            int result = await _publisherRepository.Delete(id);
            return Ok(result);
        }
    }
}
