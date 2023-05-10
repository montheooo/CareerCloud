using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class CareerCloudContext : DbContext
    {
        public DbSet<ApplicantEducationPoco>? ApplicantEducationPoco { get; set; }
        // public DbSet<Order> Orders { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string connectString = new ConfigurationBuilder().AddConfiguration.ConnectString();
            optionsBuilder.UseSqlServer("Data Source=POWERNEWLIFE\\SQL2022;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True");
        }
    }
}
