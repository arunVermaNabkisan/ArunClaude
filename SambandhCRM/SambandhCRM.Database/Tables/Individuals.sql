CREATE TABLE [dbo].[Individuals]
(
    [IndividualId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [FullName] NVARCHAR(255) NOT NULL,
    [MobileNumber] NVARCHAR(15) NOT NULL,
    [Email] NVARCHAR(255) NULL,
    [PANNumber] NVARCHAR(10) NULL,
    [DINNumber] NVARCHAR(8) NULL, -- Director Identification Number
    [LinkedInProfile] NVARCHAR(255) NULL,
    [AlternatePhone] NVARCHAR(15) NULL,
    [Address] NVARCHAR(500) NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [CreatedBy] INT NULL,
    [ModifiedDate] DATETIME NULL,
    [ModifiedBy] INT NULL,
    CONSTRAINT UQ_Individuals_Mobile UNIQUE ([MobileNumber])
);

GO

CREATE INDEX IX_Individuals_FullName ON [dbo].[Individuals](FullName);
GO;
CREATE INDEX IX_Individuals_MobileNumber ON [dbo].[Individuals](MobileNumber);
GO;
CREATE INDEX IX_Individuals_Email ON [dbo].[Individuals](Email);
GO;
CREATE INDEX IX_Individuals_PANNumber ON [dbo].[Individuals](PANNumber);
GO;
