using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {

        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistories { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=POWERNEWLIFE\\SQL2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

           

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(appjobapppoco => appjobapppoco.ApplicantProfile)
                .WithMany(appprofpoco => appprofpoco.ApplicantJobApplications)
                .HasForeignKey(appjobapppoco => appjobapppoco.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(appjobapppoco => appjobapppoco.CompanyJob)
                .WithMany(compjobpoco => compjobpoco.ApplicantJobApplications)
                .HasForeignKey(appjobapppoco => appjobapppoco.Job);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(appprofpoco => appprofpoco.SecurityLogin)
                .WithMany(seclogin => seclogin.ApplicantProfiles)
                .HasForeignKey(appprofpoco => appprofpoco.Login);

            modelBuilder.Entity<ApplicantEducationPoco>()
               .HasOne(appedupoco => appedupoco.ApplicantProfile)
               .WithMany(appprofilepoco => appprofilepoco.ApplicantEducations)
               .HasForeignKey(appedupoco => appedupoco.Applicant);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(appprofpoco => appprofpoco.SystemCountryCode)
                .WithMany(syscouncode => syscouncode.ApplicantProfiles)
                .HasForeignKey(appprofpoco => appprofpoco.Country); 

            modelBuilder.Entity<ApplicantResumePoco>()
                .HasOne(apprespoco => apprespoco.ApplicantProfile)
                .WithMany(appprofpoco => appprofpoco.ApplicantResumes)
                .HasForeignKey(apprespoco => apprespoco.Applicant);

            modelBuilder.Entity<ApplicantSkillPoco>()
                .HasOne(appskilpoco => appskilpoco.ApplicantProfile)
                .WithMany(appprof => appprof.ApplicantSkills)
                .HasForeignKey(appskilpoco => appskilpoco.Applicant);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(appworkhispoco => appworkhispoco.ApplicantProfile)
                .WithMany(appprof => appprof.ApplicantWorkHistorys)
                .HasForeignKey(appworkhispoco => appworkhispoco.Applicant);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(syscoucodepoco => syscoucodepoco.SystemCountryCode)
                .WithMany(syscoucodepoco => syscoucodepoco.ApplicantWorkHistories)
                .HasForeignKey(syscoucodepoco => syscoucodepoco.CountryCode);  

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(comdescpoco => comdescpoco.CompanyProfile)
                .WithMany(comprof => comprof.CompanyDescriptions)
                .HasForeignKey(comdescpoco => comdescpoco.Company);

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(comdescpoco => comdescpoco.SystemLanguageCode)
                .WithMany(syslancodepoco => syslancodepoco.CompanyDescriptions)
                .HasForeignKey(comdescpoco => comdescpoco.LanguageId); 

            modelBuilder.Entity<CompanyJobDescriptionPoco>()
                .HasOne(comjobdespoco => comjobdespoco.CompanyJob)
                .WithMany(c => c.CompanyJobDescriptions)
                .HasForeignKey(comjobdescpoco => comjobdescpoco.Job);

            modelBuilder.Entity<CompanyJobEducationPoco>()
                .HasOne(comjobedupoco => comjobedupoco.CompanyJob)
                .WithMany(comjob => comjob.CompanyJobEducations)
                .HasForeignKey(comjobedupoco => comjobedupoco.Job);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasOne(comjobpoco => comjobpoco.CompanyProfile)
                .WithMany(comprof => comprof.CompanyJobs)
                .HasForeignKey(comjobpoco => comjobpoco.Company);

            modelBuilder.Entity<CompanyJobSkillPoco>()
                .HasOne(comjobskilpoco => comjobskilpoco.CompanyJob)
                .WithMany(comjob => comjob.CompanyJobSkills)
                .HasForeignKey(comjobskilpoco => comjobskilpoco.Job);

            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(comlocapoco => comlocapoco.CompanyProfile)
                .WithMany(comprof => comprof.CompanyLocations)
                .HasForeignKey(comlocapoco => comlocapoco.Company);

            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(comlocapoco => comlocapoco.SystemCountryCode)
                .WithMany(syscoucode => syscoucode.CompanyLocations)
                .HasForeignKey(comlocapoco => comlocapoco.CountryCode); 

            modelBuilder.Entity<SecurityLoginsLogPoco>()
                .HasOne(secloglogpoco => secloglogpoco.SecurityLogin)
                .WithMany(seclogin => seclogin.SecurityLoginsLogs)
                .HasForeignKey(secloglogpoco => secloglogpoco.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(seclogrolepoco => seclogrolepoco.SecurityLogin)
                .WithMany(seclog => seclog.SecurityLoginsRoles)
                .HasForeignKey(seclogrolepoco => seclogrolepoco.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(seclogrolpoco => seclogrolpoco.SecurityRole)
                .WithMany(secrole => secrole.SecurityLoginsRoles)
                .HasForeignKey(seclogrolpoco => seclogrolpoco.Role);
        }
    
    }
}
