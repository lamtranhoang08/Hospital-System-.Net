namespace hospitalSystem
{
    public class Appointment
    {
        public Doctor Doctor { get; set; }  //THe doctor treating the appoinment
        public Patient Patient { get; set; }  // The patient who is booking the appointment

        public string Description { get; set; } //Patient describe how the feel 

        public Appointment(Doctor doctor, Patient patient, string description)
        {
            Doctor = doctor;
            Patient = patient;
            Description = description;
        }

        // Default constructor for cases where you may want to instantiate without setting properties initially
        public Appointment() { }

        // Override ToString method to print appointment details
        public override string ToString()
        {
            return $"Appointment with Dr. {Doctor.Name} for {Patient.Name} .\n" +
                   $"Reason: {Description}";
        }
    }
}