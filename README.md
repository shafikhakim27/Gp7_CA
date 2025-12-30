# Gp7_CA

Mobile Application Development Course Assignment - Group 7

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL Server 8.0+](https://dev.mysql.com/downloads/mysql/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- [Postman](https://www.postman.com/downloads/) (for API testing)

## Database Setup

### Execute UserDB.sql

**Method A: MySQL Workbench**
1. Open **MySQL Workbench** and connect to your server
2. Open `UserDB.sql` from the project root
3. Click **Execute** (lightning bolt icon) or press `Ctrl+Shift+Enter`

**Method B: MySQL Command Line**
```bash
mysql -u root -p < UserDB.sql
```

### Configure Connection String

Update your MySQL password in `Repository\Constants.cs`:

```csharp
public static string CONNECTION_STRING = @"server=localhost;uid=root;pwd=YOUR_PASSWORD;database=user";
```

## Running the Project

1. Open `Gp7_CA.sln` in Visual Studio 
2. Press `F5` to build and run

The application will launch at:
- **HTTP**: `http://localhost:5107`

## Testing with Postman

**Endpoint:**
```
POST https://localhost:5107/User/Authenticate
```

**Headers:**
```
Content-Type: application/json
```

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

**Test Users:**

Contact team members for test user credentials.

## Troubleshooting

**SSL Error in Postman:** Disable SSL verification in Settings or use HTTP endpoint

**Database Connection Failed:** 
- Ensure MySQL is running
- Verify credentials in `Constants.cs`
- Run `UserDB.sql` to create database

**NullReferenceException:** 
- Set `Content-Type: application/json` header
- Ensure JSON body is properly formatted

## Security Note
**Educational project only.** Passwords are stored in plain text. For production, use password hashing and secure authentication.
