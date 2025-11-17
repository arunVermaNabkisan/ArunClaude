CREATE TABLE [dbo].[Leads]
(
    [LeadId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [LeadCode] NVARCHAR(20) NOT NULL UNIQUE, -- Format: LEAD-YYYYMM-9999
    [CustomerId] INT NOT NULL,
    [LeadSource] NVARCHAR(50) NOT NULL, -- Direct Walk-in, Website, Referral - Customer, Referral - Employee, Campaign, Others
    [LeadSourceDetails] NVARCHAR(255) NULL,
    [ProductInterest] NVARCHAR(100) NOT NULL, -- Term Loan, Working Capital, Guarantee, Others
    [LoanAmountMin] DECIMAL(18,2) NULL,
    [LoanAmountMax] DECIMAL(18,2) NULL,
    [Priority] NVARCHAR(20) NOT NULL DEFAULT 'Medium', -- High, Medium, Low
    [LeadStatus] NVARCHAR(50) NOT NULL DEFAULT 'New', -- New, In-Progress, Documentation, Submitted to Credit, Dropped, Converted
    [DropReason] NVARCHAR(500) NULL,

    -- Assignment
    [AssignedToUserId] INT NULL,
    [AssignedDate] DATETIME NULL,
    [AssignedBy] INT NULL,

    -- Follow-up
    [LastContactDate] DATETIME NULL,
    [NextFollowUpDate] DATETIME NULL,
    [Notes] NVARCHAR(MAX) NULL,

    -- Document Checklist
    [HasKYCDocuments] BIT NULL,
    [HasFinancialStatements] BIT NULL,
    [HasBusinessDocuments] BIT NULL,
    [HasOtherDocuments] BIT NULL,

    -- Conversion
    [ConvertedDate] DATETIME NULL,
    [ConvertedBy] INT NULL,

    -- Audit
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [CreatedBy] INT NULL,
    [ModifiedDate] DATETIME NULL,
    [ModifiedBy] INT NULL,

    CONSTRAINT FK_Leads_Customer FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers]([CustomerId]),
    CONSTRAINT FK_Leads_AssignedUser FOREIGN KEY ([AssignedToUserId]) REFERENCES [dbo].[Users]([UserId])
);

GO

CREATE INDEX IX_Leads_LeadCode ON [dbo].[Leads](LeadCode);
CREATE INDEX IX_Leads_CustomerId ON [dbo].[Leads](CustomerId);
CREATE INDEX IX_Leads_LeadStatus ON [dbo].[Leads](LeadStatus);
CREATE INDEX IX_Leads_AssignedToUserId ON [dbo].[Leads](AssignedToUserId);
CREATE INDEX IX_Leads_NextFollowUpDate ON [dbo].[Leads](NextFollowUpDate);
CREATE INDEX IX_Leads_CreatedDate ON [dbo].[Leads](CreatedDate);
CREATE INDEX IX_Leads_Priority ON [dbo].[Leads](Priority);
