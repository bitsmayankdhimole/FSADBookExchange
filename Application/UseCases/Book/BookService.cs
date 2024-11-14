using BookEntity = Application.Domain.Entities.Book;

namespace Application.UseCases.Book
{
    public class BookService : IBookService
    {
        private readonly BookEntity.IBookRepository _bookRepository;

        public BookService(BookEntity.IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task AddBookAsync(BookEntity.Book book)
        {
            await _bookRepository.AddBookAsync(book);
        }

        public async Task UpdateBookAsync(BookEntity.Book book)
        {
            await _bookRepository.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(int bookId)
        {
            await _bookRepository.DeleteBookAsync(bookId);
        }

        public async Task<BookEntity.Book?> GetBookByIdAsync(int bookId)
        {
            return await _bookRepository.GetBookByIdAsync(bookId);
        }

        public async Task<IEnumerable<BookEntity.Book>> GetBooksByUserIdAsync(int userId)
        {
            return await _bookRepository.GetBooksByUserIdAsync(userId);
        }

        public async Task<IEnumerable<BookEntity.Book>> SearchBooksAsync(int userId, string? searchTerm, string? genre = null, string? condition = null, string? availabilityStatus = null, string? language = null, int page = 1, int pageSize = 10)
        {
            return await _bookRepository.SearchBooksAsync(userId, searchTerm, genre, condition, availabilityStatus, language, page, pageSize);
        }
    }
}
