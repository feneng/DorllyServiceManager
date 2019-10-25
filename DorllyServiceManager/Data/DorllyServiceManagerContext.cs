using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DorllyServiceManager.Models
{
    public class DorllyServiceManagerContext : DbContext
    {
        public DorllyServiceManagerContext (DbContextOptions<DorllyServiceManagerContext> options)
            : base(options)
        {
        }

        public DbSet<DorllyServiceManager.Models.User> User { get; set; }
        public DbSet<DorllyServiceManager.Models.Service> Service { get; set; }
        public DbSet<DorllyServiceManager.Models.Role> Role { get; set; }
        public DbSet<DorllyServiceManager.Models.SubSystem> SubSystem { get; set; }
        public DbSet<DorllyServiceManager.Models.Appraise> Appraise { get; set; }
        public DbSet<DorllyServiceManager.Models.Contract> Contract { get; set; }
        public DbSet<DorllyServiceManager.Models.Garden> Garden { get; set; }
        public DbSet<DorllyServiceManager.Models.Module> Module { get; set; }
        public DbSet<DorllyServiceManager.Models.Order> Order { get; set; }
        public DbSet<DorllyServiceManager.Models.Permission> Permission { get; set; }
        public DbSet<DorllyServiceManager.Models.PropertyValue> PropertyValue { get; set; }
        public DbSet<DorllyServiceManager.Models.ServiceCategory> ServiceCategory { get; set; }
        public DbSet<DorllyServiceManager.Models.ServiceProperty> ServiceProperty { get; set; }
        public DbSet<DorllyServiceManager.Models.ServiceSupplier> ServiceSupplier { get; set; }
        public DbSet<DorllyServiceManager.Models.SupplierLevel> SupplierLevel { get; set; }
        public DbSet<DorllyServiceManager.Models.SystemSetting> SystemSetting { get; set; }

        public DbSet<DorllyServiceManager.Models.RolePermission> RolePermission { get; set; }
        public DbSet<DorllyServiceManager.Models.UserRole> UserRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<SubSystem>().ToTable("SubSystem");
            modelBuilder.Entity<Appraise>().ToTable("Appraise");
            modelBuilder.Entity<Contract>().ToTable("Contract");
            modelBuilder.Entity<Garden>().ToTable("Garden");

            modelBuilder.Entity<Module>().ToTable("Module");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Permission>().ToTable("Permission");
            modelBuilder.Entity<PropertyValue>().ToTable("PropertyValue");

            modelBuilder.Entity<ServiceCategory>().ToTable("ServiceCategory");
            modelBuilder.Entity<ServiceProperty>().ToTable("ServiceProperty");
            modelBuilder.Entity<ServiceSupplier>().ToTable("ServiceSupplier");
            modelBuilder.Entity<SupplierLevel>().ToTable("SupplierLevel");

            modelBuilder.Entity<SystemSetting>().ToTable("SystemSetting");
            modelBuilder.Entity<RolePermission>().ToTable("RolePermission")
                .HasKey(c => new { c.RoleId, c.PermissionId });
            modelBuilder.Entity<UserRole>().ToTable("UserRole")
                .HasKey(c=>new { c.RoleId,c.UserId});
            modelBuilder.Entity<CategoryProperties>().ToTable("CategoryProperties")
                .HasKey(c => new { c.CategoryId, c.PropertyId });
        }
    }
}
