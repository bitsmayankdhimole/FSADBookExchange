namespace Application.Domain.Entities.Book
{
    public class Book
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Condition { get; set; } = string.Empty; // New, Like New, Very Good, Good, Fair, Poor
        public string AvailabilityStatus { get; set; } = string.Empty; // Available, Unavailable
        public string Language { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
