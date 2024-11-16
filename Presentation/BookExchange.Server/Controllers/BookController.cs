using Application.Domain.Entities.Book;
using Application.UseCases.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookExchange.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            await _bookService.AddBookAsync(book);
            return Ok();
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook(int bookId, [FromBody] Book book)
        {
            book.BookId = bookId;
            await _bookService.UpdateBookAsync(book);
            return Ok();
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            await _bookService.DeleteBookAsync(bookId);
            return Ok();
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBookById(int bookId)
        {
            var book = await _bookService.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBooksByUserId(int userId)
        {
            var books = await _bookService.GetBooksByUserIdAsync(userId);
            return Ok(books);
        }

        [HttpGet("search/{userId}")]
        public async Task<IActionResult> SearchBooks(int userId, [FromQuery] string? searchTerm, [FromQuery] string? genre, [FromQuery] string? condition, [FromQuery] string? availabilityStatus, [FromQuery] string? language, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var books = await _bookService.SearchBooksAsync(userId, searchTerm, genre, condition, availabilityStatus, language, page, pageSize);
            return Ok(books);
        }
    }
}
