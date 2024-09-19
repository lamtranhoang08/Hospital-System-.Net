using System.hospitalSystem;

namespace hospitalSystem
{
    public class Doctor : User
    {
        // List of patients registered with the doctor
        public List<Patient> registeredPatients { get; set; }
        // List of appointments scheduled with the doctor
        public List<Appointment> Appointments { get; set; }

        // Constructor to initialize the doctor with their details
        public Doctor(string id, string name, string address, string email, string phone)
            : base(id, name, address, email, phone, "doctor")
        {
            registeredPatients = new List<Patient>();
            Appointments = new List<Appointment>();
        }

        // Method to display the doctor's menu and handle input choices
        public void printDoctorMenu(Program p)
        {
            bool stayInMenu = true;
            while (stayInMenu)
            {
                Console.Clear();
                Console.WriteLine(" _____________________________________________________________");
                Console.WriteLine(" |             DOTNET Hospital Management System             |");
                Console.WriteLine(" |_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _|");
                Console.WriteLine(" |                                                           |");
                Console.WriteLine(" |                      Doctor Menu                          |");
                Console.WriteLine(" |___________________________________________________________|");
                Console.WriteLine($" | WELCOME TO DOTNET HOSPITAL SYSTEM, {this.Name,-34} |");
                Console.WriteLine(" |____________________________________________________________|");
                Console.WriteLine(" | Please choose an option:                                   |");
                Console.WriteLine(" |------------------------------------------------------------|");
                Console.WriteLine(" | 1. List doctor details                                     |");
                Console.WriteLine(" | 2. List patients                                           |");
                Console.WriteLine(" | 3. List all appointments                                   |");
                Console.WriteLine(" | 4. Check particular patient                                |");
                Console.WriteLine(" | 5. List appointments with patient                          |");
                Console.WriteLine(" | 6. Log out (Return to login)                               |");
                Console.WriteLine(" | 7. Exit system                                             |");
                Console.WriteLine(" |____________________________________________________________|");
                Console.Write("Your choice: ");

                // Get the user's choice
                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                // Handle the user's choice with a switch statement
                switch (choice)
                {
                    case '1':
                        listMyDetails();
                        break;
                    case '2':
                        listPatients();
                        break;
                    case '3':
                        listAppointments();
                        break;
                    case '4':
                        checkPatient();
                        break;
                    case '5':
                        appointmentWithPatient();
                        break;
                    case '6':
                        stayInMenu = false;
                        p.LoginMenu(); // Return to login menu
                        return;
                    case '7':
                        // Exit system
                        Console.WriteLine("Exiting system...");
                        Environment.Exit(0); // Closes the application
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // Override the ToString() method to display doctor details in a formatted way
        public override string ToString()
        {
            return $"  {Name,-16}  {Email,-27}  {Phone,-16} {Address,-18} ";
        }

        // Method to list the doctor's personal details
        public void listMyDetails()
        {
            Console.Clear();
            Console.WriteLine(" _______________________________________________________________");
            Console.WriteLine(" |                                                             |");
            Console.WriteLine(" |                DOTNET Hospital Management System            |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine(" |                                                             |");
            Console.WriteLine(" |                        My Details                           |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine("Doctor Details:");
            Console.WriteLine($"Name: {this.Name}");
            Console.WriteLine($"Address: {this.Address}");
            Console.WriteLine($"Email: {this.Email}");
            Console.WriteLine($"Phone: {this.Phone}");
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        // Method to list all patients registered with the doctor
        public void listPatients()
        {
            Console.Clear();
            Console.WriteLine(" _______________________________________________________________");
            Console.WriteLine(" |                                                             |");
            Console.WriteLine(" |                DOTNET Hospital Management System            |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine(" |                                                             |");
            Console.WriteLine(" |                        My Patients                          |");
            Console.WriteLine(" |_____________________________________________________________|");

            if (registeredPatients.Count == 0)
            {
                Console.WriteLine("No patients are currently registered with Dr. " + this.Name);
            }
            else
            {
                Console.WriteLine($"Patients registered with Dr. {this.Name}:");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine(" | Patient Name      |       Address          | Email               | Phone       |");
                Console.WriteLine(" |-------------------|------------------------|---------------------|-------------|");
                foreach (var patient in registeredPatients)
                {
                    Console.WriteLine(patient.ToString());
                }
            }
            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }

        // Method to list all appointments scheduled with the doctor
        public void listAppointments()
        {
            Console.Clear();
            Console.WriteLine(" _______________________________________________________________ ");
            Console.WriteLine(" |                                                             |");
            Console.WriteLine(" |                DOTNET Hospital Management System            |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine(" |                                                             |");
            Console.WriteLine(" |                   All Appointments                          |");
            Console.WriteLine(" |_____________________________________________________________|");

            if (Appointments.Count == 0)
            {
                Console.WriteLine("  No appointments found.");
            }
            else
            {
                Console.WriteLine(" | Doctor Name    | Description              |         Patient Name ");
                Console.WriteLine(" |----------------|--------------------------|----------------------|");

                foreach (var appointment in Appointments)
                {
                    Console.WriteLine($"| {appointment.Doctor.Name,-14}   |  {appointment.Description,-23} | {appointment.Patient.Name}");
                }
            }

            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine(" Press any key to return to the menu...");
            Console.ReadKey();
        }

        // Method to check details of a specific patient by their ID
        public void checkPatient()
        {
            Console.Clear();
            Console.WriteLine(" _______________________________________________________________ ");
            Console.WriteLine(" |                                                             |");
            Console.WriteLine(" |                DOTNET Hospital Management System            |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine(" |                                                             |");
            Console.WriteLine(" |                   Check Patient Details                     |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine("\n Enter the ID of the patient to check: ");
            string PatientID = Console.ReadLine();

            // Find the patient by their ID
            var patient = registeredPatients.FirstOrDefault(p => p.ID == PatientID);
            if (patient != null)
            {
                Console.WriteLine(" | Patient Name      |       Address             | Email                    | Phone       |");
                Console.WriteLine(" |-------------------|---------------------------|--------------------------|-------------|");
                Console.WriteLine(patient.ToString());
            }
            else
            {
                Console.WriteLine("Patient Not Found");
            }

            Console.WriteLine(" Press any key to return to the menu...");
            Console.ReadKey();
        }

        // Method to list all appointments for a specific patient by their ID
        public void appointmentWithPatient()
        {
            Console.Clear();
            Console.WriteLine(" _______________________________________________________________ ");
            Console.WriteLine(" |                                                             |");
            Console.WriteLine(" |                DOTNET Hospital Management System            |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine(" |                                                             |");
            Console.WriteLine(" |                   Check Patient Appointments                |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine("\n Enter the ID of the patient you would like to view appointments for: ");
            string PatientID = Console.ReadLine();

            // Find the patient by their ID
            var patient = registeredPatients.FirstOrDefault(p => p.ID == PatientID);

            if (patient != null)
            {
                // Find all appointments associated with the patient
                var appointments = Appointments.Where(p => p.Patient.ID == PatientID).ToList();

                if (appointments.Count > 0)
                {
                    Console.WriteLine(" | Doctor                |       Patient           |    Description    |");
                    Console.WriteLine(" |-----------------------|-------------------------|-------------------|");

                    foreach (var app in appointments)
                    {
                        Console.WriteLine($"{app.Doctor.Name,-23}   |  {app.Patient.Name,-25} | {app.Description}");
                    }
                }
                else
                {
                    Console.WriteLine("No appointments found for this patient!");
                }
            }
            else
            {
                Console.WriteLine("Patient Not Found");
            }

            Console.WriteLine(" Press any key to return to the menu...");
            Console.ReadKey();
        }
    }
}
