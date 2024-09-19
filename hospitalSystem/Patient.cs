using System.hospitalSystem;
using System.Xml.Serialization;

namespace hospitalSystem
{
    public class Patient : User
    {
        // List of doctors currently registered with this patient
        public List<Doctor> Doctors { get; set; }

        // List of appointments currently registered with this patient
        public List<Appointment> Appointments { get; set; }

        // Delegates to handle different patient actions
        public delegate void AppointmentHandler(Patient patient);
        public delegate void DoctorHandler(Patient patient, Program program);
        public AppointmentHandler OnListAppointments { get; set; }
        public DoctorHandler OnBookAppointment { get; set; }
        // Constructor initializes the patient with default properties and empty lists for doctors and appointments
        public Patient(string id, string name, string address, string email, string phone)
            : base(id, name, address, email, phone, "patient")
        {
            Doctors = new List<Doctor>();
            Appointments = new List<Appointment>();
            // Assign default delegate methods
            OnListAppointments = DefaultListAppointments;
            OnBookAppointment = DefaultBookAppointment;
        }

        // Get the patient's name
        public string getName()
        {
            return this.Name;
        }

        // Get the patient's address
        public string getAddress()
        {
            return this.Address;
        }

        /*
         * Displays patient details
         */
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
            Console.WriteLine("Patient Details:");
            Console.WriteLine($"Patient ID: {this.ID}");
            Console.WriteLine($"Name: {this.Name}");
            Console.WriteLine($"Address: {this.Address}");
            Console.WriteLine($"Email: {this.Email}");
            Console.WriteLine($"Phone: {this.Phone}");
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        /**
         * Displays details of doctors registered with the patient
         */
        public void listDoctorDetails(Program p)
        {
            Console.Clear();
            Console.WriteLine(" _______________________________________________________________ ");
            Console.WriteLine(" |                                                             |");
            Console.WriteLine(" |                DOTNET Hospital Management System            |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine(" |                                                             |");
            Console.WriteLine(" |                        My Doctor                            |");
            Console.WriteLine(" |_____________________________________________________________|");

            if (Doctors.Count == 0)
            {
                Console.WriteLine("No registered doctors available.");
            }
            else
            {
                Console.WriteLine(" | Name            | Email Address         | Phone       | Address           |");
                Console.WriteLine(" |-----------------|-----------------------|-------------|-------------------|");
                foreach (var doctor in Doctors)
                {
                    Console.WriteLine(doctor.ToString());
                }
                Console.WriteLine(" |_____________________________________________________________|");
            }

            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }
        // Default method for booking appointments
        public void DefaultBookAppointment(Patient patient, Program p)
        {
            patient.bookAppointment(p); // Use the existing booking logic
        }
        /**
         * Allows the patient to book an appointment with either a registered or a new doctor
         */
        public void bookAppointment(Program p)
        {
            Console.Clear();
            Console.WriteLine(" _______________________________________________________________ ");
            Console.WriteLine(" |                                                             |");
            Console.WriteLine(" |                DOTNET Hospital Management System            |");
            Console.WriteLine(" |_____________________________________________________________|");
            Console.WriteLine(" |                                                             |");
            Console.WriteLine(" |                        Booking Appointment                   |");
            Console.WriteLine(" |_____________________________________________________________|");

            // Step 1: Check if the patient already has registered doctors
            if (Doctors.Count > 0)
            {
                Console.WriteLine("You are already registered with the following doctor(s):");
                foreach (var doctor in Doctors)
                {
                    Console.WriteLine($"- Dr. {doctor.Name} ({doctor.Address})");
                }

                Console.WriteLine("Do you want to book an appointment with one of your registered doctors or choose a new one?");
                Console.WriteLine("1. Book with a registered doctor");
                Console.WriteLine("2. Choose a new doctor");

                Console.Write("Enter your choice (1 or 2): ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    // Step 2: Let the patient choose from their registered doctors
                    Console.WriteLine("Select the doctor to book an appointment with:");
                    for (int i = 0; i < Doctors.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. Dr. {Doctors[i].Name} - {Doctors[i].Address}");
                    }

                    Console.Write("Enter the number of the doctor: ");
                    if (int.TryParse(Console.ReadLine(), out int doctorChoice) && doctorChoice > 0 && doctorChoice <= Doctors.Count)
                    {
                        Doctor selectedDoctor = Doctors[doctorChoice - 1];
                        CreateAppointment(selectedDoctor);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Returning to menu...");
                        return;
                    }
                }
            }

            // Step 3: If no registered doctors or patient chose to select a new doctor
            Console.WriteLine("Please choose a doctor from the list below to register:");

            for (int i = 0; i < p.doctors.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Dr. {p.doctors[i].Name} - {p.doctors[i].Address}");
            }

            Console.Write("Enter the number of the doctor you wish to register with: ");
            if (int.TryParse(Console.ReadLine(), out int newDoctorChoice) && newDoctorChoice > 0 && newDoctorChoice <= p.doctors.Count)
            {
                Doctor selectedNewDoctor = p.doctors[newDoctorChoice - 1];
                Doctors.Add(selectedNewDoctor); // Add to patient's registered doctors
                selectedNewDoctor.registeredPatients.Add(this); // Add patient to doctor's list
                Console.WriteLine($"You have been registered with Dr. {selectedNewDoctor.Name}.");
                CreateAppointment(selectedNewDoctor);
            }
            else
            {
                Console.WriteLine("Invalid choice. Returning to menu...");
            }
        }

        /**
         * Helper method to create an appointment
         */
        private void CreateAppointment(Doctor doctor)
        {
            Console.WriteLine($"Booking appointment with Dr. {doctor.Name}.");

            // Prompt for appointment description
            Console.Write("Enter appointment description: ");
            string description = Console.ReadLine();

            // Create new appointment and add it to the patient's and doctor's list of appointments
            Appointment newAppointment = new Appointment
            {
                Description = description,
                Doctor = doctor,
                Patient = this
            };

            Appointments.Add(newAppointment);
            doctor.Appointments.Add(newAppointment); // Add to doctor's list
            Console.WriteLine("Appointment booked successfully!");
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        /**
         * Displays all appointments of the patient
         */
        // Default method for listing appointments
        public void DefaultListAppointments(Patient patient)
        {
            Console.Clear();
            Console.WriteLine(" _________________________________________________________________ ");
            Console.WriteLine(" |                                                               |");
            Console.WriteLine(" |                DOTNET Hospital Management System              |");
            Console.WriteLine(" |_______________________________________________________________|");
            Console.WriteLine(" |                                                               |");
            Console.WriteLine(" |                      Your Appointments                        |");
            Console.WriteLine(" |_______________________________________________________________|");

            if (patient.Appointments.Count == 0)
            {
                Console.WriteLine(" |                       No appointments found.                  |");
            }
            else
            {
                Console.WriteLine(" | Doctor Name      | Description                                |");
                Console.WriteLine(" |------------------|--------------------------------------------|");

                foreach (var appointment in patient.Appointments)
                {
                    Console.WriteLine($" | {appointment.Doctor.Name,-16} | {appointment.Description,-43} |");
                }
            }

            Console.WriteLine(" |_______________________________________________________________|");
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        // Override ToString() method to display patient's details in a formatted way
        public override string ToString()
        {
            return $"{Name,-20} {Address,-24} {Email,-27} {Phone,-13}";
        }

        /**
         * Displays the patient menu
         * and handles input for different functionalities
         */
        public void printPatientMenu(Program p)
        {
            bool stayInMenu = true;

            while (stayInMenu)
            {
                Console.Clear();
                Console.WriteLine(" ______________________________________________________________ ");
                Console.WriteLine(" |                                                            |");
                Console.WriteLine(" |                DOTNET Hospital Management System           |");
                Console.WriteLine(" |____________________________________________________________|");
                Console.WriteLine(" |                                                            |");
                Console.WriteLine(" |                        Patient Menu                        |");
                Console.WriteLine(" |____________________________________________________________|");
                Console.WriteLine($"| WELCOME TO DOTNET HOSPITAL SYSTEM, {this.Name,-25}        |");
                Console.WriteLine(" |____________________________________________________________|");
                Console.WriteLine(" | Please choose an option:                                    |");
                Console.WriteLine(" |-------------------------------------------------------------|");
                Console.WriteLine(" | 1. List patient details                                     |");
                Console.WriteLine(" | 2. List my doctor details                                   |");
                Console.WriteLine(" | 3. List all appointments                                    |");
                Console.WriteLine(" | 4. Book an appointment                                      |");
                Console.WriteLine(" | 5. Log out (Return to login)                                |");
                Console.WriteLine(" | 6. Exit system                                              |");
                Console.WriteLine(" |_____________________________________________________________|");
                Console.Write("Your choice: ");

                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        listMyDetails();
                        break;

                    case '2':
                        listDoctorDetails(p);
                        break;

                    case '3':
                        OnListAppointments(this); // Call delegate instead of direct method call
                        break;

                    case '4':
                        OnBookAppointment(this, p); // Call delegate for booking an appointment
                        break;

                    case '5':
                        stayInMenu = false;
                        p.LoginMenu(); // Return to login menu
                        return;

                    case '6':
                        Console.WriteLine("Exiting system...");
                        Environment.Exit(0); // Close the application
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
