CREATE TABLE [dbo].[MasterData]
(
    [MasterDataId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Category] NVARCHAR(50) NOT NULL, -- BusinessSegment, ProductCategory, LeadSource, CommunicationType, DocumentType, RejectionReason, etc.
    [Value] NVARCHAR(255) NOT NULL,
    [DisplayOrder] INT NOT NULL DEFAULT 0,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsSystemDefined] BIT NOT NULL DEFAULT 0, -- Cannot be deleted if true
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [CreatedBy] INT NULL,
    [ModifiedDate] DATETIME NULL,
    [ModifiedBy] INT NULL,
    CONSTRAINT UQ_MasterData_Category_Value UNIQUE ([Category], [Value])
);

GO

CREATE INDEX IX_MasterData_Category ON [dbo].[MasterData](Category);
GO;
CREATE INDEX IX_MasterData_IsActive ON [dbo].[MasterData](IsActive);
GO;

-- Insert default values
--INSERT INTO [dbo].[MasterData] ([Category], [Value], [DisplayOrder], [IsSystemDefined])
--VALUES
--    -- Legal Constitutions
--    ('LegalConstitution', 'Company', 1, 1),
--    ('LegalConstitution', 'Society', 2, 1),
--    ('LegalConstitution', 'Trust/NGO', 3, 1),
--    ('LegalConstitution', 'Partnership/LLP', 4, 1),
--    ('LegalConstitution', 'Individual/Proprietor', 5, 1),

--    -- Business Segments
--    ('BusinessSegment', 'FPO', 1, 1),
--    ('BusinessSegment', 'Agri-Startup', 2, 1),
--    ('BusinessSegment', 'MFI', 3, 1),
--    ('BusinessSegment', 'NBFC', 4, 1),
--    ('BusinessSegment', 'HFC', 5, 1),
--    ('BusinessSegment', 'CSR Partner', 6, 1),
--    ('BusinessSegment', 'PACS', 7, 1),
--    ('BusinessSegment', 'Others', 8, 1),

--    -- Lead Sources
--    ('LeadSource', 'Direct Walk-in', 1, 1),
--    ('LeadSource', 'Website', 2, 1),
--    ('LeadSource', 'Referral - Customer', 3, 1),
--    ('LeadSource', 'Referral - Employee', 4, 1),
--    ('LeadSource', 'Campaign', 5, 1),
--    ('LeadSource', 'Others', 6, 1),

--    -- Product Interests
--    ('ProductInterest', 'Term Loan', 1, 1),
--    ('ProductInterest', 'Working Capital', 2, 1),
--    ('ProductInterest', 'Guarantee', 3, 1),
--    ('ProductInterest', 'Others', 4, 1),

--    -- Communication Types
--    ('CommunicationType', 'Phone Call', 1, 1),
--    ('CommunicationType', 'Email', 2, 1),
--    ('CommunicationType', 'Meeting', 3, 1),
--    ('CommunicationType', 'Site Visit', 4, 1),
--    ('CommunicationType', 'WhatsApp', 5, 1),
--    ('CommunicationType', 'SMS', 6, 1),

--    -- Document Types
--    ('DocumentType', 'KYC Documents', 1, 1),
--    ('DocumentType', 'Financial Statements', 2, 1),
--    ('DocumentType', 'Business Documents', 3, 1),
--    ('DocumentType', 'Contract', 4, 1),
--    ('DocumentType', 'Others', 5, 1),

--    -- Roles in Organization
--    ('RoleInOrganization', 'Chairman', 1, 1),
--    ('RoleInOrganization', 'CEO/MD', 2, 1),
--    ('RoleInOrganization', 'Director', 3, 1),
--    ('RoleInOrganization', 'CFO', 4, 1),
--    ('RoleInOrganization', 'Authorized Signatory', 5, 1),
--    ('RoleInOrganization', 'Primary Contact', 6, 1),
--    ('RoleInOrganization', 'Other', 7, 1);

--GO;
