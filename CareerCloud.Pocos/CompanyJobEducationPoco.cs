using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Company_Job_Educations")]
    public class CompanyJobEducationPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; } // Id (Primary key)

        [Column("Job")]
        public Guid Job { get; set; } // Job

        [Column("Major")]
        public String Major { get; set; } // Major

        [Column("Importance")]
        public Int16 Importance { get; set; } // Importance

        [Column("Time_Stamp")]
        [NotMapped]
        public byte[] TimeStamp { get; set; } // Time_Stamp (length: 8)

        public virtual CompanyJobPoco CompanyJob { get; set; }
    }
}
