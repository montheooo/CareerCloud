using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Security_Logins")]
    public class SecurityLoginPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; } // Id (Primary key)

        [Column("Login")]
        public string Login { get; set; } // Login (length: 50)

        [Column("Password")]
        public string Password { get; set; } // Password (length: 100)

        [Column("Created_Date")]
        public DateTime Created { get; set; } // Created_Date

        [Column("Password_Update_Date")]
        public DateTime? PasswordUpdate { get; set; } // Password_Update_Date

        [Column("Agreement_Accepted_Date")]
        public DateTime? AgreementAccepted { get; set; } // Agreement_Accepted_Date

        [Column("Is_Locked")]
        public bool IsLocked { get; set; } // Is_Locked

        [Column("Is_Inactive")]
        public bool IsInactive { get; set; } // Is_Inactive

        [Column("Email_Address")]
        public string EmailAddress { get; set; } // Email_Address (length: 50)

        [Column("Phone_Number")]
        public string PhoneNumber { get; set; } // Phone_Number (length: 20)

        [Column("Full_Name")]
        public string FullName { get; set; } // Full_Name (length: 100)

        [Column("Force_Change_Password")]
        public bool ForceChangePassword { get; set; } // Force_Change_Password

        [Column("Prefferred_Language")]
        public string PrefferredLanguage { get; set; } // Prefferred_Language (length: 10)

        [Column("Time_Stamp")]
        [NotMapped]
        public byte[] TimeStamp { get; set; } // Time_Stamp (length: 8)

        public virtual ICollection<ApplicantProfilePoco> ApplicantProfiles { get; set; }

        public virtual ICollection<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }

        public virtual ICollection<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }

        


    }
}
