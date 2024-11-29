using CMS.Models;
using System.Data.SqlClient;
using System.Data;

namespace CMS.Repository
{
    public class StaffRepository:IStaffRepository

    {
        private readonly string connectionString;

        public StaffRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("connectionStringMVCadmin");
        }
         public void AddStaff(Staff staff)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AddStaffWithLogin", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StaffId", staff.StaffId);
                    cmd.Parameters.AddWithValue("@FirstName", staff.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", staff.LastName);
                    cmd.Parameters.AddWithValue("@Gender", staff.Gender);
                    cmd.Parameters.AddWithValue("@DateOfBirth", staff.DateOfBirth);
                    cmd.Parameters.AddWithValue("@BloodGroup", (object?)staff.BloodGroup ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@JoiningDate", staff.JoiningDate);
                    cmd.Parameters.AddWithValue("@Salary", staff.Salary);
                    cmd.Parameters.AddWithValue("@Experience", staff.Experience);
                    cmd.Parameters.AddWithValue("@PhoneNumber", staff.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", staff.Email);
                    cmd.Parameters.AddWithValue("@Qualification", (object?)staff.Qualification ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", (object?)staff.Address ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", staff.IsActive);
                    cmd.Parameters.AddWithValue("@DepartmentName", staff.DepartmentName);
                    cmd.Parameters.AddWithValue("@RoleName", staff.RoleName);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void EnableStaff(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ToggleStaffStatus", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffId", id);
                cmd.Parameters.AddWithValue("@IsActive", true);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DisableStaff(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ToggleStaffStatus", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffId", id);
                cmd.Parameters.AddWithValue("@IsActive", false);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
       

        public List<String> GetDepartments()
        {
            var departments = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT DepartmentName FROM Departments", conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departments.Add(reader["DepartmentName"].ToString());
                    }
                }
            }

            return departments;
        }

        public List<String> GetRoles()
        {
            var roles = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT RoleName FROM Roles", conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(reader["RoleName"].ToString());
                    }
                }
            }

            return roles;
        }

       
        public Staff GetStaffById(int? id)
        {
            Staff staff = new Staff();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetStaffDetailsById", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffId", id);

                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    staff.StaffId = Convert.ToInt32(dr["StaffId"]);
                    staff.FirstName = dr["FirstName"].ToString();
                    staff.LastName = dr["LastName"].ToString();
                    staff.Gender = dr["Gender"].ToString();
                    staff.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
                    staff.BloodGroup = dr["BloodGroup"].ToString();
                    staff.JoiningDate = Convert.ToDateTime(dr["JoiningDate"]);
                    staff.Salary = Convert.ToDecimal(dr["Salary"]);
                    staff.Experience = Convert.ToInt32(dr["Experience"]);
                    staff.PhoneNumber = dr["PhoneNumber"].ToString();
                    staff.Email = dr["Email"].ToString();
                    staff.Qualification = dr["Qualification"].ToString();
                    staff.Address = dr["Address"].ToString();
                    staff.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    staff.DepartmentName = dr["DepartmentName"].ToString();
                    staff.RoleName = dr["RoleName"].ToString();
                }
            }

            return staff;
        }

        public IEnumerable<Staff> GetStaffList()
        {
            List<Staff> staffList = new List<Staff>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetStaffLists", connection); // Updated stored procedure name
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Staff staff = new Staff
                    {
                        StaffId = Convert.ToInt32(dr["StaffId"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        PhoneNumber = dr["PhoneNumber"] != DBNull.Value ? dr["PhoneNumber"].ToString() : string.Empty, // Null check
                        Address = dr["Address"] != DBNull.Value ? dr["Address"].ToString() : string.Empty, // Null check
                        DateOfBirth = dr["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(dr["DateOfBirth"]) : default(DateTime), // Null check
                        JoiningDate = dr["JoiningDate"] != DBNull.Value ? Convert.ToDateTime(dr["JoiningDate"]) : default(DateTime), // Null check
                        RoleName = dr["RoleName"].ToString(),
                        Email = dr["Email"] != DBNull.Value ? dr["Email"].ToString() : string.Empty, // Null check
                        DepartmentName = dr["DepartmentName"].ToString(),
                        Salary = dr["Salary"] != DBNull.Value ? Convert.ToDecimal(dr["Salary"]) : 0m, // Null check for decimal
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                        BloodGroup = dr["BloodGroup"] != DBNull.Value ? dr["BloodGroup"].ToString() : string.Empty, // Null check for BloodGroup
                        Qualification = dr["Qualification"] != DBNull.Value ? dr["Qualification"].ToString() : string.Empty, // Null check for Qualification
                        Experience = dr["Experience"] != DBNull.Value ? Convert.ToInt32(dr["Experience"]) : 0 // Null check for Experience
                    };
                    staffList.Add(staff);
                }
            }

            return staffList;

        }

        public void UpdateStaff(Staff staff)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateStaff", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters from the Staff object
                cmd.Parameters.AddWithValue("@StaffId", staff.StaffId);
                cmd.Parameters.AddWithValue("@FirstName", staff.FirstName);
                cmd.Parameters.AddWithValue("@LastName", staff.LastName);
                cmd.Parameters.AddWithValue("@Gender", staff.Gender);
                cmd.Parameters.AddWithValue("@DateOfBirth", staff.DateOfBirth);
                cmd.Parameters.AddWithValue("@BloodGroup", staff.BloodGroup);
                cmd.Parameters.AddWithValue("@JoiningDate", staff.JoiningDate);
                cmd.Parameters.AddWithValue("@Salary", staff.Salary);
                cmd.Parameters.AddWithValue("@Experience", staff.Experience);
                cmd.Parameters.AddWithValue("@PhoneNumber", staff.PhoneNumber);
                cmd.Parameters.AddWithValue("@Email", staff.Email);
                cmd.Parameters.AddWithValue("@Qualification", staff.Qualification);
                cmd.Parameters.AddWithValue("@Address", staff.Address);
                cmd.Parameters.AddWithValue("@IsActive", staff.IsActive);
                cmd.Parameters.AddWithValue("@DepartmentName", staff.DepartmentName);
                cmd.Parameters.AddWithValue("@RoleName", staff.RoleName);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Staff> SearchStaffByPhoneNumberAndName(string PhoneNumber = null, string name = null)
        {
            List<Staff> staffList = new List<Staff>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SearchStaffByPhoneNumberAndNames", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PhoneNumber", (object)PhoneNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Name", (object)name ?? DBNull.Value);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Staff staff = new Staff
                        {
                            StaffId = reader.GetInt32(reader.GetOrdinal("StaffId")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Gender = reader.GetString(reader.GetOrdinal("Gender")),
                            DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                            BloodGroup = reader.IsDBNull(reader.GetOrdinal("BloodGroup")) ? null : reader.GetString(reader.GetOrdinal("BloodGroup")),
                            JoiningDate = reader.GetDateTime(reader.GetOrdinal("JoiningDate")),
                            Salary = reader.GetDecimal(reader.GetOrdinal("Salary")),
                            Experience = reader.GetInt32(reader.GetOrdinal("Experience")),
                            PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Qualification = reader.IsDBNull(reader.GetOrdinal("Qualification")) ? null : reader.GetString(reader.GetOrdinal("Qualification")),
                            Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                            DepartmentName = reader.GetString(reader.GetOrdinal("DepartmentName")),
                            RoleName = reader.GetString(reader.GetOrdinal("RoleName"))
                        };

                        staffList.Add(staff);
                    }
                }
            }

            return staffList;
        }
    }
}
