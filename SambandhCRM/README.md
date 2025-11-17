# Sambandh CRM - Customer Relationship Management System

## Overview

Sambandh CRM is a comprehensive Customer Relationship Management system designed for NABKISAN Finance Limited. It's built using **.NET 8 Blazor Server**, follows **MVC architecture**, uses **SSDT for database management**, and **Dapper for data access** with **MS SQL Server**.

## System Architecture

### Technology Stack

- **Frontend**: Blazor Server (ASP.NET Core 8.0)
- **Backend**: C# .NET 8
- **Database**: Microsoft SQL Server
- **ORM**: Dapper (Micro-ORM)
- **Database Management**: SSDT (SQL Server Data Tools)
- **Authentication**: Cookie-based Authentication
- **UI Framework**: Bootstrap 5
- **Icons**: Bootstrap Icons

### Project Structure

```
SambandhCRM/
├── SambandhCRM.sln                      # Solution file
├── SambandhCRM.Database/                # SSDT Database Project
│   ├── Tables/                          # Database table definitions
│   │   ├── Users.sql
│   │   ├── Roles.sql
│   │   ├── Customers.sql
│   │   ├── Leads.sql
│   │   ├── Communications.sql
│   │   └── ... (other tables)
│   └── SambandhCRM.Database.sqlproj
├── SambandhCRM.Core/                    # Core Business Logic Layer
│   ├── Models/                          # Domain models
│   ├── Enums/                           # Enumerations
│   ├── Interfaces/                      # Interface definitions
│   ├── Services/                        # Business services
│   └── SambandhCRM.Core.csproj
├── SambandhCRM.Data/                    # Data Access Layer
│   ├── Context/                         # Database context
│   │   └── DapperContext.cs
│   ├── Repositories/                    # Repository implementations
│   │   ├── UserRepository.cs
│   │   ├── CustomerRepository.cs
│   │   ├── LeadRepository.cs
│   │   └── ... (other repositories)
│   └── SambandhCRM.Data.csproj
└── SambandhCRM.Web/                     # Blazor Server Web Application
    ├── Pages/                           # Razor pages
    │   ├── Login.razor
    │   ├── Dashboard.razor
    │   ├── Customers/
    │   ├── Leads/
    │   └── ... (other pages)
    ├── Shared/                          # Shared components
    │   ├── MainLayout.razor
    │   ├── NavMenu.razor
    │   └── ... (other shared components)
    ├── wwwroot/                         # Static files
    │   └── css/
    │       └── site.css
    ├── Program.cs                       # Application entry point
    ├── appsettings.json                 # Configuration
    └── SambandhCRM.Web.csproj

```

## Key Features

### 1. Customer Database (Party Master)
- Comprehensive customer information management
- Support for multiple legal constitutions:
  - Company
  - Society
  - Trust/NGO
  - Partnership/LLP
  - Individual/Proprietor
- Business segment categorization
- Duplicate prevention mechanisms
- MCA integration support for company verification

### 2. Lead Tracking
- Lead pipeline management
- Status tracking (New, In-Progress, Documentation, Submitted to Credit, Dropped, Converted)
- Follow-up scheduling
- Document checklist management
- Lead source tracking

### 3. Relationship Management
- Key person profiles
- Organization-individual relationship mapping
- Role-based contact management
- Decision maker identification

### 4. Communication Log
- Multi-channel communication tracking (Phone, Email, Meeting, Site Visit, WhatsApp, SMS)
- Bulk communication support
- Communication history
- Next action scheduling

### 5. Reports & Analytics
- Dashboard with key metrics
- Customer reports
- Lead pipeline reports
- Activity reports
- Data export capabilities

### 6. System Administration
- User management
- Role-based access control (RBAC)
- Master data configuration
- System settings management

## User Roles

1. **Administrator**: Full system access and configuration
2. **Senior Management**: View and report access
3. **Regional Manager**: Manage multiple relationship managers
4. **Relationship Manager**: Manage customer relationships and leads

## Prerequisites

### Software Requirements

- **Visual Studio 2022** or later (with .NET 8 SDK)
- **SQL Server 2019** or later (Express, Developer, or Standard Edition)
- **SQL Server Management Studio (SSMS)** (recommended)
- **SQL Server Data Tools (SSDT)** (for database project)
- **.NET 8 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/8.0)

### Hardware Requirements

- Processor: 2 GHz or faster
- RAM: 4 GB minimum (8 GB recommended)
- Disk Space: 10 GB available space

## Installation & Setup

### 1. Clone or Extract the Project

```bash
# If using Git
git clone <repository-url>
cd SambandhCRM

# Or extract the ZIP file to your desired location
```

### 2. Database Setup

#### Option A: Using SSDT (Recommended)

1. Open `SambandhCRM.sln` in Visual Studio
2. Right-click on `SambandhCRM.Database` project
3. Select **Publish**
4. Configure connection to your SQL Server
5. Database name: `SambandhCRM`
6. Click **Publish**

#### Option B: Using SQL Scripts Manually

1. Open SQL Server Management Studio (SSMS)
2. Create a new database:
   ```sql
   CREATE DATABASE SambandhCRM;
   GO
   USE SambandhCRM;
   GO
   ```

3. Execute all SQL scripts from `SambandhCRM.Database/Tables/` folder in this order:
   - Roles.sql
   - Users.sql
   - UserRoles.sql
   - MasterData.sql
   - Customers.sql
   - CustomerContacts.sql
   - Individuals.sql
   - IndividualOrganizations.sql
   - Leads.sql
   - LeadActivities.sql
   - Communications.sql
   - Documents.sql
   - AuditLog.sql

### 3. Configure Connection String

1. Open `SambandhCRM.Web/appsettings.json`
2. Update the connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=SambandhCRM;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

**Connection String Examples:**

- **Windows Authentication (Trusted Connection):**
  ```
  Server=localhost;Database=SambandhCRM;Trusted_Connection=True;TrustServerCertificate=True;
  ```

- **SQL Server Authentication:**
  ```
  Server=localhost;Database=SambandhCRM;User Id=sa;Password=YourPassword;TrustServerCertificate=True;
  ```

- **Named Instance:**
  ```
  Server=localhost\\SQLEXPRESS;Database=SambandhCRM;Trusted_Connection=True;TrustServerCertificate=True;
  ```

### 4. Create Initial Admin User

Execute this SQL script in SSMS to create the first admin user:

```sql
USE SambandhCRM;
GO

-- Create admin user (Password: Admin@123)
-- Password hash for "Admin@123"
INSERT INTO Users (UserName, Email, PasswordHash, FirstName, LastName, IsActive, CreatedDate)
VALUES ('admin', 'admin@nabkisan.com', 'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=', 'System', 'Administrator', 1, GETDATE());

-- Assign Administrator role
DECLARE @AdminUserId INT = SCOPE_IDENTITY();
DECLARE @AdminRoleId INT = (SELECT RoleId FROM Roles WHERE RoleName = 'Administrator');

INSERT INTO UserRoles (UserId, RoleId, AssignedDate)
VALUES (@AdminUserId, @AdminRoleId, GETDATE());
GO
```

**Default Admin Credentials:**
- Username: `admin`
- Password: `Admin@123`

**Important**: Change the default password after first login!

### 5. Restore NuGet Packages

```bash
cd SambandhCRM
dotnet restore
```

Or in Visual Studio:
- Right-click on Solution → Restore NuGet Packages

### 6. Build the Solution

```bash
dotnet build
```

Or in Visual Studio:
- Build → Build Solution (Ctrl+Shift+B)

### 7. Run the Application

```bash
cd SambandhCRM.Web
dotnet run
```

Or in Visual Studio:
- Set `SambandhCRM.Web` as startup project
- Press F5 or click Start

The application will be available at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

## Configuration

### Application Settings

Edit `appsettings.json` for:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your connection string"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Authentication Settings

Configure in `Program.cs`:
- Cookie expiration time (default: 8 hours)
- Sliding expiration
- Login/Logout paths

## Usage Guide

### First Time Login

1. Navigate to `https://localhost:5001`
2. You'll be redirected to the login page
3. Enter credentials:
   - Username: `admin`
   - Password: `Admin@123`
4. After login, you'll see the Dashboard

### Creating Users

As an Administrator:
1. Navigate to **Administration** → **Users**
2. Click **Add New User**
3. Fill in user details
4. Assign roles
5. Save

### Adding Customers

1. Navigate to **Customers**
2. Click **Add New Customer**
3. Select Legal Constitution
4. Fill in customer details
5. For companies, use **Fetch from MCA** to auto-populate data
6. Save

### Creating Leads

1. Navigate to **Leads**
2. Click **Add New Lead**
3. Select customer (or create new)
4. Fill in lead details
5. Assign to Relationship Manager
6. Schedule follow-ups
7. Save

### Managing Communications

1. Navigate to **Communications**
2. Click **Log Communication**
3. Select customer/lead
4. Choose communication type
5. Add summary and next actions
6. Save

## Database Schema

### Core Tables

#### Users
- User authentication and profile information
- Fields: UserId, UserName, Email, PasswordHash, FirstName, LastName, etc.

#### Roles
- System roles definition
- Default roles: Administrator, Senior Management, Regional Manager, Relationship Manager

#### Customers
- Central customer database (Party Master)
- Supports all legal constitutions
- Comprehensive contact and financial information

#### Leads
- Lead pipeline management
- Links to customers
- Status tracking and follow-ups

#### Communications
- Multi-channel communication log
- Links to customers and leads
- Supports bulk communications

#### MasterData
- Configurable dropdown values
- Categories: BusinessSegment, ProductCategory, LeadSource, etc.

#### AuditLog
- System-wide audit trail
- Tracks all data changes

### Relationships

```
Users (1) ----< (M) UserRoles (M) >---- (1) Roles
Users (1) ----< (M) Customers (assignee)
Customers (1) ----< (M) Leads
Customers (1) ----< (M) Communications
Leads (1) ----< (M) Communications
Customers (1) ----< (M) IndividualOrganizations (M) >---- (1) Individuals
Customers (1) ----< (M) Documents
Leads (1) ----< (M) Documents
```

## API Reference

### Repository Pattern

All data access follows the repository pattern:

```csharp
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<int> AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
}
```

### Using Repositories in Pages

```csharp
@inject ICustomerRepository CustomerRepo

@code {
    private List<Customer> customers = new();

    protected override async Task OnInitializedAsync()
    {
        customers = (await CustomerRepo.GetAllAsync()).ToList();
    }
}
```

## Security

### Authentication
- Cookie-based authentication
- Secure password hashing (SHA256)
- Session management with configurable timeout

### Authorization
- Role-based access control (RBAC)
- Policy-based authorization
- Page-level and component-level security

### Data Protection
- SQL injection prevention (Dapper parameterized queries)
- XSS protection (Blazor automatic encoding)
- HTTPS enforcement in production
- Secure password storage (hashed)

### Best Practices Implemented
- Input validation
- Soft deletes (IsActive flag)
- Audit logging
- Connection string encryption (in production)

## Deployment

### IIS Deployment

1. **Publish the Application**
   ```bash
   dotnet publish -c Release -o ./publish
   ```

2. **Install IIS** (if not already installed)
   - Enable IIS in Windows Features
   - Install .NET 8 Hosting Bundle

3. **Create IIS Site**
   - Open IIS Manager
   - Add new website
   - Point to publish folder
   - Configure application pool (.NET CLR Version: No Managed Code)

4. **Configure Connection String**
   - Update connection string in published `appsettings.json`
   - Use production SQL Server

5. **Set Permissions**
   - Grant IIS_IUSRS read/write permissions to publish folder

### Azure App Service Deployment

1. **Create Azure SQL Database**
2. **Publish Database Schema** using SSDT
3. **Create Azure App Service** (.NET 8)
4. **Configure Connection String** in App Service settings
5. **Deploy Application**
   - Use Visual Studio Publish
   - Or Azure DevOps pipeline
   - Or GitHub Actions

### Docker Deployment

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SambandhCRM.Web/SambandhCRM.Web.csproj", "SambandhCRM.Web/"]
COPY ["SambandhCRM.Core/SambandhCRM.Core.csproj", "SambandhCRM.Core/"]
COPY ["SambandhCRM.Data/SambandhCRM.Data.csproj", "SambandhCRM.Data/"]
RUN dotnet restore "SambandhCRM.Web/SambandhCRM.Web.csproj"
COPY . .
WORKDIR "/src/SambandhCRM.Web"
RUN dotnet build "SambandhCRM.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SambandhCRM.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SambandhCRM.Web.dll"]
```

## Troubleshooting

### Common Issues

#### 1. Connection String Error
**Error**: Cannot connect to database

**Solution**:
- Verify SQL Server is running
- Check server name and authentication method
- Ensure database exists
- Test connection in SSMS first

#### 2. Login Not Working
**Error**: Invalid username or password

**Solution**:
- Verify admin user was created
- Check password hash matches
- Clear browser cookies
- Check UserRoles table for role assignment

#### 3. NuGet Package Restore Failed
**Error**: Package restore failed

**Solution**:
```bash
dotnet nuget locals all --clear
dotnet restore
```

#### 4. Port Already in Use
**Error**: Port 5001 is already in use

**Solution**:
- Change port in `Properties/launchSettings.json`
- Or kill process using the port

#### 5. Database Permission Issues
**Error**: Cannot open database

**Solution**:
- Grant db_owner role to application user
- Check SQL Server authentication mode
- Verify connection string credentials

## Performance Optimization

### Database
- Indexes on frequently queried columns
- Stored procedures for complex queries
- Connection pooling (automatic with Dapper)

### Application
- Lazy loading for large datasets
- Caching for master data
- Pagination for list views
- Async/await throughout

### Frontend
- Component virtualization for large lists
- Minimize re-renders
- Optimize CSS/JS delivery

## Maintenance

### Backup Strategy
- Daily automated database backups
- Transaction log backups every 15 minutes
- Store backups offsite

### Monitoring
- Application logs in `Logs` folder
- SQL Server error logs
- Performance counters
- User activity audit logs

### Updates
- Regular .NET updates
- NuGet package updates
- Security patches
- Database schema migrations

## Support & Documentation

### Additional Resources
- .NET 8 Documentation: https://docs.microsoft.com/dotnet
- Blazor Documentation: https://docs.microsoft.com/aspnet/core/blazor
- Dapper Documentation: https://github.com/DapperLib/Dapper
- SQL Server Documentation: https://docs.microsoft.com/sql

### Getting Help
- Check troubleshooting section
- Review application logs
- Contact IT support team

## License

Copyright © 2024 NABKISAN Finance Limited. All rights reserved.

This software is proprietary and confidential.

## Version History

### Version 1.0 (November 2024)
- Initial release
- Core CRM functionality
- Customer and Lead management
- Communication logging
- Basic reporting
- User management

---

**Document Version**: 1.0
**Last Updated**: November 17, 2024
**Author**: IT Team, NABKISAN Finance Limited
