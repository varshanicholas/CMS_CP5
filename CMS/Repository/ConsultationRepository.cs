using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMS.Repository
{
    public class ConsultationRepository : IConsultationRepository
    {
        private readonly string _connectionString;

        public ConsultationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddConsultationDetails(PatientHistory history)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddConsultationDetails", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //cmd.Parameters.AddWithValue("@MedicineId", history.MedicineId);
                cmd.Parameters.AddWithValue("@AppointmentId", history.AppointmentId);
                cmd.Parameters.AddWithValue("@Symptoms", history.Symptoms);
                cmd.Parameters.AddWithValue("@Diagnosis", history.Diagnosis);
                cmd.Parameters.AddWithValue("@TreatmentPlan", history.TreatmentPlan);
                cmd.Parameters.AddWithValue("@MedicineId", history.MedicineId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Dosage", history.Dosage ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Frequency", history.Frequency ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TestId", history.TestId ?? (object)DBNull.Value);
                //cmd.Parameters.AddWithValue("@CreatedBy", history.CreatedBy);
                cmd.Parameters.AddWithValue("@CreatedBy",1);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<PatientHistory> GetAllDetailsForHistory(int patientId, bool includeNew)
        {
            var histories = new List<PatientHistory>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllDetailsForHistory", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PatientId", patientId);
                cmd.Parameters.AddWithValue("@IncludeNew", includeNew);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        histories.Add(new PatientHistory
                        {
                            Id = (int)reader["MainPrescriptionId"],
                            PatientId = patientId,
                            Symptoms = reader["Symptoms"].ToString(),
                            Diagnosis = reader["Diagnosis"].ToString(),
                            TreatmentPlan = reader["TreatmentPlan"].ToString(),
                            Medicine = reader["MedicineName"].ToString(),
                            Dosage = reader["Dosage"]?.ToString(),
                            Frequency = reader["Frequency"]?.ToString(),
                            LabTest = reader["LabTest"]?.ToString(),
                            Date = (DateTime)reader["CreatedDate"]
                        });
                    }
                }
            }
            return histories;
        }
    }
}
