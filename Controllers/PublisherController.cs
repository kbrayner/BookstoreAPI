using AutoMapper;
using BookstoreSystem.DTOs;
using BookstoreSystem.Models;
using BookstoreSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
                publishers = await _publisherRepository.GetByName(name);
            }

            return Ok(_mapper.Map<IEnumerable<PublisherDTO>>(publishers));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherDTO>> GetById(int id)
        {
            Publisher publisher = await _publisherRepository.GetById(id);
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

            Publisher savedPublisher = await _publisherRepository.Add(publisher);

            return Ok(_mapper.Map<PublisherDTO>(savedPublisher));
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

            Publisher savedPublisher = await _publisherRepository.Add(publisher);

            return Ok(_mapper.Map<PublisherDTO>(savedPublisher));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteById(int id)
        {
            int result = await _publisherRepository.Delete(id);
            return Ok(result);
        }
    }
}
