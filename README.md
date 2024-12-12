# TimesheetPlayground

## Architecture

**3-Layer Architecture**

- Data Access Layer: .NET Core 8 Project
  - Data First approach
- Business Logic Layer - integrated in UI project
- User Interface Layer: ASP .NET Core Web Application - MVC
  - Model - View - Controller

**Test project**

- XUnit 2.9

**Database**
- MSSQL Server - Express 16.x

## Required NuGet Packages

- Entity Famework Core 9
- Automapper 13

## Install
- Create a new database (TimesheetPlayground) 
- Execute the build SQL found in root database folder (database.sql)
- Set the connection string found in TimesheetPlaygroundContext.cs
- Build the application
- Enjoy 

## Users
- All users' password: 111
- Manager user
  - Username: Manager
- Employee user
  - Username: User (4 timesheets)
  - Username: User2 (1 timesheet)
  - Username: User3 (without timesheets)

