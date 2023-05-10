using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    // Company_Jobs

    [Table("Company_Jobs")]
    public class CompanyJobPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; } // Id (Primary key)

        [Column("Company")]
        public Guid Company { get; set; } // Company

        [Column("Profile_Created")]
        public DateTime ProfileCreated { get; set; } // Profile_Created

        [Column("Is_Inactive")]
        public bool IsInactive { get; set; } // Is_Inactive

        [Column("Is_Company_Hidden")]
        public bool IsCompanyHidden { get; set; } // Is_Company_Hidden

        [Column("Time_Stamp")]
        [NotMapped]
        public byte[] TimeStamp { get; set; } // Time_Stamp (length: 8)


        public virtual ICollection<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }

        public virtual ICollection<CompanyJobSkillPoco> CompanyJobSkills { get; set; }

        public virtual CompanyProfilePoco CompanyProfile { get; set; }

        public virtual ICollection<CompanyJobEducationPoco> CompanyJobEducations { get; set; }

        public virtual ICollection<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }


    }
}
