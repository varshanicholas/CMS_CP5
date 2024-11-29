using CMS.Models;

namespace CMS.Repository
{
    public interface IReceptionistRepository
    {
        public IEnumerable<Patients> GetPatients();

        public void AddPatients(Patients pat);

        public Patients GetPatientById(int? Id);


        public void UpdatePatients(Patients pat);

        //View all doctors
        IEnumerable<dynamic> GetDoctors();

        // Appointment methods
        void AddAppointment(Appointment appointment);

        //view all appointments
        IEnumerable<Appointment> GetAllAppointments();



        //delete or cancel the appointment
        void DeleteAppointment(int id);

        // Department methods
        IEnumerable<Department> GetDepartments();

        //get doctors by department name
        IEnumerable<Doctor> GetDoctorsByDepartment(int departmentId);


        int GenerateTokenForDoctor(int doctorId, DateTime appointmentDate);

        int GetLastTokenForDoctor(int doctorId, DateTime appointmentDate);

        //Appointment GetAppointmentById(int appointmentId);

        decimal GetConsultationFeeByDoctorId(int doctorId);

        //getting all specialization of doctors
        IEnumerable<Specialization> GetSpecializations();

        //Get doctors with thier respective specialization
        IEnumerable<DoctorViewModel> GetDoctorsBySpecialization(int specializationId);

        //get doctors using their respective id's
        DoctorViewModel GetDoctorById(int doctorId);

        //Get appointment details
        AppointmentConfirmationViewModel GetAppointmentDetails(int appointmentId);

        //Get staff by id
        public Staff GetStaffById(int staffId);



    }
}


