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
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        private readonly string _connectionString = "Data Source=POWERNEWLIFE\\SQL2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        public void Add(params SecurityLoginPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                foreach (var item in items)
                {

                    using (var command = new SqlCommand())

                    {
                        command.CommandText = "INSERT INTO [dbo].[Security_Logins](Id, Login, Password, Created_Date, Password_Update_Date, Agreement_Accepted_Date, Is_Locked, Is_Inactive, Email_Address, Phone_Number, Full_Name, Force_Change_Password, Prefferred_Language)" +
                            "VALUES (@Id, @Login, @Password, @Created_Date, @Password_Update_Date, @Agreement_Accepted_Date, @Is_Locked, @Is_Inactive, @Email_Address, @Phone_Number, @Full_Name, @Force_Change_Password, @Prefferred_Language)";

                        command.Connection = conn;

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Login", item.Login);
                        command.Parameters.AddWithValue("@Password", item.Password);
                        command.Parameters.AddWithValue("@Created_Date", item.Created);
                        command.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                        command.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                        command.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                        command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                        command.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                        command.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                        command.Parameters.AddWithValue("@Full_Name", item.FullName);
                        command.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                        command.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);


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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            var allSecurityLogin = new List<SecurityLoginPoco>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM [dbo].[Security_Logins] ", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allSecurityLogin.Add(new SecurityLoginPoco
                            {
                                Id = reader.GetGuid(0),
                                Login = reader.GetString(1),
                                Password = reader.GetString(2),
                                Created = reader.GetDateTime(3),
                                PasswordUpdate = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                                AgreementAccepted = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
                                IsLocked = reader.GetBoolean(6),
                                IsInactive = reader.GetBoolean(7),
                                EmailAddress = reader.GetString(8),
                                PhoneNumber = reader.IsDBNull(9) ? null : reader.GetString(9),
                                FullName = reader.IsDBNull(10) ? null : reader.GetString(10),
                                ForceChangePassword = reader.GetBoolean(11),
                                PrefferredLanguage = reader.IsDBNull(12) ? null : reader.GetString(12)
                            });
                        }
                    }
                }
            }

            return allSecurityLogin;
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault(); ;
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM [dbo].[Security_Logins] WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE [dbo].[Security_Logins] " +
                        " SET Login = @Login ," +
                        "     Password = @Password ," +
                        "     Created_Date = @Created_Date ," +
                        "     Password_Update_Date = @Password_Update_Date ," +
                        "     Agreement_Accepted_Date = @Agreement_Accepted_Date ," +
                        "     Is_Locked = @Is_Locked ," +
                        "     Is_Inactive = @Is_Inactive ," +
                        "     Email_Address = @Email_Address ," +
                        "     Phone_Number = @Phone_Number ," +
                        "     Full_Name = @Full_Name ," +
                        "     Force_Change_Password = @Force_Change_Password ," +
                        "     Prefferred_Language = @Prefferred_Language " +

                        "WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.Parameters.AddWithValue("@Login", item.Login);
                        command.Parameters.AddWithValue("@Password", item.Password);
                        command.Parameters.AddWithValue("@Created_Date", item.Created);
                        command.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                        command.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                        command.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                        command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                        command.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                        command.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                        command.Parameters.AddWithValue("@Full_Name", item.FullName);
                        command.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                        command.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
