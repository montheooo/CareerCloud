using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    // Company_Profiles
    [Table("Company_Profiles")]
    public class CompanyProfilePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; } // Id (Primary key)

        [Column("Registration_Date")]
        public DateTime RegistrationDate { get; set; } // Registration_Date

        [Column("Company_Website")]
        public string CompanyWebsite { get; set; } // Company_Website (length: 100)

        [Column("Contact_Phone")]
        public string ContactPhone { get; set; } // Contact_Phone (length: 20)

        [Column("Contact_Name")]
        public string ContactName { get; set; } // Contact_Name (length: 50)

        [Column("Company_Logo")]
        public byte[] CompanyLogo { get; set; } // Company_Logo

        [Column("Time_Stamp")]
        [NotMapped]
        public byte[] TimeStamp { get; set; } // Time_Stamp (length: 8)

        public virtual ICollection<CompanyJobPoco> CompanyJobs { get; set; }

        public virtual ICollection<CompanyLocationPoco> CompanyLocations { get; set; }

        public virtual ICollection<CompanyDescriptionPoco> CompanyDescriptions { get; set; }




    }

}
