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
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        private readonly string _connectionString = "Data Source=POWERNEWLIFE\\SQL2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        public void Add(params CompanyLocationPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                foreach (var item in items)
                {

                    using (var command = new SqlCommand())

                    {
                        command.CommandText = "INSERT INTO [dbo].[Company_Locations](Id, Company, Country_Code, State_Province_Code, Street_Address, City_Town, Zip_Postal_Code)" +
                            "VALUES (@Id, @Company, @Country_Code, @State_Province_Code, @Street_Address, @City_Town, @Zip_Postal_Code)";

                        command.Connection = conn;

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Company", item.Company);
                        command.Parameters.AddWithValue("@Country_Code", item.CountryCode);
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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            var allCompanyLocation = new List<CompanyLocationPoco>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM [dbo].[Company_Locations] ", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allCompanyLocation.Add(new CompanyLocationPoco
                            {
                                Id = reader.GetGuid(0),
                                Company = reader.GetGuid(1),
                                CountryCode = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Province = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Street = reader.IsDBNull(4) ? null : reader.GetString(4),
                                City = reader.IsDBNull(5) ? null : reader.GetString(5),
                                PostalCode = reader.IsDBNull(6) ? null : reader.GetString(6)
                            });
                        }
                    }
                }
            }

            return allCompanyLocation;
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM [dbo].[Company_Locations] WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE [dbo].[Company_Locations] " +
                        " SET Company = @Company ," +
                        "     Country_Code = @Country_Code ," +
                        "     State_Province_Code = @State_Province_Code ," +
                        "     Street_Address = @Street_Address ," +
                        "     City_Town = @City_Town ," +
                        "     Zip_Postal_Code = @Zip_Postal_Code " +

                        "WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.Parameters.AddWithValue("@Company", item.Company);
                        command.Parameters.AddWithValue("@Country_Code", item.CountryCode);
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
