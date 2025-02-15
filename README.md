# .NET Engineer Workshop

## Overview
This workshop demonstrates how to build a RESTful API using **Clean Architecture**, **CQRS**, **MediatR**, and **Hangfire** for background job scheduling. It includes a **user management system** with authentication, session management, and inactive user suspension.

## Project Structure (Clean Architecture)

```
UsersManagement.API
│── Core  
│   ├── UsersManagement.Application   # Application logic (CQRS, MediatR, DTOs, Services, Validators)
│   ├── UsersManagement.Domain        # Business entities & domain logic  
│  
│── Infrastructure  
│   ├── UsersManagement.Infrastructure # External services (Middlewares, Services, Repositories)  
│   ├── UsersManagement.Persistence    # Database context & Migrations  
│  
│── Presentation  
│   ├── UsersManagement.API            # Controllers, Middlewares, API endpoints  
│       ├── Controllers                # API endpoints  
│       ├── appsettings.json           # Configuration  
│       ├── Program.cs                 # Entry point  
```

## Features
### User Management
- **Roles:**
    - Super Admin
    - Admin
    - Employee
- **CRUD Operations:**
    - **Create:** Only Super Admin can create employees.
    - **Read:** Super Admin & Admins can access user details.
    - **Update:**
        - Admins can update their own profiles.
        - Super Admin can update Admin profiles.
        - Employees can update their profiles (requires Admin approval).
        - Employees receive an email when an admin approves their updates.
    - **Delete:** Only Super Admin can delete users.
    - Send **login credentials** via email on employee creation.

### Authentication
- **First Login:**
    - Employees log in with a temporary password.
    - They must change it before proceeding.
- **Subsequent Logins:**
    - Employees log in with their updated password.
- **Multiple Concurrent Sessions:**
    - Employees can log in from multiple sessions.
    - Super Admin can view and terminate specific employee sessions.
- **Inactive User Suspension:**
    - Employees who don’t log in for a configurable timeframe get **suspended**.
    - Suspended users **cannot log in**.
    - Admins can **reactivate** suspended users.

## Technologies Used
- **.NET Core**
- **MSSQL**
- **CQRS & MediatR** (for request-response handling)
- **Hangfire** (for background jobs)
- **Third-Party Library for Password Hashing**

## API Endpoints

### **Login**
| HTTP Method | Endpoint      | Description |
|------------|--------------|-------------|
| POST       | `/api/login`  | User login  |

### **UserAccount**
| HTTP Method | Endpoint                     | Description |
|------------|------------------------------|-------------|
| POST       | `/api/userAccount`            | Create a new user account |
| PUT        | `/api/userAccount`            | Update user account details |
| GET        | `/api/userAccount/AllActive`  | Get all active user accounts |
| GET        | `/api/userAccount/AllInactive` | Get all inactive user accounts |
| PUT        | `/api/userAccount/Reactivate` | Reactivate a suspended account |

### **UserProfile**
| HTTP Method | Endpoint                                  | Description |
|------------|------------------------------------------|-------------|
| PUT        | `/api/userProfile`                       | Update user profile |
| PUT        | `/api/userProfile/UpdateProfileByUserId` | Update profile by User ID |

### **UserProfileUpdates**
| HTTP Method | Endpoint                                              | Description |
|------------|------------------------------------------------------|-------------|
| GET        | `/api/userProfileUpdates/AllPendingRequests`         | Get all pending profile update requests |
| PUT        | `/api/userProfileUpdates/ApprovePendingRequests`     | Approve profile update requests |

### **UserSession**
| HTTP Method | Endpoint                                    | Description |
|------------|--------------------------------------------|-------------|
| GET        | `/api/userSession/AllEmployeeSession`      | Get all active employee sessions |
| DELETE     | `/api/userSession/EmployeeSession`        | Terminate an employee session |

## Setup Instructions
1. Clone the repository:
   ```sh
   git clone https://github.com/your-repo-url.git
   cd your-repo
   ```
2. Configure the **database connection**, **mail configuration**, and **JWT token secret key** in `appsettings.json`.
3. Run database migrations:
   ```sh
   dotnet ef database update
   ```
4. Start the application:
   ```sh
   dotnet run
   ```
5. Access the **Swagger Dashboard** at:
    ```
   https://localhost:{port}/swagger/index.html
   ```
6. Access the **Hangfire Dashboard** at:
   ```
   http://localhost:{port}/hangfire
   ```

## Background Jobs (Hangfire)
- **User Suspension Job:**
    - Runs periodically to check inactive users and suspend them.
    - Configurable inactivity period.
- **Email Notifications:**
    - Sends an email when an employee update is approved.
    - Sends login credentials for newly created employees.



