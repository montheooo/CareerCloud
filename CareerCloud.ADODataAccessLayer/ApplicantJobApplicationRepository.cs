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
    public class ApplicantJobApplicationRepository : IDataRepository<ApplicantJobApplicationPoco>
    {
        private readonly string _connectionString = "Data Source=POWERNEWLIFE\\SQL2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";


        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties) {

            var allApplicantJobApplication = new List<ApplicantJobApplicationPoco>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT Id, Applicant, Job, Application_Date FROM [Applicant_Job_Applications]", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allApplicantJobApplication.Add(new ApplicantJobApplicationPoco
                            {
                                Id = reader.GetGuid(0),
                                Applicant = reader.GetGuid(1),
                                Job = reader.GetGuid(2),
                                ApplicationDate= reader.GetDateTime(3), 
                            });
                        }
                    }
                }
            }

            return allApplicantJobApplication;
        }
        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }
        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }
        public void Add(params ApplicantJobApplicationPoco[] items) {

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                foreach (var item in items)
                {

                    using (var command = new SqlCommand())

                    {
                        command.CommandText = "INSERT INTO [dbo].[Applicant_Job_Applications](Id, Applicant, Job, Application_Date)" +
                            "VALUES (@Id, @Applicant, @Job, @Application_Date)";

                        command.Connection = conn;

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Applicant", item.Applicant);
                        command.Parameters.AddWithValue("@Job", item.Job);
                        command.Parameters.AddWithValue("@Application_Date", item.ApplicationDate);
                        //command.Parameters.AddWithValue("@Start_Date", item.StartDate);
                        //command.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                        //command.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);

                        command.ExecuteNonQuery();
                    }

                }
            }

        }
        public void Update(params ApplicantJobApplicationPoco[] items) {

            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE [dbo].[Applicant_Job_Applications] " +
                        " SET Applicant = @Applicant ," +
                        "     Job = @Job ," +
                        "     Application_Date = @ApplicationDate" +
                        " WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.Parameters.AddWithValue("@Applicant", item.Applicant);
                        command.Parameters.AddWithValue("@Job", item.Job);
                        command.Parameters.AddWithValue("@ApplicationDate", item.ApplicationDate);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
        public void Remove(params ApplicantJobApplicationPoco[] items) {

            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM [dbo].[Applicant_Job_Applications] WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }

        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters) {

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
    }
}
