# Gp7_CA

Mobile Application Development Course Assignment - Group 7

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL Server 8.0+](https://dev.mysql.com/downloads/mysql/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- [Postman](https://www.postman.com/downloads/) (optional, for API testing)

## Database Setup

### Execute UserDB.sql

**MySQL Workbench:**
1. Open **MySQL Workbench** and connect to your server
2. Open `UserDB.sql` from the project root
3. Click **Execute** (⚡) or press `Ctrl+Shift+Enter`

**MySQL Command Line:**
```bash
mysql -u root -p < UserDB.sql
```

### Configure Connection String

Update MySQL password in `Repository\Constants.cs`:
```csharp
public static string CONNECTION_STRING = @"server=localhost;uid=root;pwd=YOUR_PASSWORD;database=user";
```

## Running the Project

1. Open `Gp7_CA.sln` in Visual Studio 2022
2. Press `F5` to build and run
3. Application launches at `http://localhost:5107`

## API Testing with Postman

**Endpoint:** `POST http://localhost:5107/User/Authenticate`

**Headers:** `Content-Type: application/json`

**Request Body:**
```json
{
  "username": "testuser",
  "password": "testpass"
}
```

**Success Response (200 OK):**
```json
{
  "success": true,
  "message": "Welcome testuser",
  "isPaidUser": "False"
}
```
**Test Credentials:** Contact team members for user credentials.

## Troubleshooting
- **Database Connection Failed:** Ensure MySQL is running and credentials in `Constants.cs` are correct
- **NullReferenceException:** Verify `Content-Type: application/json` header is set in Postman
- **Table Not Found:** Run `UserDB.sql` to create the database and tables

## Security Note
⚠️ Educational project only. Passwords stored in plain text. Use proper authentication for production.