using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Security_Logins_Roles")]
    public class SecurityLoginsRolePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; } // Id (Primary key)

        [Column("Login")]
        public Guid Login { get; set; } // Login

        [Column("Role")]
        public Guid Role { get; set; } // Role

        [Column("Time_Stamp")]
        [NotMapped]
        public byte[] TimeStamp { get; set; } // Time_Stamp (length: 8)

        public virtual SecurityLoginPoco SecurityLogin { get; set; }

        public virtual SecurityRolePoco SecurityRole { get; set; }

    }
}
