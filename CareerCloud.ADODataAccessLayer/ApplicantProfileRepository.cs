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
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {

        private readonly string _connectionString = "Data Source=POWERNEWLIFE\\SQL2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                foreach (var item in items)
                {

                    using (var command = new SqlCommand())

                    {
                        command.CommandText = "INSERT INTO [dbo].[Applicant_Profiles]" +
                            "(Id, Login, Current_Salary,Current_Rate,Currency,Country_Code,State_Province_Code,Street_Address,City_Town,Zip_Postal_Code)" +
                            "VALUES (@Id, @Login, @Current_Salary, @Current_Rate, @Currency, @Country_Code, @State_Province_Code,@Street_Address,@City_Town,@Zip_Postal_Code)";

                        command.Connection = conn;

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Login", item.Login);
                        command.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                        command.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                        command.Parameters.AddWithValue("@Currency", item.Currency);
                        command.Parameters.AddWithValue("@Country_Code", item.Country);
                        command.Parameters.AddWithValue("@State_Province_Code", item.Province);
                        command.Parameters.AddWithValue("@Street_Address", item.Street);
                        command.Parameters.AddWithValue("@City_Town", item.City);
                        command.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);
                        

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

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            var allApplicantProfile = new List<ApplicantProfilePoco>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT Id, Login, Current_Salary, Current_Rate, Currency, Country_Code, State_Province_Code, Street_Address ," +
                    "City_Town, Zip_Postal_Code " +
                    "FROM [dbo].[Applicant_Profiles]", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allApplicantProfile.Add(new ApplicantProfilePoco
                            {
                                Id = reader.GetGuid(0),
                                Login = reader.GetGuid(1),
                                CurrentSalary = reader.GetDecimal(2),
                                CurrentRate = reader.GetDecimal(3),
                                Currency = reader.GetString(4),
                                Country= reader.GetString(5),
                                Province= reader.GetString(6),
                                Street= reader.GetString(7),
                                City= reader.GetString(8),
                                PostalCode= reader.GetString(9)
                            });
                        }
                    }
                }
            }

            return allApplicantProfile;
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM [dbo].[Applicant_Profiles] WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE [dbo].[Applicant_Profiles] " +
                        " SET Login = @Login ," +
                        "     Current_Salary = @Current_Salary ," +
                        "     Current_Rate = @Current_Rate ," +
                        "     Currency = @Currency ," +
                        "     Country_Code = @Country_Code ," +
                        "     State_Province_Code = @State_Province_Code ," +
                        "     Street_Address = @Street_Address ," +
                        "     City_Town = @City_Town ," +
                        "     Zip_Postal_Code = @Zip_Postal_Code " +

                        "WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.Parameters.AddWithValue("@Login", item.Login);
                        command.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                        command.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                        command.Parameters.AddWithValue("@Currency", item.Currency);
                        command.Parameters.AddWithValue("@Country_Code", item.Country);
                        command.Parameters.AddWithValue("@State_Province_Code", item.Province);
                        command.Parameters.AddWithValue("@Street_Address", item.Street);
                        command.Parameters.AddWithValue("@City_Town", item.City);
                        command.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
