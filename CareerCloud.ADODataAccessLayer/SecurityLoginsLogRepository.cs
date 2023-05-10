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

namespace CareerCloud.ADODataAccessLayer
{

    public class SecurityLoginsLogRepository : IDataRepository<SecurityLoginsLogPoco>
    {
        private readonly string _connectionString = "Data Source=POWERNEWLIFE\\SQL2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                foreach (var item in items)
                {

                    using (var command = new SqlCommand())

                    {
                        command.CommandText = "INSERT INTO [dbo].[Security_Logins_Log](Id, Login, Source_IP, Logon_Date, Is_Succesful)" +
                            "VALUES (@Id, @Login, @Source_IP, @Logon_Date, @Is_Succesful)";

                        command.Connection = conn;

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Login", item.Login);
                        command.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                        command.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                        command.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);


                        command.ExecuteNonQuery();
                    }

                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            var allSecurityLoginsLog = new List<SecurityLoginsLogPoco>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM [dbo].[Security_Logins_Log] ", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allSecurityLoginsLog.Add(new SecurityLoginsLogPoco
                            {
                                Id = reader.GetGuid(0),
                                Login = reader.GetGuid(1),
                                SourceIP = reader.GetString(2),
                                LogonDate = reader.GetDateTime(3),
                                IsSuccesful = reader.GetBoolean(4)
                                
                            });
                        }
                    }
                }
            }

            return allSecurityLoginsLog;
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM [dbo].[Security_Logins_Log] WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE [dbo].[Security_Logins_Log] " +
                        " SET Login = @login ," +
                        "     Source_IP = @Source_IP ," +
                        "     Logon_Date = @Logon_Date ," +
                        "     Is_Succesful = @Is_Succesful " +

                        "WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.Parameters.AddWithValue("@login", item.Login);
                        command.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                        command.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                        command.Parameters.AddWithValue("Is_Succesful", item.IsSuccesful);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
