using CMS.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CMS.Repository
{
    public class LabTestRepository : ILabTestRepository
    {
        private readonly string _connectionString;

        public LabTestRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LabTest GetLabTestById(int testId)
        {
            LabTest labTest = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetLabTestById", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@TestId", testId);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        labTest = new LabTest
                        {
                            TestId = (int)reader["TestId"],
                            TestName = reader["TestName"].ToString(),
                            //Description = reader["Description"].ToString()
                        };
                    }
                }
            }

            return labTest;
        }

        public List<LabTest> GetAllLabTests()
        {
            var labTests = new List<LabTest>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllLabTests", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        labTests.Add(new LabTest
                        {
                            TestId = (int)reader["TestId"],
                            TestName = reader["TestName"].ToString(),
                            //Description = reader["Description"].ToString()
                        });
                    }
                }
            }

            return labTests;
        }
    }
}
