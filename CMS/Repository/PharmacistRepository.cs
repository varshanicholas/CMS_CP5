using System.Data;
using System.Data.SqlClient;
using CMS.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;


namespace CMS.Repository
{
    public class PharmacistRepository : IPharmacistRepository
    {
        private readonly string connectionString;

        public PharmacistRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("connectionStringMVC");
        }


        public List<MedicinePrescriptionModel> GetMedicinePrescriptions()
        {
            var prescriptions = new List<MedicinePrescriptionModel>();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("GetMedicinePrescriptions", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prescriptions.Add(new MedicinePrescriptionModel
                            {
                                AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                                PatientName = reader["PatientName"].ToString(),
                                Age = Convert.ToInt32(reader["Age"]),
                                Medicine = reader["Medicine"].ToString(),
                                DoctorName = reader["DoctorName"].ToString()
                            });
                        }
                    }
                }
            }

            return prescriptions;
        }



        public AppointmentDetailsModel GetAppointmentDetails(int? appointmentId)
        {
            var appointmentDetails = new AppointmentDetailsModel();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("GetAppointmentDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        // Read appointment details
                        if (reader.Read())
                        {
                            appointmentDetails.AppointmentId = Convert.ToInt32(reader["AppointmentId"]);
                            appointmentDetails.AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                            appointmentDetails.PatientName = reader["PatientName"].ToString();
                            appointmentDetails.PhoneNumber = reader["PhoneNumber"].ToString();
                            appointmentDetails.Email = reader["Email"].ToString();
                            appointmentDetails.DoctorName = reader["DoctorName"].ToString();
                        }

                        // Move to medicine details result set
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                appointmentDetails.Medicines.Add(new MedicineDetailModel
                                {
                                    SerialNo = Convert.ToInt32(reader["SerialNo"]),
                                    MedicineName = reader["MedicineName"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    Dosage = reader["Dosage"].ToString(),
                                    Duration = reader["Duration"].ToString(),
                                    Frequency = reader["Frequency"].ToString(),
                                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                                });
                            }
                        }
                    }
                }
            }

            return appointmentDetails;
        }


        public PrescriptionDetailModel GetBillDetails(int? appointmentId)
        {
            var billDetails = new PrescriptionDetailModel();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("GetBillDetailsByAppointment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AppointmentId", appointmentId);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        // Read general appointment details
                        if (reader.Read())
                        {
                            billDetails.AppointmentId = Convert.ToInt32(reader["AppointmentId"]);
                            billDetails.PatientName = reader["PatientName"].ToString();
                            billDetails.DoctorName = reader["DoctorName"].ToString();
                            billDetails.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                        }

                        // Move to medicine details result set
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                billDetails.MedicineDetails.Add(new BillDetailsModel
                                {
                                    MedicineName = reader["MedicineName"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    PricePerStrip = Convert.ToDecimal(reader["PricePerUnit"]),
                                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                    GST = Convert.ToDecimal(reader["GST"]),
                                });
                            }
                        }
                    }
                }
            }

                // Calculate grand total (sum of total amounts and GST)
                billDetails.GrandTotal = billDetails.MedicineDetails.Sum(m => m.TotalAmount + m.GST);

                return billDetails;
            
        }
    }
}
    /* public BillDetailsModel GetBillDetails(int appointmentId)
     {
         var billDetails = new BillDetailsModel();

         using (var connection = new SqlConnection(connectionString))
         {
             using (var command = new SqlCommand("GetBillDetails", connection))
             {
                 command.CommandType = CommandType.StoredProcedure;
                 command.Parameters.AddWithValue("@AppointmentId", appointmentId);
                 connection.Open();

                 using (var reader = command.ExecuteReader())
                 {
                     // Read basic bill details
                     if (reader.Read())
                     {
                         billDetails.AppointmentId = Convert.ToInt32(reader["AppointmentId"]);
                         billDetails.PatientName = reader["PatientName"].ToString();
                     }

                     // Read bill item details
                     if (reader.NextResult())
                     {
                         while (reader.Read())
                         {
                             billDetails.BillItems.Add(new BillItemModel
                             {
                                 MedicineName = reader["MedicineName"].ToString(),
                                 Quantity = Convert.ToInt32(reader["Quantity"]),
                                 PricePerUnit = Convert.ToDecimal(reader["PricePerUnit"]),
                                 TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                 GST = Convert.ToDecimal(reader["GST"]),
                                 FinalAmount = Convert.ToDecimal(reader["FinalAmount"])
                             });
                         }
                     }
                 }
             }
         }

         // Calculate grand total
         billDetails.GrandTotal = billDetails.BillItems.Sum(item => item.FinalAmount);

         return billDetails;
     }
    */






