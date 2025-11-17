-- =============================================
-- Seed Data for SambandhCRM
-- This script creates:
-- 1. Default Roles
-- 2. Default Admin User
-- 3. User-Role Mapping
-- 4. Master Data (optional)
-- =============================================

USE SambandhCRM;
GO

-- =============================================
-- 1. INSERT DEFAULT ROLES
-- =============================================
PRINT 'Inserting Roles...';

IF NOT EXISTS (SELECT 1 FROM Roles WHERE RoleName = 'Administrator')
BEGIN
    INSERT INTO Roles (RoleName, Description, IsActive, CreatedDate)
    VALUES ('Administrator', 'Full system access', 1, GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM Roles WHERE RoleName = 'Senior Management')
BEGIN
    INSERT INTO Roles (RoleName, Description, IsActive, CreatedDate)
    VALUES ('Senior Management', 'Senior management access', 1, GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM Roles WHERE RoleName = 'Regional Manager')
BEGIN
    INSERT INTO Roles (RoleName, Description, IsActive, CreatedDate)
    VALUES ('Regional Manager', 'Regional management access', 1, GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM Roles WHERE RoleName = 'Relationship Manager')
BEGIN
    INSERT INTO Roles (RoleName, Description, IsActive, CreatedDate)
    VALUES ('Relationship Manager', 'Customer relationship management access', 1, GETDATE());
END

PRINT 'Roles inserted successfully.';

-- =============================================
-- 2. INSERT DEFAULT ADMIN USER
-- =============================================
PRINT 'Inserting default admin user...';

-- Default credentials:
-- Username: admin
-- Password: Admin@123
-- Email: admin@sambandhcrm.com

IF NOT EXISTS (SELECT 1 FROM Users WHERE UserName = 'admin')
BEGIN
    INSERT INTO Users (
        UserName,
        Email,
        PasswordHash,
        FirstName,
        LastName,
        MobileNumber,
        IsActive,
        CreatedDate
    )
    VALUES (
        'admin',
        'admin@sambandhcrm.com',
        '6G94qKPK8LYNjnTllCqm2G3BUM08AzOK7yW30tfjrMc=',  -- Password: Admin@123
        'System',
        'Administrator',
        NULL,
        1,
        GETDATE()
    );
    PRINT 'Default admin user created successfully.';
END
ELSE
BEGIN
    PRINT 'Admin user already exists.';
END

-- =============================================
-- 3. MAP ADMIN USER TO ADMINISTRATOR ROLE
-- =============================================
PRINT 'Mapping admin user to Administrator role...';

DECLARE @AdminUserId INT;
DECLARE @AdminRoleId INT;

SELECT @AdminUserId = UserId FROM Users WHERE UserName = 'admin';
SELECT @AdminRoleId = RoleId FROM Roles WHERE RoleName = 'Administrator';

IF @AdminUserId IS NOT NULL AND @AdminRoleId IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM UserRoles WHERE UserId = @AdminUserId AND RoleId = @AdminRoleId)
    BEGIN
        INSERT INTO UserRoles (UserId, RoleId, AssignedDate)
        VALUES (@AdminUserId, @AdminRoleId, GETDATE());
        PRINT 'Admin user mapped to Administrator role successfully.';
    END
    ELSE
    BEGIN
        PRINT 'Admin user role mapping already exists.';
    END
END

-- =============================================
-- 4. INSERT MASTER DATA (OPTIONAL)
-- Uncomment the sections below if you want to populate master data
-- =============================================

/*
PRINT 'Inserting Master Data...';

-- Business Segments
IF NOT EXISTS (SELECT 1 FROM MasterData WHERE Category = 'Business Segment' AND Value = 'MSME')
BEGIN
    INSERT INTO MasterData (Category, Value, Description, DisplayOrder, IsActive, CreatedDate)
    VALUES
        ('Business Segment', 'MSME', 'Micro, Small and Medium Enterprises', 1, 1, GETDATE()),
        ('Business Segment', 'Corporate', 'Large Corporate Enterprises', 2, 1, GETDATE()),
        ('Business Segment', 'Retail', 'Retail Banking', 3, 1, GETDATE()),
        ('Business Segment', 'Agriculture', 'Agriculture and Allied Activities', 4, 1, GETDATE()),
        ('Business Segment', 'Priority Sector', 'Priority Sector Lending', 5, 1, GETDATE());
END

-- Lead Sources
IF NOT EXISTS (SELECT 1 FROM MasterData WHERE Category = 'Lead Source' AND Value = 'Walk-in')
BEGIN
    INSERT INTO MasterData (Category, Value, Description, DisplayOrder, IsActive, CreatedDate)
    VALUES
        ('Lead Source', 'Walk-in', 'Customer walked into branch', 1, 1, GETDATE()),
        ('Lead Source', 'Reference', 'Referred by existing customer', 2, 1, GETDATE()),
        ('Lead Source', 'Campaign', 'Marketing campaign', 3, 1, GETDATE()),
        ('Lead Source', 'Website', 'Website inquiry', 4, 1, GETDATE()),
        ('Lead Source', 'Cold Call', 'Outbound cold calling', 5, 1, GETDATE()),
        ('Lead Source', 'Event', 'Trade show or event', 6, 1, GETDATE());
END

-- Lead Status
IF NOT EXISTS (SELECT 1 FROM MasterData WHERE Category = 'Lead Status' AND Value = 'New')
BEGIN
    INSERT INTO MasterData (Category, Value, Description, DisplayOrder, IsActive, CreatedDate)
    VALUES
        ('Lead Status', 'New', 'New lead received', 1, 1, GETDATE()),
        ('Lead Status', 'Contacted', 'Initial contact made', 2, 1, GETDATE()),
        ('Lead Status', 'Qualified', 'Lead qualified', 3, 1, GETDATE()),
        ('Lead Status', 'Proposal', 'Proposal sent', 4, 1, GETDATE()),
        ('Lead Status', 'Negotiation', 'In negotiation', 5, 1, GETDATE()),
        ('Lead Status', 'Won', 'Lead converted to customer', 6, 1, GETDATE()),
        ('Lead Status', 'Lost', 'Lead lost', 7, 1, GETDATE());
END

-- Communication Types
IF NOT EXISTS (SELECT 1 FROM MasterData WHERE Category = 'Communication Type' AND Value = 'Email')
BEGIN
    INSERT INTO MasterData (Category, Value, Description, DisplayOrder, IsActive, CreatedDate)
    VALUES
        ('Communication Type', 'Email', 'Email communication', 1, 1, GETDATE()),
        ('Communication Type', 'Phone', 'Phone call', 2, 1, GETDATE()),
        ('Communication Type', 'Meeting', 'In-person meeting', 3, 1, GETDATE()),
        ('Communication Type', 'SMS', 'SMS message', 4, 1, GETDATE()),
        ('Communication Type', 'WhatsApp', 'WhatsApp message', 5, 1, GETDATE());
END

-- Loan Types
IF NOT EXISTS (SELECT 1 FROM MasterData WHERE Category = 'Loan Type' AND Value = 'Term Loan')
BEGIN
    INSERT INTO MasterData (Category, Value, Description, DisplayOrder, IsActive, CreatedDate)
    VALUES
        ('Loan Type', 'Term Loan', 'Term Loan', 1, 1, GETDATE()),
        ('Loan Type', 'Working Capital', 'Working Capital Loan', 2, 1, GETDATE()),
        ('Loan Type', 'Cash Credit', 'Cash Credit Facility', 3, 1, GETDATE()),
        ('Loan Type', 'Overdraft', 'Overdraft Facility', 4, 1, GETDATE()),
        ('Loan Type', 'Bill Discounting', 'Bill Discounting', 5, 1, GETDATE()),
        ('Loan Type', 'Letter of Credit', 'Letter of Credit', 6, 1, GETDATE());
END

-- Customer Types
IF NOT EXISTS (SELECT 1 FROM MasterData WHERE Category = 'Customer Type' AND Value = 'Individual')
BEGIN
    INSERT INTO MasterData (Category, Value, Description, DisplayOrder, IsActive, CreatedDate)
    VALUES
        ('Customer Type', 'Individual', 'Individual customer', 1, 1, GETDATE()),
        ('Customer Type', 'Partnership', 'Partnership firm', 2, 1, GETDATE()),
        ('Customer Type', 'Private Limited', 'Private Limited Company', 3, 1, GETDATE()),
        ('Customer Type', 'Public Limited', 'Public Limited Company', 4, 1, GETDATE()),
        ('Customer Type', 'LLP', 'Limited Liability Partnership', 5, 1, GETDATE()),
        ('Customer Type', 'Proprietorship', 'Sole Proprietorship', 6, 1, GETDATE());
END

PRINT 'Master Data inserted successfully.';
*/

-- =============================================
-- SUMMARY
-- =============================================
PRINT '';
PRINT '==============================================';
PRINT 'Seed data installation completed successfully!';
PRINT '==============================================';
PRINT '';
PRINT 'DEFAULT LOGIN CREDENTIALS:';
PRINT '  Username: admin';
PRINT '  Password: Admin@123';
PRINT '  Email: admin@sambandhcrm.com';
PRINT '';
PRINT 'IMPORTANT: Please change the default password after first login!';
PRINT '==============================================';
GO
