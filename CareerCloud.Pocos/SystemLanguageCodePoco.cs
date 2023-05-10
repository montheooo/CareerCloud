﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("System_Language_Codes")]
    public class SystemLanguageCodePoco
    {
        [Key]
        [Column("LanguageID")]
        public string LanguageID { get; set; } // LanguageID (Primary key) (length: 10)

        [Column("Name")]
        public string Name { get; set; } // Name (length: 50)

        [Column("Native_Name")]
        public string NativeName { get; set; } // Native_Name (length: 50)

        public virtual ICollection<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
    }

}
