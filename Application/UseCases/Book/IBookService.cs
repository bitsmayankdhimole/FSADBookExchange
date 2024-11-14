using BookEntity = Application.Domain.Entities.Book;

namespace Application.UseCases.Book
{
    public interface IBookService
    {
        Task AddBookAsync(BookEntity.Book book);
        Task UpdateBookAsync(BookEntity.Book book);
        Task DeleteBookAsync(int bookId);
        Task<BookEntity.Book?> GetBookByIdAsync(int bookId);
        Task<IEnumerable<BookEntity.Book>> GetBooksByUserIdAsync(int userId);
        Task<IEnumerable<BookEntity.Book>> SearchBooksAsync(int userId, string? searchTerm, string? genre = null, string? condition = null, string? availabilityStatus = null, string? language = null, int page = 1, int pageSize = 10);
    }
}
