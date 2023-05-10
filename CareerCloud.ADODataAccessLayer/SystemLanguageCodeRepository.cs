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
    public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        private readonly string _connectionString = "Data Source=POWERNEWLIFE\\SQL2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                foreach (var item in items)
                {

                    using (var command = new SqlCommand())

                    {
                        command.CommandText = "INSERT INTO [dbo].[System_Language_Codes](LanguageID, Name,Native_Name )" +
                            "VALUES (@LanguageID, @Name, @Native_Name)";

                        command.Connection = conn;

                        command.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                        command.Parameters.AddWithValue("@Name", item.Name);
                        command.Parameters.AddWithValue("@Native_Name", item.NativeName);


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

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            var allSystemLanguage = new List<SystemLanguageCodePoco>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT LanguageID, Name, Native_Name FROM [dbo].[System_Language_Codes]", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allSystemLanguage.Add(new SystemLanguageCodePoco
                            {
                               
                                LanguageID = reader.GetString(0),
                                Name = reader.GetString(1),
                                NativeName = reader.GetString(2)
                            });
                        }
                    }
                }
            }

            return allSystemLanguage;
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM [dbo].[System_Language_Codes] WHERE LanguageID = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.LanguageID);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE [dbo].[System_Language_Codes] " +
                        " SET Name = @Name ," +
                        "     Native_Name = @Native_Name WHERE LanguageID = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.LanguageID);
                        command.Parameters.AddWithValue("@Name", item.Name);
                        command.Parameters.AddWithValue("@Native_Name", item.NativeName);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
