using System.Data;
using System.hospitalSystem;
using System.Linq;
using System.Numerics;
using System.Transactions;

namespace hospitalSystem
{
    public class Program
    {
        // Collections to hold lists of patients, doctors, admins, and appointments
        public List<Patient> patients = new List<Patient>();
        public List<Doctor> doctors = new List<Doctor>();
        public List<Admin> admins = new List<Admin>();
        private List<Appointment> appointments = new List<Appointment>();

        // Current logged-in user
        private User currentUser;

        // Constructor to initialize the program and load stored data
        public Program()
        {
            retrieveData(); // Loads user data from the file
        }

        public static void Main(string[] args)
        {
            Program programInstance = new Program(); // Create an instance of the program
            bool exitSystem = false;
            while (!exitSystem)
            {
                programInstance.LoginMenu(); // Continuously show the login menu until exit
            }
        }

        // Method to retrieve user data from a file (credentials.txt)
        public void retrieveData()
        {
            string[] loginCredentials = File.ReadAllLines("credentials.txt");
            foreach (var credentialsItem in loginCredentials)
            {
                string[] login = credentialsItem.Split('|').Select(s => s.Trim()).ToArray();

                if (login.Length == 7 &&
                    login[6].Trim().ToLower() == "doctor")
                {
                    // Retrieve user information dynamically
                    string userId = login[0].Trim();
                    string userPassword = login[1].Trim();
                    string userName = login[2].Trim();
                    string userAddress = login[3].Trim();
                    string userEmail = login[4].Trim();
                    string userPhone = login[5].Trim();
                    //int userPhone = int.Parse(login[5].Trim());
                    Doctor doctor1 = new Doctor(userId, userName, userAddress, userEmail, userPhone);
                    doctors.Add(doctor1);
                }
                else if (login.Length == 7 &&
                    login[6].Trim().ToLower() == "patient")
                {
                    string userId = login[0].Trim();
                    string userPassword = login[1].Trim();
                    string userName = login[2].Trim();
                    string userAddress = login[3].Trim();
                    string userEmail = login[4].Trim();
                    string userPhone = login[5].Trim();
                    //int userPhone = int.Parse(login[5].Trim());
                    Patient patient1 = new Patient(userId, userName, userAddress, userEmail, userPhone);
                    patients.Add(patient1);

                }
                else if (login.Length == 7 &&
                    login[6].Trim().ToLower() == "admin")
                {
                    string userId = login[0].Trim();
                    string userPassword = login[1].Trim();
                    string userName = login[2].Trim();
                    string userAddress = login[3].Trim();
                    string userEmail = login[4].Trim();
                    string userPhone = login[5].Trim();
                    Admin admin1 = new Admin(userId, userName, userAddress, userEmail, userPhone);
                    admins.Add(admin1);

                }

            }
        }

        // Getter methods for retrieving all patients and doctors
        public List<Patient> GetAllPatients() { return patients; }
        public List<Doctor> GetAllDoctors() { return doctors; }

        public void LoginMenu()
        {
            bool loginSuccessful = false;

            while (!loginSuccessful)
            {
                Console.Clear();
                printLoginMenu();

                Console.SetCursorPosition(7, 7);
                string id = Console.ReadLine();

                Console.SetCursorPosition(12, 8);
                string password = readPassword(); // Masked password input

                Console.WriteLine();

                if (File.Exists("credentials.txt"))
                {
                    string[] loginCredentials = File.ReadAllLines("credentials.txt");
                    try
                    {
                        loginSuccessful = loginCredentials.Any(loginCredential =>
                        {
                            string[] login = loginCredential.Split('|').Select(s => s.Trim()).ToArray();

                            if (login.Length == 7 &&
                                    string.Equals(id, login[0], StringComparison.OrdinalIgnoreCase) &&
                                    string.Equals(password, login[1], StringComparison.OrdinalIgnoreCase))
                            {
                                // Retrieve user information dynamically
                                string role = login[6].Trim();

                                // Create the corresponding user based on the role
                                switch (role.ToLower())
                                {
                                    case "patient":
                                        var patient = patients.FirstOrDefault(p => p.ID == id);
                                        if (patient != null)
                                        {
                                            Console.Clear();
                                            patient.printPatientMenu(this);
                                        }
                                        break;
                                    case "doctor":
                                        var doctor = doctors.FirstOrDefault(p => p.ID == id);
                                        if (doctor is not null)
                                        {
                                            Console.Clear();
                                            doctor.printDoctorMenu(this);
                                        }
                                        break;
                                    case "admin":
                                        var admin = admins.FirstOrDefault(p => p.ID == id);
                                        if(admin is not null)
                                        {
                                            Console.Clear();
                                            admin.printAdminMenu(this);
                                        }
                                        break;

                                    default:
                                        Console.WriteLine("Unknown role. Please contact the administrator.");
                                        Console.ReadKey();
                                        loginSuccessful = false;
                                        return false;
                                }
                                return true; // Break out of the loop once login is successful
                            }
                            return false;
                        });

                        if (loginSuccessful && currentUser != null)
                        {
                            // Display corresponding menu
                            if (currentUser is Patient patient)
                            {
                                Console.Clear();
                                patient.printPatientMenu(this);
                            }
                            else if (currentUser is Doctor doctor)
                            {
                                Console.Clear();
                                doctor.printDoctorMenu(this);
                            }
                            else if (currentUser is Admin admin)
                            {
                                Console.Clear();
                                admin.printAdminMenu(this);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Credentials. Please try again.");
                            Console.ReadKey();
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("Error: credentials.txt does not contain login credentials in the correct format.");
                        Console.Write("Press any key to exit...");
                        Console.ReadKey();
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("No such file found!");
                    Console.Write("Press any key to exit...");
                    Console.ReadKey();
                    return;
                }
            }
            return; 
        }
        // Method to read masked password input and returns the masked one
        private string readPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            while (true)
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                    break;

                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Remove(password.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            }
            return password;
        }


        //This method prints out the main menu
        public void printLoginMenu()
        {
            Console.WriteLine(" _____________________________________________________________");
            Console.WriteLine(" |             DOTNET Hospital Management System             |");
            Console.WriteLine(" |_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _|");
            Console.WriteLine(" |                                                           |");
            Console.WriteLine(" |                           Login                           |");
            Console.WriteLine(" |___________________________________________________________|");
            Console.WriteLine(" |                                                           |");
            Console.WriteLine(" |ID:                                                        |");
            Console.WriteLine(" |Password:                                                  |");
            Console.WriteLine(" |                                                           |");
            Console.WriteLine(" |___________________________________________________________|");
            Console.WriteLine();
        }

    }

}
