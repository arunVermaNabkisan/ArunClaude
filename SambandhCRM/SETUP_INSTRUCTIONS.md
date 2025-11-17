# SambandhCRM - Setup Instructions

## Initial Database Setup

Since the Users table is empty, you need to run the seed data script to create a default admin user.

### Step 1: Run the Seed Data Script

Execute the SQL seed data script using one of the following methods:

#### Option A: Using SQL Server Management Studio (SSMS)
1. Open SQL Server Management Studio
2. Connect to your SQL Server instance (localhost)
3. Open the file: `SambandhCRM.Database/SeedData.sql`
4. Click **Execute** or press `F5`

#### Option B: Using sqlcmd (Command Line)
```bash
sqlcmd -S localhost -d SambandhCRM -E -i SambandhCRM.Database/SeedData.sql
```

#### Option C: Using Azure Data Studio
1. Open Azure Data Studio
2. Connect to your SQL Server instance
3. Open the file: `SambandhCRM.Database/SeedData.sql`
4. Click **Run** or press `F5`

### Step 2: Verify the Data

After running the script, verify that the data was inserted:

```sql
-- Check roles
SELECT * FROM Roles;

-- Check users
SELECT UserId, UserName, Email, FirstName, LastName, IsActive
FROM Users;

-- Check user-role mapping
SELECT ur.*, u.UserName, r.RoleName
FROM UserRoles ur
JOIN Users u ON ur.UserId = u.UserId
JOIN Roles r ON ur.RoleId = r.RoleId;
```

### Step 3: Login to the Application

Use the following default credentials:

```
Username: admin
Password: Admin@123
Email: admin@sambandhcrm.com
```

**IMPORTANT:** Change the default password after your first login!

---

## What the Seed Script Creates

The seed data script creates:

1. **Four Default Roles:**
   - Administrator (full system access)
   - Senior Management
   - Regional Manager
   - Relationship Manager

2. **Default Admin User:**
   - Username: `admin`
   - Password: `Admin@123` (SHA256 hashed)
   - Email: `admin@sambandhcrm.com`
   - Name: System Administrator
   - Active: Yes

3. **Role Assignment:**
   - Maps the admin user to the Administrator role

4. **Master Data (Optional):**
   - The script includes commented sections for master data
   - Uncomment if you want to populate default master data for:
     - Business Segments
     - Lead Sources
     - Lead Status
     - Communication Types
     - Loan Types
     - Customer Types

---

## Troubleshooting

### Issue: "Cannot open database 'SambandhCRM'"
**Solution:** Make sure the database exists. If not, create it:
```sql
CREATE DATABASE SambandhCRM;
GO
```

### Issue: "Login failed for user"
**Solution:** Check your connection string in `appsettings.json` and ensure SQL Server is running.

### Issue: "Object already exists" errors
**Solution:** The script uses `IF NOT EXISTS` checks, so it's safe to run multiple times. Existing data won't be duplicated.

### Issue: Login page doesn't accept the credentials
**Solution:**
1. Verify the user was created: `SELECT * FROM Users WHERE UserName = 'admin'`
2. Check that the application is running and the connection string is correct
3. Clear browser cache and cookies
4. Restart the application

---

## Next Steps

After logging in successfully:

1. **Change the default password**
   - Go to user profile/settings (when implemented)
   - Or update directly in the database for now

2. **Create additional users**
   - Currently, there's no UI for user management
   - You can insert users directly via SQL or create a user management page

3. **Populate master data**
   - Uncomment the master data sections in `SeedData.sql` and re-run
   - Or create your own master data as needed

4. **Assign roles to users**
   - Use the UserRoles table to assign roles to users
   - Example:
     ```sql
     INSERT INTO UserRoles (UserId, RoleId, AssignedDate)
     VALUES (@UserId, @RoleId, GETDATE());
     ```

---

## Creating Additional Users Manually

If you need to create additional users before a user management UI is built:

```sql
-- 1. Calculate password hash (use C# or Python)
-- Python: python3 -c "import hashlib; import base64; print(base64.b64encode(hashlib.sha256(b'YourPassword').digest()).decode())"

-- 2. Insert user
INSERT INTO Users (UserName, Email, PasswordHash, FirstName, LastName, IsActive, CreatedDate)
VALUES ('username', 'user@example.com', 'HASHED_PASSWORD', 'First', 'Last', 1, GETDATE());

-- 3. Get the new UserId
DECLARE @NewUserId INT = SCOPE_IDENTITY();

-- 4. Assign role (example: Relationship Manager)
DECLARE @RoleId INT;
SELECT @RoleId = RoleId FROM Roles WHERE RoleName = 'Relationship Manager';

INSERT INTO UserRoles (UserId, RoleId, AssignedDate)
VALUES (@NewUserId, @RoleId, GETDATE());
```

---

## Security Notes

- The default password uses SHA256 hashing
- Passwords are never stored in plain text
- The `PasswordHash` column stores the Base64-encoded SHA256 hash
- All role-based authorization is configured in the application
- Cookie-based authentication with 8-hour session timeout

---

## Support

For issues or questions, refer to the application documentation or contact the development team.
