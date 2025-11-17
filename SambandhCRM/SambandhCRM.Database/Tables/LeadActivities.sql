CREATE TABLE [dbo].[LeadActivities]
(
    [LeadActivityId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [LeadId] INT NOT NULL,
    [ActivityDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [ActivityType] NVARCHAR(50) NOT NULL, -- Status Change, Note Added, Document Uploaded, Follow-up Scheduled, Assignment Changed
    [OldValue] NVARCHAR(500) NULL,
    [NewValue] NVARCHAR(500) NULL,
    [Notes] NVARCHAR(MAX) NULL,
    [CreatedBy] INT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_LeadActivities_Lead FOREIGN KEY ([LeadId]) REFERENCES [dbo].[Leads]([LeadId]),
    CONSTRAINT FK_LeadActivities_User FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users]([UserId])
);

GO

CREATE INDEX IX_LeadActivities_LeadId ON [dbo].[LeadActivities](LeadId);
CREATE INDEX IX_LeadActivities_ActivityDate ON [dbo].[LeadActivities](ActivityDate);
CREATE INDEX IX_LeadActivities_ActivityType ON [dbo].[LeadActivities](ActivityType);
