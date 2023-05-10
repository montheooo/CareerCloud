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
    public class CompanyJobRepository : IDataRepository<CompanyJobPoco>
    {
        private readonly string _connectionString = "Data Source=POWERNEWLIFE\\SQL2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        public void Add(params CompanyJobPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                foreach (var item in items)
                {

                    using (var command = new SqlCommand())

                    {
                        command.CommandText = "INSERT INTO [dbo].[Company_Jobs](Id, Company, Profile_Created, Is_Inactive,Is_Company_Hidden)" +
                            "VALUES (@Id, @Company, @Profile_Created, @Is_Inactive, @Is_Company_Hidden)";

                        command.Connection = conn;

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Company", item.Company);
                        command.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                        command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                        command.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);


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

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            var allCompanyJob = new List<CompanyJobPoco>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM [dbo].[Company_Jobs] ", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allCompanyJob.Add(new CompanyJobPoco
                            {
                                Id = reader.GetGuid(0),
                                Company = reader.GetGuid(1),
                                ProfileCreated = reader.GetDateTime(2),
                                IsInactive = reader.GetBoolean(3),
                                IsCompanyHidden = reader.GetBoolean(4)
                                
                            });
                        }
                    }
                }
            }

            return allCompanyJob;
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM [dbo].[Company_Jobs] WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE [dbo].[Company_Jobs] " +
                        " SET Company = @Company ," +
                        "     Profile_Created = @Profile_Created ," +
                        "     Is_Inactive = @Is_Inactive ," +
                        "     Is_Company_Hidden = @Is_Company_Hidden " +
                        
                        "WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.Parameters.AddWithValue("@Company", item.Company);
                        command.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                        command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                        command.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);
                        

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }

}
