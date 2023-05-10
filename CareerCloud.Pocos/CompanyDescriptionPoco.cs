using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Company_Descriptions")] // Annotation Table Mapping to Database
    public class CompanyDescriptionPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; } // Id (Primary key)

        [Column("Company")]
        public Guid Company { get; set; } // Company

        [Column("LanguageId")]
        public String LanguageId { get; set; } // LanguageId

        [Column("Company_Name")]
        public String CompanyName { get; set; } // Company Name

        [Column("Company_Description")]
        public String CompanyDescription { get; set; } // Company Description

        [Column("Time_Stamp")]
        [NotMapped]
        public byte[] TimeStamp { get; set; } // Time_Stamp (length: 8)

        public virtual CompanyProfilePoco CompanyProfile { get; set; }

        public virtual SystemLanguageCodePoco SystemLanguageCode { get; set; }

        
    }
}
