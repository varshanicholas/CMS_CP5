using CMS.Models;
using System.Data.SqlClient;
using System.Data;


namespace CMS.Repository
{
    public class ReceptionistRepository : IReceptionistRepository
    {
        private readonly string connectionString;


        public ReceptionistRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("connectionStringReceptionist");
        }

        public IEnumerable<Patients> GetPatients()
        {
            List<Patients> patList = new List<Patients>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Sp_GetPatient", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Patients pat = new Patients();
                    pat.PatientId = Convert.ToInt32(dr["PatientId"]); // Maps to PatientId in the table
                    pat.PatientName = dr["PatientName"].ToString();  // Maps to PatientName in the table
                    pat.DOB = Convert.ToDateTime(dr["DOB"]);         // Maps to DOB in the table
                    pat.Gender = dr["Gender"].ToString();            // Maps to Gender in the table
                    pat.BloodGroup = dr["BloodGroup"].ToString();    // Maps to BloodGroup in the table
                    pat.PhoneNumber = dr["PhoneNumber"].ToString();  // Maps to PhoneNumber in the table
                    pat.Address = dr["Address"].ToString();          // Maps to Address in the table
                    pat.Email = dr["Email"] == DBNull.Value ? null : dr["Email"].ToString(); // Handles nullable Email field
                    pat.DateOfRegistration = Convert.ToDateTime(dr["DateOfRegistration"]);
                    patList.Add(pat);

                }
            }

            return patList;
        }

        public void AddPatients(Patients pat)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddPatientR", connection); // Assuming procedure is named Sp_InsertPatient
                cmd.CommandType = CommandType.StoredProcedure;

                // Updated parameters to match the table structure
                cmd.Parameters.AddWithValue("@PatientName", pat.PatientName);
                cmd.Parameters.AddWithValue("@DOB", pat.DOB);
                cmd.Parameters.AddWithValue("@Gender", pat.Gender);
                cmd.Parameters.AddWithValue("@BloodGroup", pat.BloodGroup);
                cmd.Parameters.AddWithValue("@PhoneNumber", pat.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", pat.Address);
                cmd.Parameters.AddWithValue("@Email", pat.Email ?? (object)DBNull.Value); // Handle nullable Email
                cmd.Parameters.AddWithValue("@DateOfRegistration", pat.DateOfRegistration);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Patients GetPatientById(int? Id)
        {
            Patients pat = new Patients();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Sp_PatientById", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PatientId", Id);
                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    pat.PatientId = Convert.ToInt32(dr["PatientId"]);
                    pat.PatientName = dr["PatientName"].ToString();
                    pat.DOB = Convert.ToDateTime(dr["DOB"]);
                    pat.Gender = dr["Gender"].ToString();
                    pat.BloodGroup = dr["BloodGroup"].ToString();
                    pat.PhoneNumber = dr["PhoneNumber"].ToString();
                    pat.Address = dr["Address"].ToString();
                    pat.Email = dr["Email"] == DBNull.Value ? null : dr["Email"].ToString();
                    pat.DateOfRegistration = Convert.ToDateTime(dr["DateOfRegistration"]);
                }
            }
            return pat;
        }

        public void UpdatePatients(Patients pat)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Sp_UpdatePatient", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PatientId", pat.PatientId);
                cmd.Parameters.AddWithValue("@PhoneNumber", pat.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", pat.Address);
                cmd.Parameters.AddWithValue("@Email", pat.Email);


                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //        public List<string> GetDepartments()
        //        {
        //            var departments = new List<string>();

        //            using (SqlConnection connection = new SqlConnection(connectionString))
        //            {
        //                SqlCommand cmd = new SqlCommand("sp_GetDepartments", connection);
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                connection.Open(); // Open the connection before executing the command

        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        departments.Add(reader["DepartmentName"].ToString());
        //                    }
        //                }
        //            }

        //            return departments;
        //        }

        //        public IEnumerable<Doctor> GetDoctors()
        //        {
        //            List<Doctor> doctors = new List<Doctor>();
        //            using (SqlConnection connection = new SqlConnection(connectionString))
        //            {
        //                SqlCommand cmd = new SqlCommand("sp_GetDoctorsByDepartmentNameR", connection);
        //                connection.Open();
        //                SqlDataReader dr = cmd.ExecuteReader();
        //                while (dr.Read())
        //                {
        //                    Doctor doctor = new Doctor
        //                    {
        //                        DoctorId = Convert.ToInt32(dr["DoctorId"]),
        //                        DoctorCode = dr["DoctorCode"].ToString(),
        //                        ConsultationFee = Convert.ToDecimal(dr["ConsultationFee"]),
        //                        SpecializationId = Convert.ToInt32(dr["SpecializationId"])
        //                    };
        //                    doctors.Add(doctor);
        //                }
        //            }
        //            return doctors;
        //        }

        //        public IEnumerable<Doctor> GetDoctorsByDepartment(int departmentId)
        //        {
        //            var doctors = new List<Doctor>();

        //            using (SqlConnection connection = new SqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                using (SqlCommand cmd = new SqlCommand("sp_GetDoctorsByDepartmentNameR", connection))
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.Parameters.AddWithValue("@DepartmentId", departmentId);

        //                    using (SqlDataReader reader = cmd.ExecuteReader())
        //                    {
        //                        while (reader.Read())
        //                        {
        //                            doctors.Add(new Doctor
        //                            {
        //                                DoctorId = Convert.ToInt32(reader["DoctorId"]),
        //                                DoctorName = reader["DoctorName"].ToString()
        //                            });
        //                        }
        //                        return doctors;
        //                    }
        //                }

        //            }
        //        }
        //        public IEnumerable<Department> GetDepartmentss()
        //        {
        //            using (var connection = new SqlConnection(connectionString))
        //            {
        //                connection.Open();

        //                using (var command = new SqlCommand("sp_GetDepartmentsR", connection))
        //                {
        //                    using (var reader = command.ExecuteReader())
        //                    {
        //                        var departments = new List<Department>();
        //                        while (reader.Read())
        //                        {
        //                            departments.Add(new Department()
        //                            {
        //                                DepartmentId = reader.GetInt32(0),
        //                                DepartmentName = reader.GetString(1)
        //                            });
        //                        }

        //                        return departments;
        //                    }
        //                }
        //            }
        //        }


        //        public void AddAppointments(Appointment app)
        //        {
        //            using (SqlConnection connection = new SqlConnection(connectionString))
        //            {
        //                SqlCommand cmd = new SqlCommand("sp_AddAppointmentR", connection); // Assuming procedure is named Sp_InsertPatient
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                // Updated parameters to match the table structure
        //                cmd.Parameters.AddWithValue("@DepartmentName", app.DepartmentName);
        //                cmd.Parameters.AddWithValue("@DoctorName", app.DoctorName);
        //                cmd.Parameters.AddWithValue("@AppointmentDate", app.AppointmentDate);


        //                connection.Open();
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //        public IEnumerable<Appointment> GetAppointmentsByPatientId(int patientId)
        //        {
        //            var appointments = new List<Appointment>();

        //            using (SqlConnection connection = new SqlConnection(connectionString))
        //            {
        //                SqlCommand cmd = new SqlCommand("sp_GetAppointmentsByPatientIdR", connection);
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@PatientId", patientId);

        //                connection.Open();
        //                using (SqlDataReader dr = cmd.ExecuteReader())
        //                {
        //                    while (dr.Read())
        //                    {
        //                        appointments.Add(new Appointment
        //                        {
        //                            AppointmentId = Convert.ToInt32(dr["AppointmentId"]),
        //                            DoctorId = Convert.ToInt32(dr["DoctorId"]),
        //                            PatientId = patientId,
        //                            AppointmentDate = Convert.ToDateTime(dr["AppointmentDate"]),
        //                            TokenNumber = Convert.ToInt32(dr["TokenNumber"]),
        //                            ConsultationStatus = dr["ConsultationStatus"].ToString()
        //                        });
        //                    }
        //                }
        //            }

        //            return appointments;
        //        }

        //        public IEnumerable<Doctor> GetDoctorsBySpecialization(int specializationId)
        //        {
        //            var doctors = new List<Doctor>();

        //            using (SqlConnection connection = new SqlConnection(connectionString))
        //            {
        //                SqlCommand cmd = new SqlCommand("[sp_GetDoctorsBySpecializationR]", connection);
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@SpecializationId", specializationId);

        //                connection.Open();
        //                using (SqlDataReader dr = cmd.ExecuteReader())
        //                {
        //                    while (dr.Read())
        //                    {
        //                        doctors.Add(new Doctor
        //                        {
        //                            DoctorId = Convert.ToInt32(dr["DoctorId"]),
        //                            DoctorName = dr["DoctorName"].ToString(),
        //                            SpecializationId = Convert.ToInt32(dr["SpecializationId"]),
        //                            ConsultationFee = Convert.ToDecimal(dr["ConsultationFee"])
        //                        });
        //                    }
        //                }
        //            }

        //            return doctors;
        //        }


        //        public IEnumerable<Specialization> GetSpecializations() //error type or namespace specialization could not found
        //        {
        //            var specializations = new List<Specialization>();  //error type or namespace specialization could not found

        //            using (SqlConnection connection = new SqlConnection(connectionString))
        //            {
        //                SqlCommand cmd = new SqlCommand("sp_GetDoctorSpecializations", connection);
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                connection.Open();
        //                using (SqlDataReader dr = cmd.ExecuteReader())
        //                {
        //                    while (dr.Read())
        //                    {
        //                        specializations.Add(new Specialization //error type or namespace specialization could not found
        //                        {
        //                            SpecializationId = Convert.ToInt32(dr["SpecializationId"]),
        //                            SpecializationName = dr["SpecializationName"].ToString()
        //                        });
        //                    }
        //                }
        //            }

        //            return specializations;
        //        }

        //        IEnumerable<Department> IReceptionistRepository.GetDepartments()
        //        {
        //            using (var connection = new SqlConnection(connectionString))
        //            {
        //                connection.Open();

        //                using (var command = new SqlCommand("sp_GetDepartmentsR", connection))
        //                {
        //                    using (var reader = command.ExecuteReader())
        //                    {
        //                        var departments = new List<Department>();
        //                        while (reader.Read())
        //                        {
        //                            departments.Add(new Department()
        //                            {
        //                                DepartmentId = reader.GetInt32(0),
        //                                DepartmentName = reader.GetString(1)
        //                            });
        //                        }

        //                        return departments;+
        //                    }
        //                }
        //            }


        //        //public Doctor GetDoctorById(int id)
        //        //{
        //        //    Doctor doctor = null;

        //        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //        //    {
        //        //        SqlCommand cmd = new SqlCommand("sp_GetDoctorById", connection);
        //        //        cmd.CommandType = CommandType.StoredProcedure;
        //        //        cmd.Parameters.AddWithValue("@DoctorId", id);

        //        //        connection.Open();
        //        //        using (SqlDataReader dr = cmd.ExecuteReader())
        //        //        {
        //        //            if (dr.Read())
        //        //            {
        //        //                doctor = new Doctor
        //        //                {
        //        //                    DoctorId = Convert.ToInt32(dr["DoctorId"]),
        //        //                    DoctorName = dr["DoctorName"].ToString(),
        //        //                    SpecializationId = Convert.ToInt32(dr["SpecializationId"]),
        //        //                    ConsultationFee = Convert.ToDecimal(dr["ConsultationFee"]),
        //        //                    DepartmentId = Convert.ToInt32(dr["DepartmentId"])
        //        //                };
        //        //            }
        //        //        }
        //        //    }

        //        //    return doctor;
        //        //}


        //        //public void AddAppointment(Appointment appointment)
        //        //{
        //        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //        //    {
        //        //        SqlCommand cmd = new SqlCommand("sp_AddAppointment", connection);
        //        //        cmd.CommandType = CommandType.StoredProcedure;

        //        //        cmd.Parameters.AddWithValue("@PatientId", appointment.PatientId);
        //        //        cmd.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
        //        //        cmd.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
        //        //        cmd.Parameters.AddWithValue("@TokenNumber", appointment.TokenNumber);
        //        //        cmd.Parameters.AddWithValue("@ConsultationStatus", appointment.ConsultationStatus);

        //        //        connection.Open();
        //        //        cmd.ExecuteNonQuery();
        //        //    }
        //        //}


        //        //public IEnumerable<Department> GetDepartments()
        //        //{
        //        //    List<Department> departments = new List<Department>();
        //        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //        //    {
        //        //        SqlCommand cmd = new SqlCommand("sp_GetDepartmentsR", connection);
        //        //        cmd.CommandType = CommandType.StoredProcedure;
        //        //        connection.Open();
        //        //        SqlDataReader dr = cmd.ExecuteReader();
        //        //        while (dr.Read())
        //        //        {
        //        //            departments.Add(new Department
        //        //            {
        //        //                DepartmentId = Convert.ToInt32(dr["DepartmentId"]),
        //        //                DepartmentName = dr["DepartmentName"].ToString()
        //        //            });
        //        //        }
        //        //    }
        //        //    return departments;
        //        //}

        //        //public IEnumerable<Doctor> GetDoctorsByDepartment(string departmentName)
        //        //{
        //        //    List<Doctor> doctors = new List<Doctor>();
        //        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //        //    {
        //        //        SqlCommand cmd = new SqlCommand("sp_GetDoctorsByDepartmentNameR", connection);
        //        //        cmd.CommandType = CommandType.StoredProcedure;
        //        //        cmd.Parameters.AddWithValue("@DepartmentName", departmentName);
        //        //        connection.Open();
        //        //        SqlDataReader dr = cmd.ExecuteReader();
        //        //        while (dr.Read())
        //        //        {
        //        //            doctors.Add(new Doctor
        //        //            {
        //        //                DoctorId = Convert.ToInt32(dr["DoctorId"]),
        //        //                DoctorName = dr["DoctorName"].ToString()
        //        //            });
        //        //        }
        //        //    }
        //        //    return doctors;
        //        //}

        //        //public int AddAppointment(AppointmentViewModel model)
        //        //{
        //        //    int appointmentId = 0;
        //        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //        //    {
        //        //        SqlCommand cmd = new SqlCommand("sp_AddAppointmentR", connection);
        //        //        cmd.CommandType = CommandType.StoredProcedure;

        //        //        cmd.Parameters.AddWithValue("@PatientId", model.PatientId);
        //        //        cmd.Parameters.AddWithValue("@DoctorId", model.DoctorId);
        //        //        cmd.Parameters.AddWithValue("@AppointmentDate", model.AppointmentDate);
        //        //        cmd.Parameters.AddWithValue("@ConsultationStatus", 0); // Default to not consulted
        //        //        cmd.Parameters.Add("@AppointmentId", SqlDbType.Int).Direction = ParameterDirection.Output;

        //        //        connection.Open();
        //        //        cmd.ExecuteNonQuery();

        //        //        appointmentId = Convert.ToInt32(cmd.Parameters["@AppointmentId"].Value);
        //        //    }
        //        //    return appointmentId;
        //        //}

        //        //public void AddAppointments(Appointment app)
        //        //{
        //        //    throw new NotImplementedException();
        //        //}

        //        //public IEnumerable<Doctor> GetDoctorsByDepartment(int departmentId)
        //        //{
        //        //    throw new NotImplementedException();
        //        //}

        //        //public IEnumerable<Doctor> GetDoctorsBySpecialization(int specializationId)
        //        //{
        //        //    throw new NotImplementedException();
        //        //}

        //        //public IEnumerable<Specialization> GetSpecializations()
        //        //{
        //        //    throw new NotImplementedException();
        //        //}

        //        //public Doctor GetDoctorById(int id)
        //        //{
        //        //    throw new NotImplementedException();
        //        //}

        //        //public void AddAppointment(Appointment appointment)
        //        //{
        //        //    throw new NotImplementedException();
        //        //}

        //        //object IReceptionistRepository.GetDoctorsByDepartment(string departmentName)
        //        //{
        //        //    throw new NotImplementedException();
        //        //}

        //        //object IReceptionistRepository.GetDoctorsByDepartment(string departmentName)
        //        //{
        //        //    throw new NotImplementedException();
        //        //}
        //    }

        //}


        public IEnumerable<Department> GetDepartments()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("sp_GetDepartmentsR", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var departments = new List<Department>();
                        while (reader.Read())
                        {
                            departments.Add(new Department
                            {
                                DepartmentId = reader.GetInt32(0),
                                DepartmentName = reader.GetString(1)
                            });
                        }
                        return departments;
                    }
                }
            }

        }

        public IEnumerable<Doctor> GetDoctorsByDepartment(int departmentId)
        {
            var doctors = new List<Doctor>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetDoctorsByDepartmentId1R", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DepartmentId", departmentId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            doctors.Add(new Doctor
                            {
                                DoctorId = Convert.ToInt32(reader["DoctorId"]),
                                DoctorName = reader["DoctorName"].ToString() // Make sure this matches the column name in SP
                            });
                        }
                    }
                }
            }

            return doctors;
        }



        public IEnumerable<dynamic> GetDoctors()
        {
            List<dynamic> doctors = new List<dynamic>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetAllDoctorR", conn)) // Using stored procedure with join
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Use anonymous types or dynamic objects to hold additional data like the doctor's name
                            var doctor = new
                            {
                                DoctorId = Convert.ToInt32(reader["DoctorId"]),
                                StaffId = Convert.ToInt32(reader["StaffId"]),
                                ConsultationFee = Convert.ToDecimal(reader["ConsultationFee"]),
                                SpecializationId = Convert.ToInt32(reader["SpecializationId"]),
                                DoctorName = reader["DoctorName"].ToString() // Get doctor name from Staff table
                            };

                            doctors.Add(doctor);
                        }
                    }
                }
            }

            return doctors;
        }


        public int GenerateTokenForDoctor(int doctorId, DateTime appointmentDate)
        {
            int lastToken = GetLastTokenForDoctor(doctorId, appointmentDate);

            if (lastToken < 30)
            {
                return lastToken + 1; // Increment token number
            }
            else
            {
                throw new InvalidOperationException("Maximum token limit of 30 reached for the doctor today.");
            }
        }

        public int GetLastTokenForDoctor(int doctorId, DateTime appointmentDate)
        {
            int lastToken = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetLastTokenForDoctorR", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DoctorId", doctorId);
                    cmd.Parameters.AddWithValue("@AppointmentDate", appointmentDate);

                    object result = cmd.ExecuteScalar(); // Execute the query and return the last token

                    if (result != DBNull.Value)
                    {
                        lastToken = Convert.ToInt32(result);
                    }
                }
            }

            return lastToken;
        }



        public decimal GetConsultationFeeByDoctorId(int doctorId)
        {
            // Query the database to get the doctor's consultation fee
            string query = "SELECT ConsultationFee FROM Doctors WHERE DoctorId = @DoctorId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DoctorId", doctorId);
                conn.Open();

                var fee = cmd.ExecuteScalar();
                return fee != null ? Convert.ToDecimal(fee) : 0; // Return the consultation fee, or 0 if not found
            }
        }

        public IEnumerable<Specialization> GetSpecializations()
        {
            var specializations = new List<Specialization>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_GetSpecializationsR", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var specialization = new Specialization
                            {
                                SpecializationId = reader.GetInt32(reader.GetOrdinal("SpecializationId")),
                                SpecializationName = reader.GetString(reader.GetOrdinal("SpecializationName"))
                            };
                            specializations.Add(specialization);
                        }
                    }
                }
            }

            return specializations;
        }

        public IEnumerable<DoctorViewModel> GetDoctorsBySpecialization(int specializationId)
        {
            var doctors = new List<DoctorViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_GetDoctorsBySpecializationR", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SpecializationId", specializationId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var doctor = new DoctorViewModel
                            {
                                DoctorId = reader.GetInt32(reader.GetOrdinal("DoctorId")),
                                DoctorName = reader.GetString(reader.GetOrdinal("FullName")),

                            };
                            doctors.Add(doctor);
                        }
                    }
                }
            }

            return doctors;
        }

        public DoctorViewModel GetDoctorById(int doctorId)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("sp_GetDoctorByIdR", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DoctorId", doctorId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DoctorViewModel
                            {
                                DoctorId = (int)reader["DoctorId"],
                                DoctorName = (string)reader["DoctorName"],
                            };
                        }
                    }
                }
            }
            return null;
        }

        public AppointmentConfirmationViewModel GetAppointmentDetails(int appointmentId)
        {
            AppointmentConfirmationViewModel appointmentDetails = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("sp_GetAppointmentDetailsR", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            appointmentDetails = new AppointmentConfirmationViewModel
                            {
                                AppointmentId = reader.IsDBNull(reader.GetOrdinal("AppointmentId")) ? 0 : reader.GetInt32(reader.GetOrdinal("AppointmentId")),
                                AppointmentDate = reader.IsDBNull(reader.GetOrdinal("AppointmentDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                                PatientName = reader.IsDBNull(reader.GetOrdinal("PatientName")) ? string.Empty : reader.GetString(reader.GetOrdinal("PatientName")),
                                TokenNumber = reader.IsDBNull(reader.GetOrdinal("TokenNumber")) ? 0 : reader.GetInt32(reader.GetOrdinal("TokenNumber")),
                                DoctorId = reader.IsDBNull(reader.GetOrdinal("DoctorId")) ? 0 : reader.GetInt32(reader.GetOrdinal("DoctorId")),
                                DoctorName = reader.IsDBNull(reader.GetOrdinal("DoctorName")) ? string.Empty : reader.GetString(reader.GetOrdinal("DoctorName"))
                            };
                        }
                    }
                }
            }

            return appointmentDetails;
        }


        public Staff GetStaffById(int staffId)
        {
            Staff staff = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("sp_GetStaffByIdR", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StaffId", staffId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            staff = new Staff
                            {
                                StaffId = (int)reader["StaffId"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()
                            };
                        }
                    }
                }
            }

            return staff;
        }

        public void AddAppointment(Appointment appointment)
        {

            // Generate Token Number for the selected doctor
            appointment.TokenNumber = GenerateTokenForDoctor(appointment.DoctorId, appointment.AppointmentDate);

            // Set consultation status to false by default
            appointment.ConsultationStatus = false;

            // Connection string to your database (use a configuration setting instead of hardcoding)
            //string connectionString = "YourConnectionStringHere"; // Replace with your actual connection string

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_AddAppointmentR", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the stored procedure
                        cmd.Parameters.AddWithValue("@PatientId", appointment.PatientId);
                        cmd.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
                        cmd.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                        cmd.Parameters.AddWithValue("@TokenNumber", appointment.TokenNumber);
                        cmd.Parameters.AddWithValue("@ConsultationStatus", appointment.ConsultationStatus);

                        // Add an output parameter to get the newly inserted AppointmentId
                        SqlParameter appointmentIdParam = new SqlParameter("@AppointmentId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(appointmentIdParam);

                        // Execute the stored procedure
                        cmd.ExecuteNonQuery();

                        // Retrieve the newly inserted AppointmentId from the output parameter
                        appointment.AppointmentId = (int)appointmentIdParam.Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                // Log or rethrow the exception based on your error handling strategy
                throw new Exception("An error occurred while adding the appointment.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        public IEnumerable<Appointment> GetAllAppointments()
        {
            throw new NotImplementedException();
        }

        public void DeleteAppointment(int id)
        {
            throw new NotImplementedException();
        }
    }

}



