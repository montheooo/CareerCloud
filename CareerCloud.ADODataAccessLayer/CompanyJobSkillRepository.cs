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
    public class CompanyJobSkillRepository : IDataRepository<CompanyJobSkillPoco>
    {
        private readonly string _connectionString = "Data Source=POWERNEWLIFE\\SQL2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        public void Add(params CompanyJobSkillPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                foreach (var item in items)
                {

                    using (var command = new SqlCommand())

                    {
                        command.CommandText = "INSERT INTO [dbo].[Company_Job_Skills](Id, Job, Skill, Skill_Level, Importance)" +
                            "VALUES (@Id, @Job, @Skill, @Skill_Level, @Importance)";

                        command.Connection = conn;

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Job", item.Job);
                        command.Parameters.AddWithValue("@Skill", item.Skill);
                        command.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                        command.Parameters.AddWithValue("@Importance", item.Importance);


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

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            var allCompanyJobSkill = new List<CompanyJobSkillPoco>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM [dbo].[Company_Job_Skills] ", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allCompanyJobSkill.Add(new CompanyJobSkillPoco
                            {
                                Id = reader.GetGuid(0),
                                Job = reader.GetGuid(1),
                                Skill = reader.GetString(2),
                                SkillLevel = reader.GetString(3),
                                Importance = reader.GetInt32(4),
                                
                            });
                        }
                    }
                }
            }

            return allCompanyJobSkill;
        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM [dbo].[Company_Job_Skills] WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE [dbo].[Company_Job_Skills] " +
                        " SET Job = @Job ," +
                        "     Skill = @Skill ," +
                        "     Skill_Level = @Skill_Level ," +
                        "     Importance = @Importance " +

                        "WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.Parameters.AddWithValue("@Job", item.Job);
                        command.Parameters.AddWithValue("@Skill", item.Skill);
                        command.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                        command.Parameters.AddWithValue("@Importance", item.Importance);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
