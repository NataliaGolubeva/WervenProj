using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WervenProj.Models;

namespace WervenProj.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }
        public DbSet<ConstractionSite> ConstractionSites { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ConstractionStatus> ConstractionStatuses { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles {  get; set; }
        public DbSet<ConstractionSiteEnrollments> ConstractionSiteEnrollments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<ConstractionStatus>().HasData(

               new ConstractionStatus()
               {
                   Id = 1,
                   StatusNr = 0,
                   StatusName = "Aangemakt"
               },
               new ConstractionStatus()
               {
                   Id = 2,
                   StatusNr = 1,
                   StatusName = "Goedgekeurd"
               },
               new ConstractionStatus()
               {
                   Id = 3,
                   StatusNr = 2,
                   StatusName = "In Werking"
               },
               new ConstractionStatus()
               {
                   Id = 4,
                   StatusNr = 3,
                   StatusName = "Afgerond"
               }
               );
            modelBuilder.Entity<EmployeeRole>().HasData(

              new EmployeeRole()
              {
                  Id = 1,
                  RoleNr = 0,
                  RoleName = "Metselaar"
              },
              new EmployeeRole()
              {
                  Id = 2,
                  RoleNr = 1,
                  RoleName = "Schrijnwerker"
              },
              new EmployeeRole()
              {
                  Id = 3,
                  RoleNr = 2,
                  RoleName = "Administratie"
              },
              new EmployeeRole()
              {
                  Id = 4,
                  RoleNr = 3,
                  RoleName = "Manager"
              }
              );
            modelBuilder.Entity<Employee>(
         entity =>
         {
             entity.HasOne(e => e.Role)
                 .WithMany(r => r.Employees)
                 .HasForeignKey("RoleId")
                 .OnDelete(DeleteBehavior.Cascade);
         });
            modelBuilder.Entity<ConstractionSite>(
        entity =>
        {
            entity.HasOne(s => s.Status)
                .WithMany(x => x.ConstractionSites)
                .HasForeignKey("StatusId")
                .OnDelete(DeleteBehavior.Cascade);
        });
            //modelBuilder.Entity<Employee>().HasData(

            //   new Employee()
            //   {
            //       Id = 1,
            //       Name = "Roy",
            //       RoleId = 1,
            //   },
            //   new Employee()
            //   {
            //       Id = 2,
            //       Name = "Nina",
            //       RoleId = 2,

            //   },
            //   new Employee()
            //   {
            //       Id = 3,
            //       Name = "Jeff",
            //       RoleId = 3,
            //   },
            //   new Employee()
            //   {
            //       Id = 4,
            //       Name = "Lenne",
            //       RoleId = 4,
            //   }
            //   );
        }

    }
}
