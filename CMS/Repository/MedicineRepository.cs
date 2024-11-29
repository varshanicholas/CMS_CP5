using CMS.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMS.Repository
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly string _connectionString;

        public MedicineRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Medicine GetMedicineById(int medicineId)
        {
            Medicine medicine = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetMedicineById", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@MedicineId", medicineId);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        medicine = new Medicine
                        {
                            MedicineId = (int)reader["MedicineId"],
                            MedicineName = reader["MedicineName"].ToString(),
                            //Description = reader["Description"].ToString()
                        };
                    }
                }
            }

            return medicine;
        }

        public List<Medicine> GetAllMedicines()
        {
            var medicines = new List<Medicine>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllMedicines", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        medicines.Add(new Medicine
                        {
                            MedicineId = (int)reader["MedicineId"],
                            MedicineName = reader["MedicineName"].ToString(),
                            //Description = reader["Description"].ToString()
                        });
                    }
                }
            }

            return medicines;
        }
    }
}
