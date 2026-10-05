# Food Court Management System

A desktop-based food court management application developed with **C# Windows Forms** and **Oracle Database**. The system provides separate workflows for customers, staff, administrators, and food-court owners.

## Features

- Welcome page and role selection
- Customer dashboard
- Staff login
- Food-stall selection
- Menu browsing and inspection
- Food ordering
- Order confirmation
- Payment workflow
- bKash payment screen
- Order history
- Employee information management
- Staff management
- Food-stall management
- Menu management
- Administrator dashboard
- Owner and administrative operations
- Oracle database integration

## User Roles

### Customer

Customers can:

- Select a food stall
- View available menu items
- Place food orders
- Confirm orders
- Complete the payment process
- View order history

### Staff

Staff members can:

- Log in to the application
- View employee information
- Inspect food stalls and menus
- Access staff-related operations

### Administrator

Administrators can:

- View food-stall records
- View employee records
- View order records
- View staff records
- View menu-item records
- Manage food-stall and staff information

### Owner

Owners can access food-court management information and review operational data.

## Technology Stack

- **Programming Language:** C#
- **Framework:** .NET Framework 4.8
- **Application Type:** Windows Forms desktop application
- **Database:** Oracle Database
- **Database Driver:** Oracle Managed Data Access
- **IDE:** Visual Studio
- **Project Type:** Visual Studio `.sln` and `.csproj` project

## Project Structure

| File or Module | Description |
|---|---|
| `Program.cs` | Application entry point |
| `welcome_page.cs` | Initial welcome screen |
| `role_selection.cs` | Customer and staff role selection |
| `staff_login.cs` | Staff login screen |
| `customer_dashboard.cs` | Customer dashboard |
| `stall_selection.cs` | Food-stall selection |
| `food_stall.cs` | Food-stall management |
| `menu_inspection.cs` | Menu inspection |
| `employee.cs` | Employee information |
| `employee_inspection.cs` | Employee inspection |
| `staff.cs` | Staff-related functionality |
| `admin.cs` | Administrator dashboard |
| `admin_employee.cs` | Employee administration |
| `admin_menu.cs` | Menu administration |
| `admin_orders.cs` | Order administration |
| `admin_owner.cs` | Owner administration |
| `admin_staffs.cs` | Staff administration |
| `payment.cs` | Payment workflow |
| `bkash.cs` | bKash payment screen |
| `confirmation.cs` | Order confirmation |
| `order_history.cs` | Previous order history |
| `thanks.cs` | Completion screen |
| `App.config` | Application and database configuration |
| `Database.DMP` | Oracle database dump |
| `*.Designer.cs` | Windows Forms designer-generated code |
| `*.resx` | Windows Forms resources |
| `packages.config` | NuGet package configuration |

## Requirements

Before running the project, install:

- Windows operating system
- Visual Studio
- .NET Framework 4.8 Developer Pack
- Oracle Database or Oracle Database Express Edition
- Oracle SQL Developer or another Oracle database tool

## Database Configuration

The application uses Oracle Database through `Oracle.ManagedDataAccess`.

The default configuration expects an Oracle database running with:

- **Host:** `localhost`
- **Port:** `1521`
- **Service name:** `XE`
- **Database user:** `food_court`

The repository includes an Oracle database dump:

```text
Database.DMP
```

### Database Setup

1. Start the Oracle Database service.
2. Create the required database user.
3. Import `Database.DMP` into Oracle.
4. Confirm that the required tables exist.
5. Update the database connection string in `App.config`.
6. Update any additional connection strings used in the C# source files.

Example connection string:

```text
Data Source=(DESCRIPTION=
  (ADDRESS_LIST=
    (ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))
  )
  (CONNECT_DATA=(SERVICE_NAME=XE))
);
User Id=YOUR_USERNAME;
Password=YOUR_PASSWORD;
```

> Do not commit real database credentials to the repository.

## Database Tables

The application uses tables such as:

- `food_stall`
- `employee`
- `orders`
- `staffs`
- `menu_item`

The exact database schema can be found in the included project report and Oracle database dump.

## Installation

Clone the repository:

```bash
git clone https://github.com/dipaneetapramanik/Food-Court-Management-System--using-C-sharp-Language.git
```

Open the project directory:

```bash
cd Food-Court-Management-System--using-C-sharp-Language
```

Then open the solution file in Visual Studio:

```text
Food Court Management System.sln
```

Restore the NuGet packages and build the solution.

## Running the Application

1. Start Oracle Database.
2. Configure the database connection.
3. Open `Food Court Management System.sln` in Visual Studio.
4. Restore NuGet packages.
5. Build the project.
6. Run the application with `F5`.

The application starts from the welcome page.

## Application Flow

```text
Welcome Page
     |
     v
Role Selection
     |
     +--> Staff Login
     |        |
     |        +--> Staff Dashboard
     |        +--> Administrator Dashboard
     |
     +--> Customer Flow
              |
              v
        Stall Selection
              |
              v
        Menu Selection
              |
              v
            Order
              |
              v
           Payment
              |
              v
        Confirmation
              |
              v
        Order History
```

## NuGet Packages

The project uses the following packages:

- `Oracle.ManagedDataAccess`
- `Microsoft.Bcl.AsyncInterfaces`
- `System.Buffers`
- `System.Memory`
- `System.Numerics.Vectors`
- `System.Runtime.CompilerServices.Unsafe`
- `System.Text.Json`
- `System.Threading.Tasks.Extensions`
- `System.ValueTuple`

Package versions are listed in `packages.config`.

## Security Considerations

This project is intended primarily for educational and demonstration purposes.

For production use, consider:

- Removing hard-coded database credentials.
- Storing secrets in environment variables or a secure secret manager.
- Hashing user passwords.
- Adding stronger authentication and authorization.
- Validating all user input.
- Using parameterized SQL queries throughout the application.
- Protecting payment-related functionality.
- Removing private certificate files from source control.
- Adding audit logs and improved exception handling.

## Known Limitations

- The application requires Windows.
- The project targets .NET Framework 4.8.
- Oracle Database must be installed and configured separately.
- Some database connection settings may need to be updated manually.
- The project does not currently include automated tests.
- Payment functionality should not be used as a production payment integration without additional security and gateway configuration.

## Future Improvements

Possible improvements include:

- Migrating to modern .NET.
- Adding password hashing.
- Implementing complete CRUD operations.
- Adding order-status tracking.
- Adding inventory management.
- Adding sales and revenue reports.
- Adding search and filtering features.
- Improving validation and exception handling.
- Adding automated unit and integration tests.
- Using a centralized database access layer.
- Replacing hard-coded credentials with secure configuration.
- Integrating a secure payment gateway.

## Academic Project

The repository includes the following project report:

```text
ADBMS, Project Report, Section-C, Group-2.pdf
```

This report contains additional information about the database design and project implementation.

## Contributing

Contributions are welcome.

1. Fork the repository.
2. Create a feature branch:

   ```bash
   git checkout -b feature/your-feature-name
   ```

3. Make your changes.
4. Test the application.
5. Commit your changes:

   ```bash
   git commit -m "Describe your changes"
   ```

6. Push your branch:

   ```bash
   git push origin feature/your-feature-name
   ```

7. Open a pull request.

## License

No license has currently been specified for this repository.

If you want others to use, modify, or distribute this project, add an appropriate open-source license.
