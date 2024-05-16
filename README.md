# Customer Management System

This project is a .NET Core API with a Razor page front end for managing customers. The system allows you to add, update, and delete customers.

## Project Structure

The solution is divided into four projects:

1. **API**: This is the API layer of the application. It exposes endpoints for retrieving, adding, deleting and updating customers.

2. **Web**: This is a simple front end using razor pages to call the api endpoints. Theres a table to view the customers, and buttons to edit and delete. There is a form to update or add new customers.  

3. **Infrastructure**: This is the infrastructure layer of the application. It contains data access logic and database configurations. It uses EF Core to interact with the SQL Server database and manage database migrations.

4. **Test**:  This project includes all the unit tests. It uses nunit and moq to to test the customer controller methods. 


### Prerequisites

- .NET 8
- SQL Server

### Installation

1. Clone the repository
2. Restore the NuGet packages
3. Update the `appsettings.json` files in both the API and Web projects with your configuration

## Configuration

### API Project

Update the following settings in the `appsettings.json` file in the API project:

- `DBConfiguration:ConnectionString`: Your database connection string
- `CorsConfiguration:Origins`: Your allowed origins for CORS
- `Kestrel:Endpoints:Https:Url`: Your API URL

### Web Project

Update the following settings in the `appsettings.json` file in the Web project:

- `AppSettings:BaseUrl`: Your API base URL
- `Kestrel:Endpoints:Https:Url`: Your Web project URL