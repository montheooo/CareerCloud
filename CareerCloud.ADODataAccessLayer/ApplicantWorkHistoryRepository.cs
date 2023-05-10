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
    public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
    {
        private readonly string _connectionString = "Data Source=POWERNEWLIFE\\SQL2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                foreach (var item in items)
                {

                    using (var command = new SqlCommand())

                    {
                        command.CommandText = "INSERT INTO [dbo].[Applicant_Work_History](Id, Applicant,Company_Name, Country_Code,Location,Job_Title, Job_Description, Start_Month,Start_Year,End_Month,End_Year )" +
                            "VALUES (@Id, @Applicant, @Company_Name, @Country_Code, @Location, @Job_Title, @Job_Description, @Start_Month, @Start_Year, @End_Month, @End_Year )";

                        command.Connection = conn;

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Applicant", item.Applicant);
                        command.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                        command.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                        command.Parameters.AddWithValue("@Location", item.Location);
                        command.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                        command.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                        command.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                        command.Parameters.AddWithValue("@Start_Year", item.StartYear);
                        command.Parameters.AddWithValue("@End_Month", item.EndMonth);
                        command.Parameters.AddWithValue("@End_Year", item.EndYear);


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

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            var allApplicantWorkHistory = new List<ApplicantWorkHistoryPoco>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM [dbo].[Applicant_Work_History] ", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allApplicantWorkHistory.Add(new ApplicantWorkHistoryPoco
                            {
                                Id = reader.GetGuid(0),
                                Applicant = reader.GetGuid(1),
                                CompanyName = reader.GetString(2),
                                CountryCode = reader.GetString(3),
                                Location = reader.GetString(4),
                                JobTitle = reader.GetString(5),
                                JobDescription = reader.GetString(6),
                                StartMonth = reader.GetInt16(7),
                                StartYear = reader.GetInt32(8),
                                EndMonth = reader.GetInt16(9),
                                EndYear = reader.GetInt32(10)
                            });
                        }
                    }
                }
            }

            return allApplicantWorkHistory;
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM [dbo].[Applicant_Work_History] WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE [dbo].[Applicant_Work_History] " +
                        " SET Applicant = @Applicant ," +
                        "     Company_Name = @Company_Name ," +
                        "     Country_Code = @Country_Code ," +
                        "     Location = @Location ," +
                        "     Job_Title = @Job_Title ," +
                        "     Job_Description = @Job_Description ," +
                        "     Start_Month = @Start_Month ," +
                        "     Start_Year = @Start_Year ," +
                        "     End_Month = @End_Month ," +
                        "     End_Year = @End_Year " +

                        "WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.Parameters.AddWithValue("@Applicant", item.Applicant);
                        command.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                        command.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                        command.Parameters.AddWithValue("@Location", item.Location);
                        command.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                        command.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                        command.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                        command.Parameters.AddWithValue("@Start_Year", item.StartYear);
                        command.Parameters.AddWithValue("@End_Month", item.EndMonth);
                        command.Parameters.AddWithValue("@End_Year", item.EndYear);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
