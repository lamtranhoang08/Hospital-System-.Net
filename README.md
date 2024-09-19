# Hospital-System-.Net

## Overview
The Hospital System is a console-based application developed using C# and .NET 6, designed to manage hospital operations. It provides functionalities for managing patient and doctor information, scheduling appointments, and maintaining data persistence between sessions.

## Features

### Patient Management
- View personal details
- List and manage registered doctors
- Book and view appointments

### Doctor Management
- View personal details
- List and manage registered patients
- View and manage appointments

### Data Persistence
- Information is stored in text files to maintain data across sessions.

## Requirements
- .NET 6 SDK must be installed on your machine.

## Installation

### Clone or Download the Project
1. Clone the repository or download the ZIP file containing the project.

### Build the Project
1. Open a terminal or command prompt.
2. Navigate to the project directory where the `.csproj` file is located.
3. Execute the following command to build the project:

   ```bash
   dotnet build
### Running the Application
Navigate to the bin/debug/net6.0 directory where the .exe file is located.
Execute the program:
On Windows, double-click the .exe file or run it from the command prompt:
  ```bash
  .\HospitalSystem.exe
```


### Contributing
Contributions are welcome! To contribute:

Fork the repository.
Make your changes and improvements.
Submit a pull request with a description of your modifications.

## Data Files
- `credentials.txt`: Contains user credentials for login.
- `registeredPatients.txt`: Stores patient information.
- `doctors.txt`: Contains doctor information.

If you encounter any issues or have questions, please refer to the [Contributing](#contributing) section or contact the project maintainers.
