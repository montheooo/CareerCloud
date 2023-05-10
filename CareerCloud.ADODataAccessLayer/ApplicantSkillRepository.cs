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
    public class ApplicantSkillRepository : IDataRepository<ApplicantSkillPoco>
    {
        private readonly string _connectionString = "Data Source=POWERNEWLIFE\\SQL2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        public void Add(params ApplicantSkillPoco[] items)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                foreach (var item in items)
                {

                    using (var command = new SqlCommand())

                    {
                        command.CommandText = "INSERT INTO [dbo].[Applicant_Skills](Id, Applicant, Skill, Skill_Level, Start_Month, Start_Year, End_Month, End_Year )" +
                            "VALUES (@Id, @Applicant, @Skill, @Skill_Level, @Start_Month, @Start_Year, @End_Month, @End_Year )";

                        command.Connection = conn;

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Applicant", item.Applicant);
                        command.Parameters.AddWithValue("@Skill", item.Skill);
                        command.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                        command.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                        command.Parameters.AddWithValue("@Start_Year", item.StartYear);
                        command.Parameters.AddWithValue("@End_Month", item.EndMonth);
                        command.Parameters.AddWithValue("@End_Year", item.EndYear);


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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            var allApplicantSkill = new List<ApplicantSkillPoco>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT Id, Applicant, Skill, Skill_Level, Start_Month, Start_Year, End_Month, End_Year FROM [dbo].[Applicant_Skills]", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allApplicantSkill.Add(new ApplicantSkillPoco
                            {
                                Id = reader.GetGuid(0),
                                Applicant = reader.GetGuid(1),
                                Skill = reader.GetString(2),
                                SkillLevel = reader.GetString(3),
                                StartMonth = reader.GetByte(4),
                                StartYear= reader.GetInt32(5),
                                EndMonth= reader.GetByte(6),
                                EndYear = reader.GetInt32(7)
                            });
                        }
                    }
                }
            }

            return allApplicantSkill;
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM [dbo].[Applicant_Skills]  WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            foreach (var item in items)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE [dbo].[Applicant_Skills]  " +
                        " SET Applicant = @Applicant ," +
                        "     Skill = @Skill ," +
                        "     Skill_Level = @Skill_Level ," +
                        "     Start_Month = @Start_Month ," +
                        "     Start_Year = @Start_Year ," +
                        "     End_Month = @End_Month ," +
                        "     End_Year = @End_Year " +

                        " WHERE Id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", item.Id);
                        command.Parameters.AddWithValue("@Applicant", item.Applicant);
                        command.Parameters.AddWithValue("@Skill", item.Skill);
                        command.Parameters.AddWithValue("Skill_Level", item.SkillLevel);
                        command.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                        command.Parameters.AddWithValue("@Start_Year", item.StartYear);
                        command.Parameters.AddWithValue("@End_Month", item.EndMonth);
                        command.Parameters.AddWithValue("@End_Year", item.EndYear);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
