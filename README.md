Employee
## Description
This is Employee Management Software
project.
## Features
- Admin,Employee login the system
- Admin can add,edit employee profile
- Employee can edit profile
- More features...
## Getting Started
### Prerequisites
- .NET Core SDK (Version 6.0.0)
- Entity Framework Core (Version 6.0.0)
- Any other dependencies
### Installation
1. Clone the repository:
 git clone https://github.com/your-username/your-project-name.git
2. Navigate to the project directory:
 cd your-project-name
### Database Setup
To set up and update the database:
1. **change connection string in appsettings.json:**
"ConnectionStrings": {
  "DefaultConnection": "Server={{yourDatabaseServer};Database={{database}};Trusted_Connection=True;"
}
2. **get package manager console for Employee.Infrastructure class library :**
  1. **Add a Migration:**
   dotnet ef migrations add MigrationName
   Replace MigrationName with a descriptive name for your migration.
  2. **Update the Database:**
   dotnet ef database update
### Running the Application
Execute the following command in the project directory:
dotnet run

