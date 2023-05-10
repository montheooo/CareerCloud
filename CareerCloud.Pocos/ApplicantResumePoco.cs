using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Resumes")] // Annotation Table Mapping to Database
    public class ApplicantResumePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; } // Id (Primary key)

        [Column("Applicant")]
        public Guid Applicant { get; set; } // Applicant

        [Column("Resume")]
        public string Resume { get; set; } // Job

        [Column("Last_Updated")]
        public DateTime? LastUpdated { get; set; } // Application Date

        [Column("Time_Stamp")]
        [NotMapped]
        public byte[] TimeStamp { get; set; } // Time_Stamp (length: 8)

        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }
    }

}
