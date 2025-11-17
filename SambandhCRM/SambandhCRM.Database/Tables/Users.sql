CREATE TABLE [dbo].[Users]
(
    [UserId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserName] NVARCHAR(100) NOT NULL UNIQUE,
    [Email] NVARCHAR(255) NOT NULL UNIQUE,
    [PasswordHash] NVARCHAR(255) NOT NULL,
    [FirstName] NVARCHAR(100) NOT NULL,
    [LastName] NVARCHAR(100) NOT NULL,
    [MobileNumber] NVARCHAR(15) NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [LastLoginDate] DATETIME NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [CreatedBy] INT NULL,
    [ModifiedDate] DATETIME NULL,
    [ModifiedBy] INT NULL,
    [PasswordResetToken] NVARCHAR(255) NULL,
    [PasswordResetExpiry] DATETIME NULL
);

GO

CREATE INDEX IX_Users_Email ON [dbo].[Users](Email);
GO;
CREATE INDEX IX_Users_UserName ON [dbo].[Users](UserName);
GO;
CREATE INDEX IX_Users_IsActive ON [dbo].[Users](IsActive);
GO;