using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Company_Job_Skills")]
    public class CompanyJobSkillPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; } // Id (Primary key)

        [Column("Job")]
        public Guid Job { get; set; } // Job

        [Column("Skill")]
        public String Skill { get; set; } // Skill

        [Column("Skill_Level")]
        public String SkillLevel { get; set; } // Skill Level

        [Column("Importance")]
        public int Importance { get; set; } // Importance

        [Column("Time_Stamp")]
        [NotMapped]
        public byte[] TimeStamp { get; set; } // Time_Stamp (length: 8)

        public virtual CompanyJobPoco CompanyJob { get; set; }
    }
}
