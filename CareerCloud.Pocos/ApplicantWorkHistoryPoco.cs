using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Work_History")] // Annotation Table Mapping to Database
    public class ApplicantWorkHistoryPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; } // Id (Primary key)

        [Column("Applicant")]
        public Guid Applicant { get; set; } // Applicant

        [Column("Company_Name")]
        public string CompanyName { get; set; } // CompanyName

        [Column("Country_Code")]
        public string CountryCode { get; set; } // Country Code

        [Column("Location")]
        public string Location { get; set; } // Location

        [Column("Job_Title")]
        public string JobTitle { get; set; } // Job Title

        [Column("Job_Description")]
        public string JobDescription { get; set; } // Job Description

        [Column("Start_Month")]
        public short StartMonth { get; set; } // Start Month

        [Column("Start_Year")]
        public int StartYear { get; set; } // Start Year

        [Column("End_Month")]
        public short EndMonth { get; set; } // End Month

        [Column("End_Year")]
        public int EndYear { get; set; } // End Year

        [Column("Time_Stamp")]
        [NotMapped]
        public byte[] TimeStamp { get; set; } // Time_Stamp (length: 8)

        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }

        public virtual SystemCountryCodePoco SystemCountryCode { get; set; }
    }
}
