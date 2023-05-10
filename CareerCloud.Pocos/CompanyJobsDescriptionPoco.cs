using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    // Company_Jobs_Descriptions

    [Table("Company_Jobs_Descriptions")] // Annotation Table Mapping to Database
    public class CompanyJobDescriptionPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; } // Id (Primary key)

        [Column("Job")]
        public Guid Job { get; set; } // Job

        [Column("Job_Name")]
        public string JobName { get; set; } // Job_Name (length: 100)

        [Column("Job_Descriptions")]
        public string JobDescriptions { get; set; } // Job_Descriptions (length: 1000)

        [Column("Time_Stamp")]
        [NotMapped]
        public byte[] TimeStamp { get; set; } // Time_Stamp (length: 8)

        public virtual CompanyJobPoco CompanyJob { get; set; }


    }

}
