
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Options;

namespace ModernSchool.DataAcces
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = @"server=youServerName,5433;Database=YourDatabaseName;User Id=sa; Password=yourPassword;";
            optionsBuilder.UseSqlServer(connectionString);
            Console.WriteLine(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }

    };

    
        
    
}