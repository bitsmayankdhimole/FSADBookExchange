Create Database BookExchangeBook

GO

Use BookExchangeBook

GO

CREATE TABLE Books (
    BookId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    Author NVARCHAR(255) NOT NULL,
    Genre NVARCHAR(100) NOT NULL,
    Condition NVARCHAR(50) NOT NULL, -- New, Like New, Very Good, Good, Fair, Poor
    AvailabilityStatus NVARCHAR(50) NOT NULL, -- Available, Unavailable
    Language NVARCHAR(50) NOT NULL,
    ImageURL NVARCHAR(500),
    Description NVARCHAR(MAX),
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedDate DATETIME NOT NULL DEFAULT GETDATE()
);

GO

CREATE PROCEDURE AddBook
    @UserId INT,
    @Title NVARCHAR(255),
    @Author NVARCHAR(255),
    @Genre NVARCHAR(100),
    @Condition NVARCHAR(50),
    @AvailabilityStatus NVARCHAR(50),
    @Language NVARCHAR(50),
    @ImageURL NVARCHAR(500),
    @Description NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Books (UserId, Title, Author, Genre, Condition, AvailabilityStatus, Language, ImageURL, Description, CreatedDate, UpdatedDate)
    VALUES (@UserId, @Title, @Author, @Genre, @Condition, @AvailabilityStatus, @Language, @ImageURL, @Description, GETDATE(), GETDATE());
END;

GO

CREATE PROCEDURE UpdateBook
    @BookId INT,
    @UserId INT,
    @Title NVARCHAR(255),
    @Author NVARCHAR(255),
    @Genre NVARCHAR(100),
    @Condition NVARCHAR(50),
    @AvailabilityStatus NVARCHAR(50),
    @Language NVARCHAR(50),
    @ImageURL NVARCHAR(500),
    @Description NVARCHAR(MAX)
AS
BEGIN
    UPDATE Books
    SET UserId = @UserId,
        Title = @Title,
        Author = @Author,
        Genre = @Genre,
        Condition = @Condition,
        AvailabilityStatus = @AvailabilityStatus,
        Language = @Language,
        ImageURL = @ImageURL,
        Description = @Description,
        UpdatedDate = GETDATE()
    WHERE BookId = @BookId;
END;

GO

CREATE PROCEDURE DeleteBook
    @BookId INT
AS
BEGIN
    DELETE FROM Books
    WHERE BookId = @BookId;
END;

GO

CREATE PROCEDURE GetBookById
    @BookId INT
AS
BEGIN
    SELECT * FROM Books
    WHERE BookId = @BookId;
END;

GO

CREATE PROCEDURE GetBooksByUserId
    @UserId INT
AS
BEGIN
    SELECT * FROM Books
    WHERE UserId = @UserId;
END;

GO

CREATE PROCEDURE SearchBooks
    @SearchTerm NVARCHAR(255),
    @Genre NVARCHAR(100) = NULL,
    @Condition NVARCHAR(50) = NULL,
    @AvailabilityStatus NVARCHAR(50) = NULL,
    @Language NVARCHAR(50) = NULL,
    @UserId INT,
    @Page INT,
    @PageSize INT
AS
BEGIN
    SELECT * FROM Books
    WHERE (@SearchTerm = '' OR Title LIKE '%' + @SearchTerm + '%' OR Author LIKE '%' + @SearchTerm + '%' OR Description LIKE '%' + @SearchTerm + '%')
      AND (@Genre IS NULL OR Genre = @Genre)
      AND (@Condition IS NULL OR Condition = @Condition)
      AND (@AvailabilityStatus IS NULL OR AvailabilityStatus = @AvailabilityStatus)
      AND (@Language IS NULL OR Language = @Language)
      AND UserId <> @UserId
    ORDER BY CreatedDate
    OFFSET (@Page - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END;





