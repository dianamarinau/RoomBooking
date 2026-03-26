# RoomBooking API

A RESTful API for managing room reservations, built with ASP.NET Core and Entity Framework Core.

## Tech Stack
- **Backend:** C# / ASP.NET Core
- **ORM:** Entity Framework Core
- **Database:** SQL Server

## Features
- Room booking management
- User management
- Overlap validation — prevents double-booking of the same room at the same time

## Database Schema
- **Users** — stores user information
- **Rooms** — stores available rooms
- **Bookings** — stores reservations linked to users and rooms

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [SSMS](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

### Setup
1. Clone the repository
```bash
   git clone https://github.com/dianamarinau/RoomBooking.git
```
2. Restore the database by running `database/RoomBooking_schema.sql` in SSMS
3. Update the connection string in `appsettings.json`:
```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=RoomBooking;Trusted_Connection=True;"
   }
```
4. Run the API:
```bash
   cd RoomBooking.API
   dotnet run
```