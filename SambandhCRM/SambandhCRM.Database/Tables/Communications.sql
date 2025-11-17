CREATE TABLE [dbo].[Communications]
(
    [CommunicationId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CustomerId] INT NULL,
    [LeadId] INT NULL,
    [CommunicationDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [CommunicationType] NVARCHAR(50) NOT NULL, -- Phone Call, Email, Meeting, Site Visit, WhatsApp, SMS
    [Direction] NVARCHAR(20) NOT NULL, -- Inbound, Outbound
    [Subject] NVARCHAR(255) NULL,
    [Summary] NVARCHAR(MAX) NULL,
    [NextActionRequired] NVARCHAR(500) NULL,
    [NextActionDate] DATETIME NULL,

    -- For bulk communications
    [IsBulkCommunication] BIT NOT NULL DEFAULT 0,
    [CampaignName] NVARCHAR(255) NULL,
    [TemplateUsed] NVARCHAR(100) NULL,
    [DeliveryStatus] NVARCHAR(50) NULL, -- Sent, Delivered, Failed, Opened, Clicked

    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [CreatedBy] INT NULL,
    [ModifiedDate] DATETIME NULL,
    [ModifiedBy] INT NULL,

    CONSTRAINT FK_Communications_Customer FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers]([CustomerId]),
    CONSTRAINT FK_Communications_Lead FOREIGN KEY ([LeadId]) REFERENCES [dbo].[Leads]([LeadId]),
    CONSTRAINT FK_Communications_User FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Users]([UserId])
);

GO

CREATE INDEX IX_Communications_CustomerId ON [dbo].[Communications](CustomerId);
CREATE INDEX IX_Communications_LeadId ON [dbo].[Communications](LeadId);
CREATE INDEX IX_Communications_CommunicationDate ON [dbo].[Communications](CommunicationDate);
CREATE INDEX IX_Communications_CommunicationType ON [dbo].[Communications](CommunicationType);
CREATE INDEX IX_Communications_CreatedBy ON [dbo].[Communications](CreatedBy);
CREATE INDEX IX_Communications_NextActionDate ON [dbo].[Communications](NextActionDate);
