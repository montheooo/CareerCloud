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
    public class SecurityRoleRepository : IDataRepository<SecurityRolePoco>
    {
        private readonly string _connectionString = "Data Source=POWERNEWLIFE\\SQL2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        public void Add(params SecurityRolePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                foreach (var item in items)
                {

                    using (var command = new SqlCommand())

                    {
                        command.CommandText = "INSERT INTO [dbo].[Security_Roles](Id, Role, Is_Inactive)" +
                            "VALUES (@Id, @Role, @Is_Inactive)";

                        command.Connection = conn;

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Role", item.Role);
                        command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);


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

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            var allSecurityRole = new List<SecurityRolePoco>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM [dbo].[Security_Roles] ", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allSecurityRole.Add(new SecurityRolePoco
                            {
                                Id = reader.GetGuid(0),
                                Role = reader.GetString(1),
                                IsInactive = reader.GetBoolean(2),
                                
                            });
                        }
                    }
                }
            }

            return allSecurityRole;
        }

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityRolePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityRolePoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM [dbo].[Security_Roles] WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(params SecurityRolePoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE [dbo].[Security_Roles] " +
                        " SET Role = @Role ," +
                        "     Is_Inactive = @Is_Inactive " +

                        "WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.Parameters.AddWithValue("@Role", item.Role);
                        command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}

