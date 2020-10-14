using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace DorllyService.Domain
{
    public class DorllyServiceManagerContext : DbContext,IContext
    {
        public DorllyServiceManagerContext(DbContextOptions<DorllyServiceManagerContext> options)
            : base(options)
        {
        }

        public static readonly LoggerFactory LogFactory = new LoggerFactory(new[] {
            new DebugLoggerProvider()
        });

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
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<CategoryProperties> CategoryProperties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User").HasIndex(u=>u.Account).IsUnique();
            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<Role>().ToTable("Role").HasIndex(r=>r.Code).IsUnique();
            modelBuilder.Entity<SubSystem>().ToTable("SubSystem");
            modelBuilder.Entity<Appraise>().ToTable("Appraise");
            modelBuilder.Entity<Contract>().ToTable("Contract");
            modelBuilder.Entity<Garden>().ToTable("Garden").HasIndex(g=>g.Name).IsUnique();

            modelBuilder.Entity<Module>().ToTable("Module");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Permission>().ToTable("Permission").HasIndex(p=>p.Code).IsUnique();
            modelBuilder.Entity<PropertyValue>().ToTable("PropertyValue").HasIndex(p=>p.Code).IsUnique();

            modelBuilder.Entity<ServiceCategory>().ToTable("ServiceCategory").HasIndex(sc=>sc.Code).IsUnique();
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
