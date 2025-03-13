using Microsoft.AspNetCore.Mvc;
using TrackMyMedia.Server.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyMedia.Shared.Models;
using Microsoft.AspNetCore.Authorization;

namespace TrackMyMedia.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService BookService;

        public BookController(IBookService BookService)
        {
            this.BookService = BookService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookModel Book)
        {
            if (Book == null)
            {
                return BadRequest("Book data is required.");
            }

            var createdBook = await BookService.CreateBookAsync(Book);
            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.MediaId }, createdBook);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var Books = await BookService.GetBooksAsync();
            return Ok(Books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var Book = await BookService.GetBookByIdAsync(id);

            if (Book == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            return Ok(Book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookModel updatedBook)
        {
            var Book = await BookService.UpdateBookAsync(id, updatedBook);

            if (Book == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            return Ok(Book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await BookService.DeleteBookAsync(id);

            if (!result)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            return NoContent(); 
        }
    }
}
