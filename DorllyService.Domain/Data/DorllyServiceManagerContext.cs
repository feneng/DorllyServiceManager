using Microsoft.EntityFrameworkCore;

namespace DorllyService.Domain
{
    public class DorllyServiceManagerContext : DbContext
    {
        public DorllyServiceManagerContext(DbContextOptions<DorllyServiceManagerContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<SubSystem> SubSystem { get; set; }
        public DbSet<Appraise> Appraise { get; set; }
        public DbSet<Contract> Contract { get; set; }
        public DbSet<Garden> Garden { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<PropertyValue> PropertyValue { get; set; }
        public DbSet<ServiceCategory> ServiceCategory { get; set; }
        public DbSet<ServiceProperty> ServiceProperty { get; set; }
        public DbSet<ServiceSupplier> ServiceSupplier { get; set; }
        public DbSet<SupplierLevel> SupplierLevel { get; set; }
        public DbSet<SystemSetting> SystemSetting { get; set; }

        public DbSet<DorllyService.Domain.RolePermission> RolePermission { get; set; }
        public DbSet<DorllyService.Domain.UserRole> UserRole { get; set; }

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
                .HasKey(c => new { c.RoleId, c.UserId });
            modelBuilder.Entity<CategoryProperties>().ToTable("CategoryProperties")
                .HasKey(c => new { c.CategoryId, c.PropertyId });
        }
    }
}
