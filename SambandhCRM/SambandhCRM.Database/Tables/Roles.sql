CREATE TABLE [dbo].[Roles]
(
    [RoleId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [RoleName] NVARCHAR(50) NOT NULL UNIQUE,
    [Description] NVARCHAR(255) NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [CreatedBy] INT NULL,
    [ModifiedDate] DATETIME NULL,
    [ModifiedBy] INT NULL
);

GO

-- Insert default roles
INSERT INTO [dbo].[Roles] ([RoleName], [Description])
VALUES
    ('Administrator', 'System Administrator with full access'),
    ('Senior Management', 'Senior management with view and report access'),
    ('Regional Manager', 'Regional Manager overseeing multiple RMs'),
    ('Relationship Manager', 'Relationship Manager managing customers');
