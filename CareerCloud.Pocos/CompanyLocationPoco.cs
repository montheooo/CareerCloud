using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Company_Locations")]
    // Company_Locations
    public class CompanyLocationPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; } // Id (Primary key)

        [Column("Company")]
        public Guid Company { get; set; } // Company

        [Column("Country_Code")]
        public string CountryCode { get; set; } // Country_Code (length: 10)

        [Column("State_Province_Code")]
        public string Province { get; set; } // State_Province_Code (length: 10)

        [Column("Street_Address")]
        public string Street { get; set; } // Street_Address (length: 100)

        [Column("City_Town")]
        public string City { get; set; } // City_Town (length: 100)

        [Column("Zip_Postal_Code")]
        public string PostalCode { get; set; } // Zip_Postal_Code (length: 20)

        [Column("Time_Stamp")]
        [NotMapped]
        public byte[] TimeStamp { get; set; } // Time_Stamp (length: 8)

        public virtual CompanyProfilePoco CompanyProfile { get; set; }
        public virtual SystemCountryCodePoco SystemCountryCode { get; set; }
    }

}
