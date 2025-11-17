CREATE TABLE [dbo].[CustomerContacts]
(
    [ContactId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CustomerId] INT NOT NULL,
    [ContactPerson] NVARCHAR(255) NOT NULL,
    [Designation] NVARCHAR(100) NULL,
    [MobileNumber] NVARCHAR(15) NULL,
    [Email] NVARCHAR(255) NULL,
    [IsPrimaryContact] BIT NOT NULL DEFAULT 0,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [CreatedBy] INT NULL,
    [ModifiedDate] DATETIME NULL,
    [ModifiedBy] INT NULL,
    CONSTRAINT FK_CustomerContacts_Customer FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers]([CustomerId])
);

GO

CREATE INDEX IX_CustomerContacts_CustomerId ON [dbo].[CustomerContacts](CustomerId);
GO;
CREATE INDEX IX_CustomerContacts_MobileNumber ON [dbo].[CustomerContacts](MobileNumber);
GO;
