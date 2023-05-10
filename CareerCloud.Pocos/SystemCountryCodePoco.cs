using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("System_Country_Codes")]
    public class SystemCountryCodePoco 
    {

        [Key]

        [Column("Code")]
        public String Code { get; set; } // Code (Primary key) (length: 10)

        [Column("Name")]
        public string Name { get; set; } // Name (length: 50)

        public virtual ICollection<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public virtual ICollection<ApplicantWorkHistoryPoco> ApplicantWorkHistories { get; set; }
        public virtual ICollection<CompanyLocationPoco> CompanyLocations { get; set; }
    }
}
