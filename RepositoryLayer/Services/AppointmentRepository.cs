using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using RepositoryLayer.SqlConnectionObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly SqlConnection sqlConnection = new SqlConnection();
        private readonly string sqlConnetionString;
        private readonly IConfiguration configuration;
        public AppointmentRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            sqlConnetionString = configuration.GetConnectionString("DBConnection");
            sqlConnection.ConnectionString = sqlConnetionString;
        }

        public bool BookAppointment(AppointmentModel appointment)
        {
            try
            {
                if (sqlConnection != null)
                {
                    SqlCommand sqlCommand = new SqlCommand("usp_BookAppointment", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("PatientId", appointment.PatientId);
                    sqlCommand.Parameters.AddWithValue("DoctorId", appointment.DoctorId);
                    sqlCommand.Parameters.AddWithValue("ConcernAbout", appointment.ConcernAbout);
                    sqlCommand.Parameters.AddWithValue("AppointmentDate", appointment.AppointmentDate);
                    sqlCommand.Parameters.AddWithValue("StartTime", appointment.StartTime);

                    sqlConnection.Open();
                    int nora = sqlCommand.ExecuteNonQuery();
                    return true;
                }
                else throw new Exception("Sql Connection not established");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { sqlConnection.Close(); }
        }

        public List<AppointmentModel> GetAllAppointments()
        {
            List<AppointmentModel> appointments = new List<AppointmentModel>();
            try
            {
                if (sqlConnection != null)
                {
                    SqlCommand sqlCommand = new SqlCommand("usp_GetAllAppointments", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlConnection.Open();
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        AppointmentModel appointmentModel = new AppointmentModel()
                        {
                            AppointmentId = (int)dataReader["AppointmentId"],
                            PatientId = (int)dataReader["PatientId"],
                            DoctorId = (int)dataReader["DoctorId"],
                            ConcernAbout = (string)dataReader["ConcernAbout"],
                            AppointmentDate = (DateTime)dataReader["AppointmentDate"],
                            StartTime = (TimeSpan)dataReader["StartTime"],
                            EndTime = (TimeSpan)dataReader["EndTime"]
                        };
                        appointments.Add(appointmentModel);
                    }
                    return appointments;
                }
                else throw new Exception("Sql Connection not established");
            }
            catch (Exception ex)
            {
                throw ex;
            }   
            finally { sqlConnection.Close(); }
        }

        public List<AppointmentPatientModel> PatientDetailsWithAppointedDoctor()
        {
            List<AppointmentPatientModel> appointmentsList = new List<AppointmentPatientModel>();
            try
            {
                if (sqlConnection != null)
                {
                    SqlCommand sqlCommand = new SqlCommand("usp_PatientDetailsWithAppointedDoctor", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlConnection.Open();
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        AppointmentPatientModel appointmentPatient = new AppointmentPatientModel()
                        {
                            AppointmentId = (int)dataReader["AppID"],
                            PatientId = (int)dataReader["PatientId"],
                            PatientName = (string)dataReader["FullName"],
                            Email = (string)dataReader["Email"],
                            Contact = (long)dataReader["Contact"],
                            DOB = (DateTime)dataReader["DOB"],
                            Age = (int)dataReader["Age"],
                            Gender = (string)dataReader["Gender"],
                            PatientImage = (string)dataReader["PatientImage"],
                            DoctorId = (int)dataReader["DID"],
                            DoctorName = (string)dataReader["DoctorName"],
                            DoctorImage = (string)dataReader["DImage"]
                        };
                        appointmentsList.Add(appointmentPatient);
                    }
                    return appointmentsList.ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { sqlConnection.Close(); }
        }
    }
}
