namespace System{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace hospitalSystem
    {

        // Abstract base class to represent a generic User in the hospital system
        public abstract class User
        {
            // Properties shared by all users in the system

            // Unique ID for each user (e.g., patient ID, doctor ID, admin ID)
            public string ID { get; set; }

            // Full name of the user
            public string Name { get; set; }

            // Address of the user
            public string Address { get; set; }

            // Email address of the user
            public string Email { get; set; }

            // Phone number of the user
            public string Phone { get; set; }

            // Role of the user (e.g., admin, doctor, patient)
            // The private set restricts the role assignment to within the class and constructor only
            public string Role { get; private set; }

            // Constructor for initializing a user
            // Protected access ensures that this class cannot be directly instantiated
            // Only derived classes (like Doctor, Patient, Admin) can call this constructor
            protected User(string id, string name, string address, string email, string phone, string role)
            {
                // Setting the properties with the values provided by derived classes
                ID = id;
                Name = name;
                Address = address;
                Email = email;
                Phone = phone;
                Role = role; // Role is assigned upon creation, e.g., "admin", "doctor", "patient"
            }
        }
    }
}