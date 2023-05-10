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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.PortableExecutable;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyDescriptionRepository : IDataRepository<CompanyDescriptionPoco>
    {

        private readonly string _connectionString = "Data Source=POWERNEWLIFE\\SQL2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        public void Add(params CompanyDescriptionPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                foreach (var item in items)
                {

                    using (var command = new SqlCommand())

                    {
                        command.CommandText = "INSERT INTO [dbo].[Company_Descriptions](Id, Company, LanguageID, Company_Name, Company_Description)" +
                            "VALUES (@Id, @Company, @LanguageID, @Company_Name, @Company_Description)";

                        command.Connection = conn;

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Company", item.Company);
                        command.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                        command.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                        command.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);
                        

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

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            var allCompanyDescription = new List<CompanyDescriptionPoco>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM [dbo].[Company_Descriptions] ", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allCompanyDescription.Add(new CompanyDescriptionPoco
                            {
                                Id = reader.GetGuid(0),
                                Company = reader.GetGuid(1),
                                LanguageId = reader.GetString(2),
                                CompanyName = reader.GetString(3),
                                CompanyDescription = reader.GetString(4)
                                
                            });
                        }
                    }
                }
            }

            return allCompanyDescription;
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM [dbo].[Company_Descriptions] WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE [dbo].[Company_Descriptions] " +
                        " SET Company = @Company ," +
                        "     LanguageId = @LanguageId ," +
                        "     Company_Name = @CompanyName ," +
                        "     Company_Description = @CompanyDescription " +
                        

                        "WHERE Id = @id", connection))
                    {
                       
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.Parameters.AddWithValue("@Company", item.Company);
                        command.Parameters.AddWithValue("@LanguageId", item.LanguageId);
                        command.Parameters.AddWithValue("@CompanyName", item.CompanyName);
                        command.Parameters.AddWithValue("@CompanyDescription", item.CompanyDescription);
                        

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
