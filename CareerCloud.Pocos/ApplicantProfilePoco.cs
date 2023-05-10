using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Profiles")] // Annotation Table Mapping to Database
    public class ApplicantProfilePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; } // Id (Primary key)

        [Column("Login")]
        public Guid Login { get; set; } // Login

        [Column("Current_Salary")]
        public decimal? CurrentSalary { get; set; } // Current salary

        [Column("Current_Rate")]
        public decimal? CurrentRate { get; set; } // Current rate

        [Column("Currency")]
        public string Currency { get; set; } // Currency

        [Column("Country_Code")]
        public string Country { get; set; } // Country Code

        [Column("State_Province_Code")]
        public string Province { get; set; } // State Province Code

        [Column("Street_Address")]
        public string Street { get; set; } // Street Address

        [Column("City_Town")]
        public string City { get; set; } // City Town

        [Column("Zip_Postal_Code")]
        public string PostalCode { get; set; } // Zip Postal Code

        [Column("Time_Stamp")]
        [NotMapped]
        public byte[] TimeStamp { get; set; } // Time_Stamp (length: 8)

        public virtual SecurityLoginPoco SecurityLogin { get; set; }

        public virtual SystemCountryCodePoco SystemCountryCode { get; set; }

        public virtual ICollection<ApplicantEducationPoco> ApplicantEducations { get; set; }

        public virtual ICollection<ApplicantWorkHistoryPoco> ApplicantWorkHistorys { get; set; }

        public virtual ICollection<ApplicantResumePoco> ApplicantResumes { get; set; }

        public virtual ICollection<ApplicantSkillPoco> ApplicantSkills { get; set; }

        public virtual ICollection<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
    }
}
