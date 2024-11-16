select * from [dbo].[Users]

select * from BookExchangeBook.dbo.Books

SELECT UserId, Email, PasswordHash, PasswordSalt, FirstName, LastName, CreatedDate, LastLoginDate
    FROM [dbo].[Users]
    WHERE Email = 'md@test.com'


	exec [dbo].[GetUser] @email = 'md@test.com'


	select * from PasswordResets
	SELECT u.*
    FROM Users u
    INNER JOIN PasswordResets pr ON u.UserId = pr.UserId
    WHERE pr.ResetToken = 'A3b5BBLTSSfmkLdPEHA8w4bovOvkR/PThJyB/eRM5lg=' AND pr.IsUsed = 0 AND pr.ExpirationDate > GETDATE();