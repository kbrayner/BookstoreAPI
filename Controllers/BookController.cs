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
                books = await _bookRepository.ListByTitle(title);
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
            Book book = _mapper.Map<Book>(creatBookDTO);

            try
            {
                Book savedBook = await _bookRepository.Add(book);
                return Ok(_mapper.Map<BookDTO>(savedBook));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException is PostgresException pgEx)
                {
                    if (pgEx.SqlState == "23505") // Unique violation code
                    {
                        return BadRequest("Could not create the Book because it already exists a book with this title");
                    }
                }
                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create a Book: {ex.Message}");
                throw new Exception("Could not create the Book. Please try it later.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDTO>> Update([FromBody] BookDTO bookDTO, int id)
        {
            bookDTO.Id = id;
            Book book = _mapper.Map<Book>(bookDTO);

            bool foundId = _bookRepository.IdExists(id);

            if (!foundId)
            {
                return NotFound();
            }

            try
            {
                Book savedBook = await _bookRepository.Update(book, id);
                return Ok(_mapper.Map<BookDTO>(savedBook));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException is PostgresException pgEx)
                {
                    if (pgEx.SqlState == "23505") // Unique violation code
                    {
                        return BadRequest("Could not udpate the Book because it already exists a book with this title");
                    }
                }
                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not update a Book: {ex.Message}");
                throw new Exception("Could not update the Book. Please try it later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteById(int id)
        {
            int result = await _bookRepository.Delete(id);
            return Ok(result);
        }
    }
}
