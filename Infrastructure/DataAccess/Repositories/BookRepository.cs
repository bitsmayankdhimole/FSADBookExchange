using Application.Domain.Entities.Book;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.DataAccess.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly string _connectionString;

        public BookRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BookConnection");
        }

        public async Task AddBookAsync(Book book)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "AddBook",
                new
                {
                    book.UserId,
                    book.Title,
                    book.Author,
                    book.Genre,
                    book.Condition,
                    book.AvailabilityStatus,
                    book.Language,
                    book.ImageURL,
                    book.Description
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateBookAsync(Book book)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "UpdateBook",
                new
                {
                    book.BookId,
                    book.UserId,
                    book.Title,
                    book.Author,
                    book.Genre,
                    book.Condition,
                    book.AvailabilityStatus,
                    book.Language,
                    book.ImageURL,
                    book.Description
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteBookAsync(int bookId)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "DeleteBook",
                new { BookId = bookId },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<Book?> GetBookByIdAsync(int bookId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<Book>(
                "GetBookById",
                new { BookId = bookId },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Book>> GetBooksByUserIdAsync(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Book>(
                "GetBooksByUserId",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(int userId, string? searchTerm, string? genre = null, string? condition = null, string? availabilityStatus = null, string? language = null, int page = 1, int pageSize = 10)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Book>(
                "SearchBooks",
                new
                {
                    SearchTerm = searchTerm ?? string.Empty,
                    Genre = genre ?? (object)DBNull.Value,
                    Condition = condition ?? (object)DBNull.Value,
                    AvailabilityStatus = availabilityStatus ?? (object)DBNull.Value,
                    Language = language ?? (object)DBNull.Value,
                    UserId = userId,
                    Page = page,
                    PageSize = pageSize
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}
