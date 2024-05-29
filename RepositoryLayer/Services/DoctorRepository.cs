using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using RepositoryLayer.SqlConnectionObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class DoctorRepository : IDoctorsRepository
    {
        //private string connectionString = @"Data Source=DESKTOP-4OR4SUH\SQLEXPRESS;Initial Catalog=HospitalManagementSystem_MVC;Integrated Security=True";
        private readonly SqlConnection sqlConnection = new SqlConnection();
        private readonly string SqlConnectionString;
        private readonly IConfiguration configuration;
        public DoctorRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            SqlConnectionString = configuration.GetConnectionString("DBConnection");
            sqlConnection.ConnectionString = SqlConnectionString;
        }
        public bool AddDoctor(Doctor doctor)
        {
            try
            {
                if (sqlConnection != null)
                {
                    SqlCommand sqlCommand = new SqlCommand("usp_InsertDoctor", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@FullName", doctor.FullName);
                    sqlCommand.Parameters.AddWithValue("@Email", doctor.Email);
                    sqlCommand.Parameters.AddWithValue("@Contact", doctor.Contact);
                    sqlCommand.Parameters.AddWithValue("@DOB", doctor.DOB);
                    sqlCommand.Parameters.AddWithValue("@Gender", doctor.Gender);
                    sqlCommand.Parameters.AddWithValue("@Qualification", doctor.Qualification);
                    sqlCommand.Parameters.AddWithValue("@Specialization", doctor.Specialization);
                    sqlCommand.Parameters.AddWithValue("@Experience", doctor.Experience);
                    sqlCommand.Parameters.AddWithValue("@DoctorImage", doctor.DoctorImage);

                    sqlConnection.Open();
                    int nora = sqlCommand.ExecuteNonQuery();
                    //if (nora > 0)
                    return true;
                    //else throw new Exception("Failed to add Doctor"); 
                }
                else throw new Exception("SqlConnection is not established");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { sqlConnection.Close(); }
        }

        public List<Doctor> GetAllDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            try
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("exec usp_FetchAllDoctors", sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Doctor doctor = new Doctor()
                        {
                            DoctorId = (int)sqlDataReader["DoctorId"],
                            FullName = (string)sqlDataReader["FullName"],
                            Email = (string)sqlDataReader["Email"],
                            Contact = (long)sqlDataReader["Contact"],
                            DOB = (DateTime)sqlDataReader["DOB"],
                            Age = (Decimal)sqlDataReader["Age"],
                            Gender = (string)sqlDataReader["Gender"],
                            Qualification = (string)sqlDataReader["Qualification"],
                            Specialization = (string)sqlDataReader["Specialization"],
                            Experience = (int)sqlDataReader["Experience"],
                            DoctorImage = (string)sqlDataReader["DoctorImage"],
                            IsTrash = (bool)sqlDataReader["IsTrash"],
                            CreatedAt = (DateTime)sqlDataReader["CreatedAt"],
                            UpdatedAt = (DateTime)sqlDataReader["UpdatedAt"]
                        };
                        doctors.Add(doctor);
                    }
                    return doctors;
                }
                else throw new Exception("SqlConnection is not established");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { sqlConnection.Close(); };
        }

        public Doctor GetDoctorById(int doctorID)
        {
            Doctor doctor = null;
            try
            {
                if (sqlConnection != null)
                {
                    SqlCommand sqlCommand = new SqlCommand("exec usp_GetDoctorById '" + doctorID + "' ", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        doctor = new Doctor()
                        {
                            DoctorId = doctorID,
                            FullName = (string)dataReader["FullName"],
                            Email = (string)dataReader["Email"],
                            Contact = (long)dataReader["Contact"],
                            DOB = (DateTime)dataReader["DOB"],
                            Age = (Decimal)dataReader["Age"],
                            Gender = (string)dataReader["Gender"],
                            Qualification = (string)dataReader["Qualification"],
                            Specialization = (string)dataReader["Specialization"],
                            Experience = (int)dataReader["Experience"],
                            DoctorImage = (string)dataReader["DoctorImage"]
                        };
                    }
                    return doctor;
                }
                else throw new Exception("SqlConnection is not established");
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                throw ex;
            }
            finally { sqlConnection.Close(); };
            return null;
        }

        public bool UpadateDoctor(Doctor doctor)
        {

            try
            {
                if (sqlConnection != null)
                {
                    SqlCommand sqlCommand = new SqlCommand("usp_UpdateDoctor", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@DoctorId", doctor.DoctorId);
                    sqlCommand.Parameters.AddWithValue("@FullName", doctor.FullName);
                    sqlCommand.Parameters.AddWithValue("@Email", doctor.Email);
                    sqlCommand.Parameters.AddWithValue("@Contact", doctor.Contact);
                    sqlCommand.Parameters.AddWithValue("@DOB", doctor.DOB);
                    sqlCommand.Parameters.AddWithValue("@Gender", doctor.Gender);
                    sqlCommand.Parameters.AddWithValue("@Qualification", doctor.Qualification);
                    sqlCommand.Parameters.AddWithValue("@Specialization", doctor.Specialization);
                    sqlCommand.Parameters.AddWithValue("@Experience", doctor.Experience);
                    sqlCommand.Parameters.AddWithValue("@DoctorImage", doctor.DoctorImage);

                    sqlConnection.Open();
                    int nora = sqlCommand.ExecuteNonQuery();
                    return true;
                }
                else throw new Exception("SqlConnection is not established");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { sqlConnection.Close(); }
        }

        public bool DeleteDoctor(int doctorID)
        {
            try
            {
                if (sqlConnection != null)
                {
                    SqlCommand sqlCommand = new SqlCommand("usp_DeleteDoctor", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@DoctorId", doctorID);

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    return true;
                }
                else throw new Exception("SqlConnection is not established");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { sqlConnection.Close(); }
        }

    }
}
