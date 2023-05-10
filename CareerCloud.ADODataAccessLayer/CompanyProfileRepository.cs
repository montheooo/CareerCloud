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
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        private readonly string _connectionString = "Data Source=POWERNEWLIFE\\SQL2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        public void Add(params CompanyProfilePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                foreach (var item in items)
                {

                    using (var command = new SqlCommand())

                    {
                        command.CommandText = "INSERT INTO [dbo].[Company_Profiles](Id, Registration_Date, Company_Website, Contact_Phone, Contact_Name, Company_Logo)" +
                            "VALUES (@Id, @Registration_Date, @Company_Website, @Contact_Phone, @Contact_Name, @Company_Logo)";

                        command.Connection = conn;

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                        command.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                        command.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                        command.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                        command.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);
                       


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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            var allCompanyProfile = new List<CompanyProfilePoco>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM [dbo].[Company_Profiles] ", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allCompanyProfile.Add(new CompanyProfilePoco
                            {
                                Id = reader.GetGuid(0),
                                RegistrationDate = reader.GetDateTime(1),
                                CompanyWebsite = reader.IsDBNull(2) ? null : reader.GetString(2),
                                ContactPhone = reader.IsDBNull(3) ? null : reader.GetString(3),
                                ContactName = reader.IsDBNull(4) ? null : reader.GetString(4)

                            });
                        }
                    }
                }
            }

            return allCompanyProfile;
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM [dbo].[Company_Profiles] WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE [dbo].[Company_Profiles] " +
                        " SET Registration_Date = @Registration_Date ," +
                        "     Company_Website = @Company_Website ," +
                        "     Contact_Phone = @Contact_Phone ," +
                        "     Contact_Name = @Contact_Name ," +
                        "     Company_Logo = @Company_Logo " +
                       

                        "WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                        command.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                        command.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                        command.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                        command.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
