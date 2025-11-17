CREATE TABLE [dbo].[Documents]
(
    [DocumentId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CustomerId] INT NULL,
    [LeadId] INT NULL,
    [DocumentType] NVARCHAR(100) NOT NULL, -- KYC, Financial Statement, Business Document, Contract, Others
    [DocumentName] NVARCHAR(255) NOT NULL,
    [DocumentDescription] NVARCHAR(500) NULL,
    [FileName] NVARCHAR(255) NOT NULL,
    [FileSize] BIGINT NULL, -- in bytes
    [FileExtension] NVARCHAR(10) NULL,
    [FilePath] NVARCHAR(500) NOT NULL, -- Physical or cloud storage path
    [UploadDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [UploadedBy] INT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [CreatedBy] INT NULL,
    CONSTRAINT FK_Documents_Customer FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers]([CustomerId]),
    CONSTRAINT FK_Documents_Lead FOREIGN KEY ([LeadId]) REFERENCES [dbo].[Leads]([LeadId]),
    CONSTRAINT FK_Documents_User FOREIGN KEY ([UploadedBy]) REFERENCES [dbo].[Users]([UserId])
);

GO

CREATE INDEX IX_Documents_CustomerId ON [dbo].[Documents](CustomerId);
CREATE INDEX IX_Documents_LeadId ON [dbo].[Documents](LeadId);
CREATE INDEX IX_Documents_DocumentType ON [dbo].[Documents](DocumentType);
CREATE INDEX IX_Documents_UploadDate ON [dbo].[Documents](UploadDate);
