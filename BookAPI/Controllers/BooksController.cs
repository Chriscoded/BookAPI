using BookAPI.Models;
using BookAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository booksRepository;

        public BooksController(IBookRepository booksRepository)
        {
            this.booksRepository = booksRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await booksRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<Book> GetBooks(int id)
        {
            return await booksRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromBody] Book book)
        {
            var newBook = booksRepository.Create(book);
            return CreatedAtAction(nameof(GetBooks), new { id = newBook.Id });
        }

        [HttpPut]
        public async Task<ActionResult<Book>> PutBook(int id, [FromBody] Book books)
        {
            if(id != books.Id)
            {
                return BadRequest();
            }

            await booksRepository.Update(books);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var bookToDelete = await booksRepository.Get(id);
            if (bookToDelete == null)
            {
                return NotFound();
            }

            await booksRepository.Delete(bookToDelete.Id);
            return NoContent();
        }
    }
}
