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

## API Endpoints

### 1. User Authentication
**POST** `http://localhost:5107/User/Authenticate`

**Request:**
```json
{
  "username": "testuser",
  "password": "testpass"
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Welcome testuser",
  "userId": 1,
  "isPaidUser": false,
  "completionTime": 0
}
```

### 2. Update Completion Time
**POST** `http://localhost:5107/User/UpdateCompletionTime`

**Request:**
```json
{
  "userId": 1,
  "completionTime": 45.5
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "message": "Completion time updated successfully"
}
```

### 3. Get Leaderboard
**GET** `http://localhost:5107/User/Leaderboard?limit=10`

**Response (200 OK):**
```json
{
  "success": true,
  "count": 2,
  "leaderboard": [
    {"username": "user1", "completionTime": 30.5, "isPaidUser": false},
    {"username": "user2", "completionTime": 45.2, "isPaidUser": true}
  ]
}
```

**Note:** All POST requests require `Content-Type: application/json` header.

**Test Credentials:** Contact team members for user credentials.

## Troubleshooting

- **Database Connection Failed:** Ensure MySQL is running and credentials in `Constants.cs` are correct
- **NullReferenceException:** Verify `Content-Type: application/json` header is set
- **Table Not Found:** Run `UserDB.sql` to create the database and tables

## Security Note

⚠️ Educational project only. Passwords stored in plain text. Use proper authentication for production.