CREATE TABLE [dbo].[IndividualOrganizations]
(
    [IndividualOrganizationId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [IndividualId] INT NOT NULL,
    [CustomerId] INT NOT NULL,
    [RoleInOrganization] NVARCHAR(100) NOT NULL, -- Chairman, CEO/MD, Director, CFO, Authorized Signatory, Primary Contact, Other
    [RoleSpecification] NVARCHAR(255) NULL, -- If Role is "Other"
    [RoleStartDate] DATE NULL,
    [IsStillActive] BIT NOT NULL DEFAULT 1,
    [RoleEndDate] DATE NULL,
    [IsDecisionMaker] BIT NOT NULL DEFAULT 0,
    [IsPreferredContact] BIT NOT NULL DEFAULT 0,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [CreatedBy] INT NULL,
    [ModifiedDate] DATETIME NULL,
    [ModifiedBy] INT NULL,
    CONSTRAINT FK_IndividualOrganizations_Individual FOREIGN KEY ([IndividualId]) REFERENCES [dbo].[Individuals]([IndividualId]),
    CONSTRAINT FK_IndividualOrganizations_Customer FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers]([CustomerId])
);

GO

CREATE INDEX IX_IndividualOrganizations_IndividualId ON [dbo].[IndividualOrganizations](IndividualId);
GO;
CREATE INDEX IX_IndividualOrganizations_CustomerId ON [dbo].[IndividualOrganizations](CustomerId);
GO;
CREATE INDEX IX_IndividualOrganizations_IsStillActive ON [dbo].[IndividualOrganizations](IsStillActive);
GO;
