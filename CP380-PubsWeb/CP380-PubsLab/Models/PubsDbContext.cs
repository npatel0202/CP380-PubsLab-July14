using System;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CP380_PubsLab.Models
{
    public class PubsDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbpath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\CP380-PubsLab\\pubs.mdf"));
            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Integrated Security=true;AttachDbFilename={dbpath}");
        }

        // TODO: Add DbSets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sales>().HasKey(c => new { c.S_ID, c.T_ID });
            modelBuilder
                .Entity<Stores>()
                .HasMany(c => c.Titles)
                .WithMany(c => c.Stores)
                .UsingEntity<Sales>(
                k => k
                    .HasOne(st => st.Titles)
                    .WithMany(t => t.Sales)
                    .HasForeignKey(st => st.T_ID),
                k => k
                    .HasOne(st => st.Stores)
                    .WithMany(s => s.Sales)
                    .HasForeignKey(st => st.S_ID)
            );
        }
            // TODO
       public DbSet <Employee> Employee { get; set; }
       public DbSet <Jobs> Jobs { get; set; }

       public DbSet <Stores> Stores { get; set; }
       public DbSet<Titles> Titles { get; set; }
       public DbSet<Sales> Sales { get; set; }

    }


    public class Titles
    {
        // TODO
        [Key]
        [Column("title_id")]
        public string T_ID { get; set; }
        [Column("title")]
        public string Tit { get; set; }

        public ICollection<Stores> Stores { get; set; }
        public List<Sales> Sales { get; set; }
    }


    public class Stores
    {
        // TODO

        [Key]
        [Column("stor_id")]
        public string S_ID { get; set; }
        [Column("stor_name")]
        public string S_Name { get; set; }

        public ICollection<Titles> Titles { get; set; }
        public List<Sales> Sales { get; set; }
    }


    public class Sales
    {
        // TODO

        [Column("stor_id")]
        public string S_ID { get; set; }
        public Stores Stores { get; set; }
        [Column("title_id")]
        public string T_ID { get; set; }
       /* [Column("title")]
        public string Tit { get; set; }*/
        public Titles Titles { get; set; }
    }

    public class Employee
    {
        // TODO
        [Key]
        [Column("emp_id")]
         public string E_ID { get; set; }

        [Column("fname")]
        public string F_Name { get; set; }

        [Column("lname")]
        public string L_Name { get; set; }

        [Column("job_id")]

        public Int16 J_ID { get; set; }

        [ForeignKey("J_ID")]

        public Jobs Jobs { get; set; }

        

    }

    public class Jobs
    {
        // TODO
        [Key]
        [Column("job_id")]
        public Int16 J_ID { get; set; }

        [Column("job_desc")]
        public string Desc { get; set; }

      //  [Column("min_lvl")]
        //public string MLevel { get; set; }

        //[Column("max_lvl")]
        //public string MX_Level { get; set; }

        public List<Employee> Employee { get; set; }
    }
}
