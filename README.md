# Employee Service Management System

Employee Service Management System is a web application developed using ASP.NET Core MVC to simplify employee and service management. The application allows administrators to manage employees, services, appointments, and available time slots through a simple and user-friendly interface.

The project was built to improve my understanding of ASP.NET Core MVC, Dapper, SQL Server, and CRUD operations while following the MVC architecture.

## Features

Employee Management

- Add new employees
- Update employee information
- Delete employees
- View employee details

Service Management

- Add and manage services
- Edit service details
- Set service duration and charges
- Delete services

Employee Service Assignment

- Assign services to employees
- Manage employee-service relationships

Appointment Booking

- Book appointments for customers
- Search available time slots
- Store customer details
- View appointment history

Dashboard

- Dashboard summary
- Charts for appointment statistics
- Overview of employees, services, and bookings

## Technologies Used

- ASP.NET Core MVC (.NET 8)
- C#
- Dapper
- SQL Server
- Bootstrap 5
- HTML
- CSS
- JavaScript
- jQuery
- Stored Procedures

## Project Structure

```
EmployeeServiceManagement
│
├── Controllers
├── Models
├── Views
├── Repository
├── Services
├── wwwroot
├── Stored Procedures
├── appsettings.json
└── EmployeeServiceManagement.sln
```

## Getting Started

### Requirements

- Visual Studio 2022
- .NET 8 SDK
- SQL Server
- SQL Server Management Studio (SSMS)

### Installation

1. Clone this repository.

```bash
git clone https://github.com/bidushi01/EmployeeServiceManagement.git
```

2. Open the solution in Visual Studio.

3. Create a SQL Server database.

4. Execute the SQL scripts included in the project.

5. Update the connection string in **appsettings.json**.

6. Restore the NuGet packages.

7. Build and run the application.

## Screenshots

### Dashboard

<img width="1920" height="1200" alt="Screenshot (932)" src="https://github.com/user-attachments/assets/9c3dbd8e-f7c8-40e1-8779-b117cb6f4e11" />


### Employee List

<img width="1920" height="1200" alt="image" src="https://github.com/user-attachments/assets/c0677fb8-0009-46d2-a933-19e1ee4e6647" />


### Employee Form

<img width="1920" height="1200" alt="Screenshot (933)" src="https://github.com/user-attachments/assets/f80c813c-9830-4fff-ba81-39fcd8eb82b2" />

### Service Management

<img width="1920" height="1200" alt="image" src="https://github.com/user-attachments/assets/94c5ee06-28fa-4b2b-9f78-fe27f12d0c33" />
<img width="1920" height="1200" alt="image" src="https://github.com/user-attachments/assets/0cbdea11-836c-41f9-91b2-ebd2af940522" />
<img width="1920" height="1200" alt="image" src="https://github.com/user-attachments/assets/69f1a936-feab-4f41-b0aa-e9db1c31a3cd" />



### Appointment Booking

<img width="1920" height="1200" alt="image" src="https://github.com/user-attachments/assets/4490f1e1-453e-4574-b5d1-f5e8023ab0a1" />


### Dashboard Charts

<img width="1920" height="1200" alt="image" src="https://github.com/user-attachments/assets/dd2445a3-912a-4fba-9763-20002930dcf3" />


## Future Improvements

Some features that can be added in the future include:

- User authentication and authorization
- Email notifications
- SMS appointment reminders
- Export data to Excel
- Employee attendance management
- Role-based access control

## What I Learned

Developing this project helped me improve my understanding of ASP.NET Core MVC and how different parts of an application work together. I gained practical experience using Dapper for database operations, SQL Server for data management, implementing CRUD functionality, managing relationships between entities, and building a responsive user interface. I also learned how to organize a project using the MVC architecture and write cleaner, more maintainable code.

## Author

**Bidushi Gautam**
