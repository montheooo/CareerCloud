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
    public class SecurityLoginsRoleRepository : IDataRepository<SecurityLoginsRolePoco>
    {
        private readonly string _connectionString = "Data Source=POWERNEWLIFE\\SQL2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        public void Add(params SecurityLoginsRolePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                foreach (var item in items)
                {

                    using (var command = new SqlCommand())

                    {
                        command.CommandText = "INSERT INTO [dbo].[Security_Logins_Roles](Id, Login, Role)" +
                            "VALUES (@Id, @Login, @Role)";

                        command.Connection = conn;

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Login", item.Login);
                        command.Parameters.AddWithValue("@Role", item.Role);

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

        public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            var allSecurityLoginsRole = new List<SecurityLoginsRolePoco>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM [dbo].[Security_Logins_Roles] ", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allSecurityLoginsRole.Add(new SecurityLoginsRolePoco
                            {
                                Id = reader.GetGuid(0),
                                Login = reader.GetGuid(1),
                                Role = reader.GetGuid(2)
                               
                            });
                        }
                    }
                }
            }

            return allSecurityLoginsRole;
        }

        public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsRolePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM [dbo].[Security_Logins_Roles] WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(params SecurityLoginsRolePoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE [dbo].[Security_Logins_Roles] " +
                        " SET Login = @Login ," +
                        "     Role = @Role " +

                        "WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.Parameters.AddWithValue("@Login", item.Login);
                        command.Parameters.AddWithValue("@Role", item.Role);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
