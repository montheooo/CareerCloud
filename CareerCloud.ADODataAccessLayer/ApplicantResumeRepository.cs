using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Data;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantResumeRepository : IDataRepository<ApplicantResumePoco>
    {
        private readonly string _connectionString = "Data Source=POWERNEWLIFE\\SQL2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        public void Add(params ApplicantResumePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                foreach (var item in items)
                {

                    using (var command = new SqlCommand())

                    {
                        command.CommandText = "INSERT INTO [dbo].[Applicant_Resumes](Id, Applicant, Resume, Last_Updated)" +
                            "VALUES (@Id, @Applicant, @Resume, @Last_Updated )";

                        command.Connection = conn;

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Applicant", item.Applicant);
                        command.Parameters.AddWithValue("@Resume", item.Resume);
                        command.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);
                        //command.Parameters.AddWithValue("@Start_Date", item.StartDate);
                        //command.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                        //command.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);

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

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            var allApplicantResume = new List<ApplicantResumePoco>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM [dbo].[Applicant_Resumes]", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allApplicantResume.Add(new ApplicantResumePoco
                            {
                                Id = reader.GetGuid(0),
                                Applicant = reader.GetGuid(1),
                                Resume = reader.GetString(2),
                                LastUpdated = reader.IsDBNull(3) ? null: reader.GetDateTime(3),
                            });
                        }
                    }
                }
            }

            return allApplicantResume;
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM [dbo].[Applicant_Resumes] WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE [dbo].[Applicant_Resumes] " +
                        " SET Applicant = @Applicant ," +
                        "     Resume = @Resume ," +
                        "     Last_Updated = @Last_Updated " +
                        " WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.Parameters.AddWithValue("@Applicant", item.Applicant);
                        command.Parameters.AddWithValue("@Resume", item.Resume);
                        command.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
