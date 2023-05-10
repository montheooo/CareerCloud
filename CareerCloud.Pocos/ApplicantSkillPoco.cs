using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Skills")] // Annotation Table Mapping to Database
    public class ApplicantSkillPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; } // Id (Primary key)

        [Column("Applicant")]
        public Guid Applicant { get; set; } // Applicant

        [Column("Skill")]
        public string Skill { get; set; } // Skill

        [Column("Skill_Level")]
        public string SkillLevel { get; set; } // Skill Level

        [Column("Start_Month")]
        public byte StartMonth { get; set; } // Start Month

        [Column("Start_Year")]
        public int StartYear { get; set; } // StartYear

        [Column("End_Month")]
        public byte EndMonth { get; set; } // EndMonth

        [Column("End_Year")]
        public int EndYear { get; set; } // Application Date

        [Column("Time_Stamp")]
        [NotMapped]
        public byte[] TimeStamp { get; set; } // Time_Stamp (length: 8)

        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }
    }
}
