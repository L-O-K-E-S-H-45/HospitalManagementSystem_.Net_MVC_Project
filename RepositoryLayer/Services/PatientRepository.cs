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
    public class PatientRepository : IPatientRepository
    {
        private readonly SqlConnection sqlConnection = new SqlConnection();
        private readonly string sqlConnectionString;
        private readonly IConfiguration configuration;
        public PatientRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            sqlConnectionString = configuration.GetConnectionString("DBConnection");
            sqlConnection.ConnectionString = sqlConnectionString;
        }
        public bool RegisterPatient(Patient patient)
        {
            try
            {
                if (sqlConnection != null)
                {
                    SqlCommand sqlCommand = new SqlCommand("usp_InsertPatient", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@FullName", patient.FullName);
                    sqlCommand.Parameters.AddWithValue("@Email", patient.Email);
                    sqlCommand.Parameters.AddWithValue("@Contact", patient.Contact);
                    sqlCommand.Parameters.AddWithValue("@DOB",patient.DOB);
                    sqlCommand.Parameters.AddWithValue("@Gender", patient.Gender);
                    sqlCommand.Parameters.AddWithValue("@PatientImage", patient.PatientImage);

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
        public List<Patient> GetAllPatients()
        {
            List<Patient> patients = new List<Patient>();

            try
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("exec usp_GetAllPatients", sqlConnection);
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        if (!(bool)dataReader["IsTrash"])
                        {
                            Patient patient = new Patient()
                            {
                                PatientId = (int)dataReader["PatientId"],
                                FullName = (string)dataReader["FullName"],
                                Email = (string)dataReader["Email"],
                                Contact = (long)dataReader["Contact"],
                                DOB = (DateTime)dataReader["DOB"],
                                Age = (int)dataReader["Age"],
                                Gender = (string)dataReader["Gender"],
                                PatientImage = (string)dataReader["PatientImage"],
                                IsTrash = (bool)dataReader["IsTrash"],
                                CreatedAt = (DateTime)dataReader["CreatedAt"],
                                UpdatedAt = (DateTime)dataReader["UpdatedAt"]
                            };
                            patients.Add(patient);
                        }
                    }
                    return patients;
                }
                else throw new Exception("Sql Connection not established");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { sqlConnection.Close(); }
        }

        public Patient GetPatientById(int patientId)
        {
            Patient patient = null;
            try
            {
                if (sqlConnection != null)
                {
                    SqlCommand sqlCommand = new SqlCommand("usp_GetPatientById", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@PatientId", patientId);

                    sqlConnection.Open();
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        patient = new Patient()
                        {
                            PatientId = (int)dataReader["PatientId"],
                            FullName = (string)dataReader["FullName"],
                            Email = (string)dataReader["Email"],
                            Contact = (long)dataReader["Contact"],
                            DOB = (DateTime)dataReader["DOB"],
                            Age = (int)dataReader["Age"],
                            Gender = (string)dataReader["Gender"],
                            PatientImage = (string)dataReader["PatientImage"],
                            IsTrash = (bool)dataReader["IsTrash"],
                            CreatedAt = (DateTime)dataReader["CreatedAt"],
                            UpdatedAt = (DateTime)dataReader["UpdatedAt"]
                        };
                    }
                    return patient;
                }
                else throw new Exception("Sql Connection not established");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { sqlConnection.Close(); }
        }

        public bool UpdatePatient(Patient patient)
        {
            try
            {
                if (sqlConnection != null)
                {
                    SqlCommand sqlCommand = new SqlCommand("usp_UpdatePatient", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@PatientId", patient.PatientId);
                    sqlCommand.Parameters.AddWithValue("@FullName", patient.FullName);
                    sqlCommand.Parameters.AddWithValue("@Email", patient.Email);
                    sqlCommand.Parameters.AddWithValue("@Contact", patient.Contact);
                    sqlCommand.Parameters.AddWithValue("@DOB", patient.DOB);
                    sqlCommand.Parameters.AddWithValue("@Gender", patient.Gender);
                    sqlCommand.Parameters.AddWithValue("@PatientImage", patient.PatientImage);

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
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

        public bool DeletePatient(int patientId)
        {
            try
            {
                if (sqlConnection != null)
                {
                    SqlCommand sqlCommand = new SqlCommand("usp_DeletePatient", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@PatientId", patientId);

                    sqlConnection.Open();
                    sqlCommand.ExecuteReader();
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
    }
}
