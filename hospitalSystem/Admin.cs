using System;
using System.Collections.Generic;
using System.hospitalSystem;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospitalSystem
{
    public class Admin : User
    {
        public List<Patient> Patients { get; set; }
        //List of all patients in the system
        public List<Doctor> Doctors { get; set; }
        //List of all doctors in the system

        // Admin constructor initializes the role as "admin" and lists of patients and doctors
        public Admin(string id, string name, string address, string email, string phone)
        : base(id, name, address, email, phone, "admin")
        {
            Patients = new List<Patient>();
            Doctors = new List<Doctor>();
        }

        // Displays the admin menu and processes user input for various actions
        public void printAdminMenu(Program p)
        {
            bool stayInMenu = true;
            while (stayInMenu)
            {
                Console.Clear();
                Console.WriteLine(" ______________________________________________________________ ");
                Console.WriteLine(" |                DOTNET Hospital Management System            |");
                Console.WriteLine(" |____________________________________________________________|");
                Console.WriteLine(" |                        ADMIN Menu                          |");
                Console.WriteLine(" |____________________________________________________________|");
                Console.WriteLine($" | WELCOME TO DOTNET HOSPITAL SYSTEM, {this.Name,-34} |");
                Console.WriteLine(" |____________________________________________________________|");
                Console.WriteLine(" | Please choose an option:                                    |");
                Console.WriteLine(" | 1. List all doctors                                         |");
                Console.WriteLine(" | 2. Check doctor details                                     |");
                Console.WriteLine(" | 3. List all patients                                        |");
                Console.WriteLine(" | 4. Check patient details                                    |");
                Console.WriteLine(" | 5. Add doctor                                               |");
                Console.WriteLine(" | 6. Add patient                                              |");
                Console.WriteLine(" | 7. Log out                                                  |");
                Console.WriteLine(" | 8. Exit                                                     |");
                Console.WriteLine(" |_____________________________________________________________|");
                Console.Write("Your choice: ");

                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        listAllDoctors(p);
                        break;
                    case '2':
                        checkDoctorDetails(p);
                        break;
                    case '3':
                        listAllPatients(p);
                        break;
                    case '4':
                        checkPatientDetails(p);
                        break;
                    case '5':
                        addDoctor(p);
                        break;
                    case '6':
                        addPatient(p);
                        break;
                    case '7':
                        stayInMenu = false;
                        p.LoginMenu(); // Return to login
                        return;
                    case '8':
                        Console.WriteLine("Exiting system...");
                        Environment.Exit(0); // Close application
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // List all doctors in the system
        public void listAllDoctors(Program p)
        {
            Console.Clear();
            Console.WriteLine(" _______________________________________________________________ ");
            Console.WriteLine(" |                DOTNET Hospital Management System            |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine(" |                        ALL DOCTORS                          |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine("\n All doctors registered to the DOTNET HOSPITAL system\n");
            Console.WriteLine(" | Name            | Email Address             | Phone       | Address           |");
            Console.WriteLine(" |-----------------|---------------------------|-------------|-------------------|");

            foreach (var doctor in p.GetAllDoctors())
            {
                Console.WriteLine(doctor.ToString());
            }

            Console.ReadKey();
        }

        // Check specific doctor details by ID
        public void checkDoctorDetails(Program p)
        {
            Console.Clear();
            Console.WriteLine(" _______________________________________________________________ ");
            Console.WriteLine(" |                DOTNET Hospital Management System            |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine(" |                       DOCTOR DETAILS                        |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine("Please enter the ID of the doctor to check. Or press 'n' to return to menu:");

            string input = Console.ReadLine();
            if (input.ToLower() == "n") return;

            var doctor = p.GetAllDoctors().FirstOrDefault(d => d.ID == input);
            if (doctor != null)
            {
                Console.WriteLine(" | Name            | Email Address             | Phone       | Address           |");
                Console.WriteLine(" |-----------------|---------------------------|-------------|-------------------|");
                Console.WriteLine(doctor.ToString());
            }
            else
            {
                Console.WriteLine("Doctor not found!");
            }

            Console.ReadKey();
        }

        // List all patients in the system
        public void listAllPatients(Program p)
        {
            Console.Clear();
            Console.WriteLine(" _________________________________________________________________________________________ ");
            Console.WriteLine(" |                        DOTNET Hospital Management System                              |");
            Console.WriteLine(" |_______________________________________________________________________________________|");
            Console.WriteLine(" |                                     ALL PATIENTS                                      |");
            Console.WriteLine(" |_______________________________________________________________________________________|");
            Console.WriteLine("\n All patients registered to the DOTNET HOSPITAL system\n");
            Console.WriteLine(" | Name            | Address              | Email Address               | Phone        |");
            Console.WriteLine(" |-----------------|----------------------|-----------------------------|--------------|");

            foreach (var patient in p.GetAllPatients())
            {
                Console.WriteLine(patient.ToString());
            }

            Console.ReadKey();
        }

        // Check specific patient details by ID
        private void checkPatientDetails(Program p)
        {
            Console.Clear();
            Console.WriteLine(" _______________________________________________________________ ");
            Console.WriteLine(" |                DOTNET Hospital Management System            |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine(" |                        PATIENT DETAILS                      |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine("Please enter the ID of the patient to check. Or press 'n' to return to menu:");

            string input = Console.ReadLine();
            if (input.ToLower() == "n") return;

            var patient = p.GetAllPatients().FirstOrDefault(p => p.ID == input);
            if (patient != null)
            {
                Console.WriteLine(" | Name            | Address              | Email Address               | Phone        |");
                Console.WriteLine(" |-----------------|----------------------|-----------------------------|--------------|");
                Console.WriteLine(patient.ToString());
            }
            else
            {
                Console.WriteLine("Patient not found!");
            }

            Console.ReadKey();
        }

        // Add a new doctor to the system
        private void addDoctor(Program p)
        {
            Console.Clear();
            Console.WriteLine(" _______________________________________________________________ ");
            Console.WriteLine(" |                DOTNET Hospital Management System            |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine(" |                        ADD DOCTOR                           |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine("\n Registering a new doctor with the system\n");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Phone: ");
            string phone = Console.ReadLine();
            Console.Write("Address: ");
            string address = Console.ReadLine();

            string fullName = $"{firstName} {lastName}";
            string doctorID = generateNewDoctorID();
            Doctor newDoctor = new Doctor(doctorID, fullName, address, email, phone);

            p.doctors.Add(newDoctor);
            string randomPassword = GenerateRandomPassword();

            // Append doctor credentials to file
            try
            {
                string filePath = "credentials.txt";
                string doctorData = $"{doctorID}|{randomPassword}|{fullName}|{address}|{email}|{phone}|doctor";

                 // Check if the file exists and if it ends with a newline
                 if (File.Exists(filePath))
                    {
                         string fileContent = File.ReadAllText(filePath);

                         // If the file does not end with a newline, add one before appending new data
                         if (!fileContent.EndsWith(Environment.NewLine))
                                 {
                                     File.AppendAllText(filePath, Environment.NewLine);
                                 }
                    }
                 // Append the new doctor data
                 File.AppendAllText(filePath, doctorData + Environment.NewLine);
                 Console.WriteLine($"{fullName} added to the system with password: {randomPassword}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error writing to the file: " + ex.Message);
                            }
                
                            Console.ReadKey();
            }

        // Generate a random doctor ID
        private string generateNewDoctorID()
        {
            Random rnd = new Random();
            int doctorID = rnd.Next(10000, 99999); // Generate a random 5-digit ID
            return doctorID.ToString();
        }

        // Add a new patient to the system
        private void addPatient(Program p)
        {
            Console.Clear();
            Console.WriteLine(" _______________________________________________________________ ");
            Console.WriteLine(" |                DOTNET Hospital Management System            |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine(" |                        ADD PATIENT                          |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine("\n Registering a new patient with the system\n");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Phone: ");
            string phone = Console.ReadLine();
            Console.Write("Address: ");
            string address = Console.ReadLine();

            string fullName = $"{firstName} {lastName}";
            string patientID = GenerateNewPatientID();
            Patient newPatient = new Patient(patientID, fullName, address, email, phone);

            p.patients.Add(newPatient);
            string randomPassword = GenerateRandomPassword();

            // Append patient credentials to file
            try
            {
                string filePath = "credentials.txt";
                string doctorData = $"{patientID}|{randomPassword}|{fullName}|{address}|{email}|{phone}|patient";

                 // Check if the file exists and if it ends with a newline
                if (File.Exists(filePath))
                     {
                         string fileContent = File.ReadAllText(filePath);

                         // If the file does not end with a newline, add one before appending new data
                         if (!fileContent.EndsWith(Environment.NewLine))
                     {
                         File.AppendAllText(filePath, Environment.NewLine);
                     }
            }

 // Append the new doctor data
 File.AppendAllText(filePath, doctorData + Environment.NewLine);
 Console.WriteLine($"{fullName} added to the system with password: {randomPassword}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to the file: " + ex.Message);
            }

            Console.ReadKey();
        }

        // Generate a random patient ID
        private string GenerateNewPatientID()
        {
            Random rnd = new Random();
            int patientID = rnd.Next(10000, 99999); // Generate a random 5-digit ID
            return patientID.ToString();
        }

        // Generate a random password
        private string GenerateRandomPassword(int length = 8)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            StringBuilder password = new StringBuilder();
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                password.Append(validChars[rnd.Next(validChars.Length)]);
            }

            return password.ToString();
        }
    }
}
