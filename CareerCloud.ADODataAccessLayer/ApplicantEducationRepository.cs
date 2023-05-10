using CareerCloud.Pocos;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CareerCloud.DataAccessLayer;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Linq;
using System.Reflection.PortableExecutable;


namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>  // Implement IDataRepository
    {

        private readonly string _connectionString = "Data Source=POWERNEWLIFE\\SQL2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";

        public void Add(params ApplicantEducationPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                foreach (var item in items)
                {

                    using (var command = new SqlCommand())

                    {
                        command.CommandText = "INSERT INTO Applicant_Educations(Id, Applicant, Major,Certificate_Diploma,Start_Date,Completion_Date,Completion_Percent)" +
                            "VALUES (@Id, @Applicant, @Major, @Certificate_Diploma, @Start_Date, @Completion_Date, @Completion_Percent)";

                        command.Connection= conn;

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Applicant", item.Applicant);
                        command.Parameters.AddWithValue("@Major", item.Major);
                        command.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                        command.Parameters.AddWithValue("@Start_Date", item.StartDate);
                        command.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                        command.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);


                        command.ExecuteNonQuery();
                    }

                }
            }

        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = name;
                SqlCommand command = new SqlCommand(query, connection);

                foreach (var item in parameters)
                {
                    command.Parameters.Add(new SqlParameter(item.Item1, SqlDbType.VarChar, 50).Value = item.Item2);
                }

                connection.Open();
                command.ExecuteNonQuery();
            }

        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            var allApplicantEducation = new List<ApplicantEducationPoco>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Applicant_Educations ", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allApplicantEducation.Add(new ApplicantEducationPoco
                            {
                                Id = reader.GetGuid(0),
                                Applicant = reader.GetGuid(1),
                                Major = reader.GetString(2),
                                CertificateDiploma = reader.GetString(3),
                                StartDate = reader.GetDateTime(4),
                                CompletionDate = reader.GetDateTime(5),
                                CompletionPercent = reader.GetByte(6)
                            });
                        }
                    }
                }
            }

            return allApplicantEducation;
        }


        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();  
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM [dbo].[Applicant_Educations] WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE [dbo].[Applicant_Educations] " +
                        " SET Applicant = @Applicant ," +
                        "     Major = @Major ," +
                        "     Certificate_Diploma = @CertificateDiploma ," +
                        "     Start_Date = @StartDate ," +
                        "     Completion_Date = @CompletionDate ," +
                        "     Completion_Percent = @CompletionPercent " +

                        "WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.Parameters.AddWithValue("@Applicant", item.Applicant);
                        command.Parameters.AddWithValue("@Major", item.Major);
                        command.Parameters.AddWithValue("@CertificateDiploma", item.CertificateDiploma);
                        command.Parameters.AddWithValue("@StartDate", item.StartDate);
                        command.Parameters.AddWithValue("@CompletionDate", item.CompletionDate);
                        command.Parameters.AddWithValue("@CompletionPercent", item.CompletionPercent);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}