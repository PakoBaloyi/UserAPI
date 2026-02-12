# UserAPI

# Prerequisites
- NET 8 SDK
- SQL Server (or LocalDB

# Clone the repository
# Restore packages

# Update database
dotnet ef database update -p UserApi.Infrastructure -s UserAPI

# Create migration (if not already present)
dotnet ef migrations add InitialCreate -p UserApi.Infrastructure -s UserAPI

# set API and BlazorUi as startup projects

