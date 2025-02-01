[![Build and Test](https://github.com/joha546/Unity-Donors/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/joha546/Unity-Donors/actions/workflows/ci-cd.yml)
# Unity Donors

## Overview

**Unity Donors** is a web-based blood donation management system designed to connect blood donors with recipients efficiently. The platform enables users to register, request blood, and 
find suitable donors based on their location and blood group. The system incorporates role-based access, allowing different types of users such as donors, seekers, admins, and hospitals, blood banks 
to interact seamlessly via e-mail and mobile sms.

## Table of Contents

- [Project Requirements](#project-requirements)
- [Technologies Used](#technologies-used)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage Overview](#usage-overview)
- [Troubleshooting and Debugging](#troubleshooting-and-debugging)
- [Contributing](#contributing)
- [Code of Conduct](#code-of-conduct)
- [Additional Resources](#additional-resources)
- [License](#license)
- [Contact](#contact)

## Project Requirements

### Software Requirements
- **Operating System**: Windows 7, 8, 8.1, 10, or Windows Server 2008 and later
- **Development Environment**: Microsoft Visual Studio 2010 or later
- **Database**: Microsoft SQL Server 2019 or later
- **MVC Version**: MVC5
- **Framework**: .NET Framework 4.7.2
- **Web Server**: IIS Express
- **Browser**: Web browsers with JavaScript enabled

### Dependencies
- ASP.NET MVC Framework
- Entity Framework
- SQL Server Management Studio (SSMS)
- Stripe API for payment integration (optional)

## Technologies Used

- **Frontend**: HTML, CSS, JavaScript, Bootstrap
- **Backend**: C# (.NET Framework)
- **Database**: Microsoft SQL Server
- **Version Control**: Git & GitHub
- **API Integrations**: Stripe (for donations)

## Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/joha546/Unity-Donors.git
   cd Unity-Donors
   ```

2. **Set Up the Database**:
   - Open SQL Server Management Studio (SSMS).
   - Create a new database on your SSMS.
   - Go to Database Layer dependency on Unity-Donors Project, Update the connection string and make migration.
   - Update connection strings in `web.config` to match your local database setup.

3. **Open the Project in Visual Studio**:
   - Open `UnityDonors.sln` in Visual Studio.
   - Restore NuGet dependencies (`Tools > NuGet Package Manager > Restore Packages`).

4. **Build and Run the Project**:
   - Set the project startup file to `UnityDonors.Web`.
   - Press `F5` to run the application.

## Configuration

### Database Configuration
- Ensure that SQL Server is running.
- Check `web.config` and update:
  ```xml
  <connectionStrings>
    <add name="DefaultConnection"
         connectionString="Server=YOUR_SERVER;Database=UnityDonorsDB;Trusted_Connection=True;"
         providerName="System.Data.SqlClient" />
  </connectionStrings>
  ```

### API Keys
- If using Stripe for donations, add your API keys in `appsettings.json`:
  ```json
  {
    "Stripe": {
      "PublishableKey": "your_publishable_key",
      "SecretKey": "your_secret_key"
    }
  }
  ```

## Usage Overview

### For Donors
1. Register and log in to the system.
2. Browse through blood donation requests.
3. Contact recipients and schedule donations.

### For Recipients/Seekers
1. Create a blood donation request.
2. View matched donors and sent request to them.
3. After responding by the requested donor or blood bank, contact with them.

### For Admins
1. Manage user roles and permissions.
2. Monitor donation activities and generate reports.

### For Volunteers
1. Assist in blood donation campaigns.
2. Post informative blogs about blood donation.

## Troubleshooting and Debugging

### Common Issues & Solutions

1. **Database Connection Issues**:
   - Ensure SQL Server is running.
   - Check connection strings in `web.config`.
   - Run the following command to check if the SQL Server service is active:
     ```powershell
     Get-Service -Name "MSSQLSERVER"
     ```

2. **IIS Express Not Running**:
   - Restart IIS Express in Visual Studio (`Ctrl + F5`).
   - Run Visual Studio as Administrator.

3. **Missing Dependencies**:
   - Run `Tools > NuGet Package Manager > Manage NuGet Packages for Solution` and install missing packages.

4. **Port Conflict**:
   - Find active ports using:
     ```powershell
     netstat -ano | findstr :5000
     ```
   - Kill the process using:
     ```powershell
     taskkill /PID <PID> /F
     ```

## Contributing

We welcome contributions! Please read [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines on how to contribute to Unity Donors.

1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature/YourFeature
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add YourFeature"
   ```
4. Push the branch:
   ```bash
   git push origin feature/YourFeature
   ```
5. Open a pull request.

## Code of Conduct

Please read our [Code of Conduct](CODE_OF_CONDUCT.md) to understand the standards we uphold.

## Additional Resources
- **ASP.NET MVC Documentation**: [https://learn.microsoft.com/en-us/aspnet/mvc/overview/](https://learn.microsoft.com/en-us/aspnet/mvc/overview/)
- **SQL Server Documentation**: [https://learn.microsoft.com/en-us/sql/sql-server/](https://learn.microsoft.com/en-us/sql/sql-server/)
- **Stripe API Guide**: [https://stripe.com/docs/api](https://stripe.com/docs/api)

## License

This project is licensed under the [MIT License](LICENSE).

## Contact

For questions or support, contact the project maintainer:
- **Md Khaled Bin Joha** - [GitHub](https://github.com/joha546) | [LinkedIn](https://www.linkedin.com/in/mdkhaledbinjoha/)

