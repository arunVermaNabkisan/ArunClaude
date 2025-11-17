CREATE TABLE [dbo].[Customers]
(
    [CustomerId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CustomerCode] NVARCHAR(20) NOT NULL UNIQUE,

    -- Basic Information
    [LegalConstitution] NVARCHAR(50) NOT NULL, -- Company, Society, Trust/NGO, Partnership/LLP, Individual/Proprietor
    [EntityName] NVARCHAR(255) NOT NULL,
    [RegistrationNumber] NVARCHAR(100) NULL, -- CIN/Society Reg/Trust Reg
    [PANNumber] NVARCHAR(10) NULL,
    [AadhaarNumber] NVARCHAR(12) NULL, -- For individuals, masked
    [DateOfIncorporation] DATE NULL,
    [DateOfBirth] DATE NULL, -- For individuals
    [BusinessSegment] NVARCHAR(100) NULL,
    [PrimaryBusinessActivity] NVARCHAR(500) NULL,
    [Occupation] NVARCHAR(100) NULL, -- For individuals

    -- Financial Information
    [AnnualTurnover] DECIMAL(18,2) NULL,
    [AnnualIncome] DECIMAL(18,2) NULL, -- For individuals
    [EmployeeCountRange] NVARCHAR(50) NULL,

    -- Contact Information
    [RegisteredAddress] NVARCHAR(500) NULL,
    [RegisteredPinCode] NVARCHAR(6) NULL,
    [OfficeAddress] NVARCHAR(500) NULL,
    [OfficePinCode] NVARCHAR(6) NULL,
    [CorrespondenceAddress] NVARCHAR(500) NULL,
    [CorrespondencePinCode] NVARCHAR(6) NULL,
    [OfficePhone] NVARCHAR(15) NULL,
    [MobileNumber] NVARCHAR(15) NULL,
    [AlternateNumber] NVARCHAR(15) NULL,
    [PrimaryEmail] NVARCHAR(255) NULL,
    [SecondaryEmail] NVARCHAR(255) NULL,
    [Website] NVARCHAR(255) NULL,
    [LinkedInProfile] NVARCHAR(255) NULL,
    [TwitterHandle] NVARCHAR(100) NULL,

    -- Banking Information
    [PrimaryBankName] NVARCHAR(255) NULL,
    [BankingSinceYear] INT NULL,
    [IsExistingCustomer] BIT NOT NULL DEFAULT 0,
    [ExistingProductType] NVARCHAR(100) NULL,
    [OutstandingAmount] DECIMAL(18,2) NULL,
    [OtherLenderRelationships] NVARCHAR(500) NULL,

    -- Status
    [CustomerStatus] NVARCHAR(50) NOT NULL DEFAULT 'Prospect', -- Prospect, Active Lead, Customer, Dormant, Archived

    -- Assignment
    [AssignedToUserId] INT NULL,
    [AssignedDate] DATETIME NULL,

    -- MCA Integration
    [IsMCAVerified] BIT NOT NULL DEFAULT 0,
    [MCAFetchDate] DATETIME NULL,
    [MCAData] NVARCHAR(MAX) NULL, -- JSON data from MCA

    -- Audit
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [CreatedBy] INT NULL,
    [ModifiedDate] DATETIME NULL,
    [ModifiedBy] INT NULL,

    CONSTRAINT FK_Customers_AssignedUser FOREIGN KEY ([AssignedToUserId]) REFERENCES [dbo].[Users]([UserId])
);

GO

CREATE INDEX IX_Customers_CustomerCode ON [dbo].[Customers](CustomerCode);
GO;
CREATE INDEX IX_Customers_PANNumber ON [dbo].[Customers](PANNumber);
GO;
CREATE INDEX IX_Customers_EntityName ON [dbo].[Customers](EntityName);
GO;
CREATE INDEX IX_Customers_LegalConstitution ON [dbo].[Customers](LegalConstitution);
GO;
CREATE INDEX IX_Customers_BusinessSegment ON [dbo].[Customers](BusinessSegment);
GO;
CREATE INDEX IX_Customers_CustomerStatus ON [dbo].[Customers](CustomerStatus);
GO;
CREATE INDEX IX_Customers_AssignedToUserId ON [dbo].[Customers](AssignedToUserId);
GO;

CREATE INDEX IX_Customers_IsActive ON [dbo].[Customers](IsActive);
GO;
CREATE INDEX IX_Customers_MobileNumber ON [dbo].[Customers](MobileNumber);
GO;
CREATE INDEX IX_Customers_RegistrationNumber ON [dbo].[Customers](RegistrationNumber);
GO;
