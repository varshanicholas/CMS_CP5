using CMS.Models;
using CMS.Repository;
using System.Data.SqlClient;
using System.Data;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly string _connectionString;

    // Constructor that uses IConfiguration to retrieve connection string
    public AppointmentRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("connectionStringDoctor");
    }

    // Constructor that directly accepts a connection string (useful for testing or alternate sources)
    public AppointmentRepository(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty.");
        }
        _connectionString = connectionString;
    }

    public string ConnectionString => _connectionString;

    // Method to fetch today's appointments for a given doctor
    public IEnumerable<Appointment> GetTodaysAppointments(int doctorId)
    {
        var appointments = new List<Appointment>();

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_GetTodaysAppointments", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DoctorId", doctorId);

            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    appointments.Add(new Appointment
                    {
                        AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                        PatientId = Convert.ToInt32(reader["PatientId"]),
                        PatientName = reader["PatientName"].ToString(),
                        AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                        TokenNumber = Convert.ToInt32(reader["TokenNumber"]),
                        ConsultationStatus = reader["ConsultationStatus"].ToString()
                    });
                }
            }
        }

        return appointments;
    }

    // Method to fetch a specific appointment by its ID
    public Appointment GetAppointmentById(int appointmentId)
    {
        Appointment appointment = null;

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_GetAppointmentById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    appointment = new Appointment
                    {
                        AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                        PatientId = Convert.ToInt32(reader["PatientId"]),
                        PatientName = reader["PatientName"].ToString(),
                        AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                        TokenNumber = Convert.ToInt32(reader["TokenNumber"]),
                        ConsultationStatus = reader["ConsultationStatus"].ToString()
                    };
                }
            }
        }

        return appointment;
    }
}
