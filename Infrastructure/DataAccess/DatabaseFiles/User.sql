CREATE TABLE [dbo].[Users] (
    [UserId] INT IDENTITY(1,1) PRIMARY KEY,
    [Email] NVARCHAR(256) NOT NULL UNIQUE,
    [PasswordHash] NVARCHAR(256) NOT NULL,
    [PasswordSalt] NVARCHAR(256) NOT NULL,
    [FirstName] NVARCHAR(100) NULL,
    [LastName] NVARCHAR(100) NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [LastLoginDate] DATETIME NULL
);

CREATE TABLE [dbo].[PasswordResets] (
    [ResetId] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [ResetToken] NVARCHAR(256) NOT NULL,
    [ExpirationDate] DATETIME NOT NULL,
    [IsUsed] BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (UserId) REFERENCES [dbo].[Users](UserId)
);

CREATE TABLE [dbo].[Sessions] (
    [SessionId] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [SessionToken] NVARCHAR(256) NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [ExpirationDate] DATETIME NOT NULL,
    FOREIGN KEY (UserId) REFERENCES [dbo].[Users](UserId)
);

-- Index for better performance on Email
CREATE INDEX IX_Users_Email ON [dbo].[Users]([Email]);

go
-- User Registration
CREATE PROCEDURE [dbo].[RegisterUser]
    @Email NVARCHAR(256),
    @PasswordHash NVARCHAR(256),
    @PasswordSalt NVARCHAR(256),
    @FirstName NVARCHAR(100) = NULL,
    @LastName NVARCHAR(100) = NULL
AS
BEGIN
    INSERT INTO [dbo].[Users] (Email, PasswordHash, PasswordSalt, FirstName, LastName, CreatedDate)
    VALUES (@Email, @PasswordHash, @PasswordSalt, @FirstName, @LastName, GETDATE());
END
GO

-- Create Session
CREATE PROCEDURE [dbo].[CreateSession]
    @UserId INT,
    @SessionToken NVARCHAR(256),
    @ExpirationDate DATETIME
AS
BEGIN
    INSERT INTO [dbo].[Sessions] (UserId, SessionToken, CreatedDate, ExpirationDate)
    VALUES (@UserId, @SessionToken, GETDATE(), @ExpirationDate);
END
GO

-- Get User
CREATE PROCEDURE [dbo].[GetUser]
    @Email NVARCHAR(256)
AS
BEGIN
    SELECT UserId, Email, FirstName, LastName, CreatedDate, LastLoginDate
    FROM [dbo].[Users]
    WHERE Email = @Email;
END
GO

-- Create Password Reset Token
CREATE PROCEDURE [dbo].[CreatePasswordResetToken]
    @UserId INT,
    @ResetToken NVARCHAR(256),
    @ExpirationDate DATETIME
AS
BEGIN
    INSERT INTO [dbo].[PasswordResets] (UserId, ResetToken, ExpirationDate, IsUsed)
    VALUES (@UserId, @ResetToken, @ExpirationDate, 0);
END
GO

-- Reset Password
CREATE PROCEDURE [dbo].[ResetPassword]
    @ResetToken NVARCHAR(256),
    @NewPasswordHash NVARCHAR(256),
    @NewPasswordSalt NVARCHAR(256)
AS
BEGIN
    DECLARE @UserId INT;

    -- Get the UserId associated with the reset token
    SELECT @UserId = UserId
    FROM [dbo].[PasswordResets]
    WHERE ResetToken = @ResetToken AND IsUsed = 0 AND ExpirationDate > GETDATE();

    IF @UserId IS NOT NULL
    BEGIN
        -- Update the user's password
        UPDATE [dbo].[Users]
        SET PasswordHash = @NewPasswordHash, PasswordSalt = @NewPasswordSalt
        WHERE UserId = @UserId;

        -- Mark the reset token as used
        UPDATE [dbo].[PasswordResets]
        SET IsUsed = 1
        WHERE ResetToken = @ResetToken;
    END
END
GO
