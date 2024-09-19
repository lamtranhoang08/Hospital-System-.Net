# Hospital-System-.Net
Overview
The Hospital System is a console-based application developed using C# and .NET 6, designed to manage hospital operations. It provides functionalities for managing patient and doctor information, scheduling appointments, and maintaining data persistence between sessions.

Features
Patient Management:

View personal details
List and manage registered doctors
Book and view appointments
Doctor Management:

View personal details
List and manage registered patients
View and manage appointments
Data Persistence:

Information is stored in text files to maintain data across sessions.
Requirements
.NET 6 SDK must be installed on your machine.
Installation
Clone or Download the Project:

Clone the repository or download the ZIP file containing the project.
Build the Project:

Open a terminal or command prompt.
Navigate to the project directory where the .csproj file is located.
Execute the following command to build the project:
bash
dotnet build
Locate the Executable:

After building, the executable file will be available in the bin>debug>net6.0 directory.
Running the Application
Navigate to the bin>debug>net6.0 directory where the .exe file is located.

Execute the program:

On Windows, double-click the .exe file or run it from the command prompt:
bash
.\HospitalSystem.exe
Usage
Login: Start the application and log in as either a patient or a doctor.

Patient Menu:

View personal details
List and manage registered doctors
Book appointments
View appointment history
Doctor Menu:

View personal details
List registered patients
View and manage appointments
Check patient details and appointments
Data Files
credentials.txt: Contains user credentials for login.
registeredPatients.txt: Stores patient information.
doctors.txt: Contains doctor information.
Contributing
Contributions are welcome! To contribute:

Fork the repository.
Make your changes and improvements.
Submit a pull request with a description of your modifications.
License
This project is licensed under the MIT License. See the LICENSE file for details.
