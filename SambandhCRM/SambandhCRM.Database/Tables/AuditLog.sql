CREATE TABLE [dbo].[AuditLog]
(
    [AuditLogId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [TableName] NVARCHAR(100) NOT NULL,
    [RecordId] INT NOT NULL,
    [Action] NVARCHAR(20) NOT NULL, -- INSERT, UPDATE, DELETE
    [FieldName] NVARCHAR(100) NULL,
    [OldValue] NVARCHAR(MAX) NULL,
    [NewValue] NVARCHAR(MAX) NULL,
    [ChangeDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [ChangedBy] INT NULL,
    [IPAddress] NVARCHAR(50) NULL,
    [UserAgent] NVARCHAR(500) NULL,
    CONSTRAINT FK_AuditLog_User FOREIGN KEY ([ChangedBy]) REFERENCES [dbo].[Users]([UserId])
);

GO

CREATE INDEX IX_AuditLog_TableName ON [dbo].[AuditLog](TableName);
CREATE INDEX IX_AuditLog_RecordId ON [dbo].[AuditLog](RecordId);
CREATE INDEX IX_AuditLog_ChangeDate ON [dbo].[AuditLog](ChangeDate);
CREATE INDEX IX_AuditLog_ChangedBy ON [dbo].[AuditLog](ChangedBy);
CREATE INDEX IX_AuditLog_Action ON [dbo].[AuditLog](Action);
