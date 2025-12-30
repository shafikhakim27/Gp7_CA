# Gp7_CA

Mobile Application Development Course Assignment - Group 7

---

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL Server 8.0+](https://dev.mysql.com/downloads/mysql/)
- [Visual Studio](https://visualstudio.microsoft.com/)

### Required Packages

```bash
dotnet add package Swashbuckle.AspNetCore
dotnet restore
```

### Database Setup

**1. Execute UserDB.sql**

*MySQL Workbench:*
1. Open MySQL Workbench and connect to your server
2. Open `UserDB.sql` from project root
3. Click Execute (⚡) or press `Ctrl+Shift+Enter`

*Command Line:*
```bash
mysql -u root -p < UserDB.sql
```

**2. Configure Connection**

Update password in `Repository\Constants.cs`:
```csharp
public static string CONNECTION_STRING = @"server=localhost;uid=root;pwd=YOUR_PASSWORD;database=user";
```

### Running the Project

1. Open `Gp7_CA.sln` in Visual Studio
2. Restore packages (right-click solution → Restore NuGet Packages)
3. Press `F5` to run
4. Application: `http://localhost:5107`
5. Swagger UI: `http://localhost:5107/swagger`

### API Endpoints

**1. User Authentication**

`POST http://localhost:5107/User/Authenticate`

```json
// Request
{
  "username": "testuser",
  "password": "testpass"
}

// Response (200 OK)
{
  "success": true,
  "message": "Welcome testuser",
  "userId": 1,
  "isPaidUser": false,
  "completionTime": 0
}
```

**2. Update Completion Time**

`POST http://localhost:5107/User/UpdateCompletionTime`

```json
// Request
{
  "userId": 1,
  "completionTime": 45.5
}

// Response (200 OK)
{
  "success": true,
  "message": "Completion time updated successfully"
}
```

**3. Get Leaderboard**

`GET http://localhost:5107/User/Leaderboard?limit=10`

```json
// Response (200 OK)
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

### Troubleshooting

| Issue | Solution |
|-------|----------|
| Database Connection Failed | Check MySQL is running and password in `Constants.cs` is correct |
| NullReferenceException (API) | Set `Content-Type: application/json` header in request |
| Table Not Found | Run `UserDB.sql` to create database and tables |
| Swagger UI Not Appearing | Install `Swashbuckle.AspNetCore` package and verify `Program.cs` configuration |
| ViewData NullReferenceException | Remove `@page` and `@model PageModel` directives from views in `/Views` folder |

### Security Note
⚠️ Educational project only. Passwords stored in plain text. Use proper authentication for production.

---

### 先决条件

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL Server 8.0+](https://dev.mysql.com/downloads/mysql/)
- [Visual Studio](https://visualstudio.microsoft.com/)

### 所需包

```bash
dotnet add package Swashbuckle.AspNetCore
dotnet restore
```

### 数据库设置

**1. 执行UserDB.sql**

*MySQL Workbench:*
1. 打开MySQL Workbench并连接到服务器
2. 从项目根目录打开`UserDB.sql`
3. 点击执行(⚡)或按`Ctrl+Shift+Enter`

*命令行:*
```bash
mysql -u root -p < UserDB.sql
```

**2. 配置连接**

在`Repository\Constants.cs`中更新密码:
```csharp
public static string CONNECTION_STRING = @"server=localhost;uid=root;pwd=YOUR_PASSWORD;database=user";
```

### 运行项目

1. 在Visual Studio中打开`Gp7_CA.sln`
2. 恢复包（右键解决方案 → 恢复NuGet包）
3. 按`F5`运行
4. 应用程序: `http://localhost:5107`
5. Swagger UI: `http://localhost:5107/swagger`

### API端点

**1. 用户认证**

`POST http://localhost:5107/User/Authenticate`

```json
// 请求
{
  "username": "testuser",
  "password": "testpass"
}

// 响应 (200 OK)
{
  "success": true,
  "message": "Welcome testuser",
  "userId": 1,
  "isPaidUser": false,
  "completionTime": 0
}
```

**2. 更新完成时间**

`POST http://localhost:5107/User/UpdateCompletionTime`

```json
// 请求
{
  "userId": 1,
  "completionTime": 45.5
}

// 响应 (200 OK)
{
  "success": true,
  "message": "Completion time updated successfully"
}
```

**3. 获取排行榜**

`GET http://localhost:5107/User/Leaderboard?limit=10`

```json
// 响应 (200 OK)
{
  "success": true,
  "count": 2,
  "leaderboard": [
    {"username": "user1", "completionTime": 30.5, "isPaidUser": false},
    {"username": "user2", "completionTime": 45.2, "isPaidUser": true}
  ]
}
```

**注意:** 所有POST请求需要`Content-Type: application/json`头。

### 故障排除

| 问题 | 解决方案 |
|------|---------|
| 数据库连接失败 | 检查MySQL正在运行且`Constants.cs`中的密码正确 |
| 空引用异常 (API) | 在请求中设置`Content-Type: application/json`头 |
| 找不到表 | 运行`UserDB.sql`创建数据库和表 |
| Swagger UI未显示 | 安装`Swashbuckle.AspNetCore`包并验证`Program.cs`配置 |
| ViewData空引用异常 | 从`/Views`文件夹中的视图删除`@page`和`@model PageModel`指令 |

### 安全说明
⚠️ 仅用于教育目的。密码以明文存储。生产环境请使用适当的身份验证。