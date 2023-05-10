using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Security_Logins_Log")]
    public class SecurityLoginsLogPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; } // Id (Primary key)

        [Column("Login")]
        public Guid Login { get; set; } // Login

        [Column("Source_IP")]
        public string SourceIP { get; set; } // Source_IP (length: 15)

        [Column("Logon_Date")]
        public DateTime LogonDate { get; set; } // Logon_Date

        [Column("Is_Succesful")]
        public bool IsSuccesful { get; set; } // Is_Succesful

        public virtual SecurityLoginPoco SecurityLogin { get; set; }

    }
}
