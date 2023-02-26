using AutoMapper;
using BookstoreSystem.DTOs;
using BookstoreSystem.Models;
using BookstoreSystem.Repositories;
using BookstoreSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> GetBooks(string? title)
        {
            List<Book> books;
            if(title == null)
            {
                books = await _bookRepository.GetAll();
            }
            else
            {
                books = await _bookRepository.GetByTitle(title);
            }
            
            return Ok(_mapper.Map<IEnumerable<BookDTO>>(books));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetById(int id)
        {
            Book book =  await _bookRepository.GetById(id);
            return Ok(_mapper.Map<BookDTO>(book));
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> Add([FromBody] CreateBookDTO creatBookDTO)
        {
            if (_bookRepository.BookTitleExists(creatBookDTO.Title))
            {
                return BadRequest("You cannot use this title. It already exists.");
            }
            Book book = _mapper.Map<Book>(creatBookDTO);

            Book savedBook = await _bookRepository.Add(book);

            return Ok(_mapper.Map<BookDTO>(savedBook));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDTO>> Update([FromBody] BookDTO bookDTO, int id)
        {
            if (_bookRepository.BookTitleExists(bookDTO.Title))
            {
                return BadRequest("You cannot use this title. It already exists.");
            }
            bookDTO.Id = id;
            Book book = _mapper.Map<Book>(bookDTO);

            bool foundId = _bookRepository.IdExists(id);

            if (!foundId)
            {
                return NotFound();
            }

            Book savedBook = await _bookRepository.Update(book, id);

            return Ok(_mapper.Map<BookDTO>(savedBook));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteById(int id)
        {
            int result = await _bookRepository.Delete(id);
            return Ok(result);
        }
    }
}
