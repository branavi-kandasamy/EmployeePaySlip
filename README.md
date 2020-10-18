# EmployeePaySlip

This is developed in .NET Core 2.2. Please make sure target framework is v2.2 when running the application

# Installation
- Clone the repo
- Restore the .nuget packages using below cmd

    `dotnet restore;`
- Run the application, Please make sure that you run the cmd in project directory

    `dotnet run --framework=netcoreapp2.2`
- Run the unit tests, Please make sure that you run the cmd in Test project directory

    `dotnet test --framework=netcoreapp2.2`

# Assumptions
- Tax data is stored in a csv file. Path of that file is defined in appSettings.json
- Output of payslips is generated in a csv file, that path is also configurable in appSettings.json
