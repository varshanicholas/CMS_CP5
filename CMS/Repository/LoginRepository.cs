using System.Data;
using System.Data.SqlClient;

namespace CMS.Repository
{
    public class LoginRepository:ILoginRepository
    {
          private readonly string _connectionString;
     public LoginRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("connectionStringMVCadmin");
        }
     public async Task<(bool isValid, int roleId, int staffId)> ValidateLoginAsync(string username, string password)
        {
            bool isValid = false;
            int roleId = 0;
            int staffId = 0;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_ValidateLogin", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                isValid = reader.GetInt32(reader.GetOrdinal("IsValid")) == 1;
                                roleId = reader.GetInt32(reader.GetOrdinal("RoleId"));
                                staffId = reader.GetInt32(reader.GetOrdinal("StaffId"));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Database operation failed.", ex);
            }

            return (isValid, roleId, staffId);
        }
    

    }
}

