using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DorllyService.Domain.Data
{
    public class DorllyServiceManagerContextFactory : IDesignTimeDbContextFactory<DorllyServiceManagerContext>
    {
        public DorllyServiceManagerContext CreateDbContext(string[] args)
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json")
                .Build();
            string connectionString = configuration.GetConnectionString("SqlServiceConnection");

            var optionsBuilder = new DbContextOptionsBuilder<DorllyServiceManagerContext>();
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("DorllyServiceManager"));

            return new DorllyServiceManagerContext(optionsBuilder.Options);
        }
    }
}
