namespace Application.Domain.Entities.Book
{
    public interface IBookRepository
    {
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int bookId);
        Task<Book?> GetBookByIdAsync(int bookId);
        Task<IEnumerable<Book>> GetBooksByUserIdAsync(int userId);
        Task<IEnumerable<Book>> SearchBooksAsync(int userId, string? searchTerm, string? genre = null, string? condition = null, string? availabilityStatus = null, string? language = null, int page = 1, int pageSize = 10);
    }

}
