using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Org.DAL.MySql
{
    internal class EmployeeDesignContextFactory : IDesignTimeDbContextFactory<EmployeContext>
    {
        public EmployeContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EmployeContext>();
            var connectionString = "server=localhost;database=EmployePortal-Migration;allowuservariables=True;user id=root;password=Suriya@1998";

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new EmployeContext(optionsBuilder.Options);
        }
    }
}