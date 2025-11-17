CREATE TABLE [dbo].[UserRoles]
(
    [UserRoleId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    [AssignedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [AssignedBy] INT NULL,
    CONSTRAINT FK_UserRoles_Users FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([UserId]),
    CONSTRAINT FK_UserRoles_Roles FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles]([RoleId]),
    CONSTRAINT UQ_UserRoles UNIQUE ([UserId], [RoleId])
);

GO

CREATE INDEX IX_UserRoles_UserId ON [dbo].[UserRoles](UserId);
CREATE INDEX IX_UserRoles_RoleId ON [dbo].[UserRoles](RoleId);
