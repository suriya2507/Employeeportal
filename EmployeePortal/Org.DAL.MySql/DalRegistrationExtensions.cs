using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Org.Common.DataProvider;
using Org.Common.Model;
using Org.DAL.MySql.DataProvider;

[assembly:InternalsVisibleTo("EmployeePortal.Tests")]
namespace Org.DAL.MySql
{
    public static class DalRegistrationExtensions
    {
        private const string CONNECTION_STRING_NAME = "EmpoyeePortalConnection";
        public static void RegisterDal(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(CONNECTION_STRING_NAME);

            services.AddDbContextPool<EmployeContext>(o =>
                {
                    o.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
                });
            
            services.AddScoped<IDatabaseMigrationProvider, DatabaseMigrationProvider>();
            services.AddScoped<ILeaveDataProvider, LeaveDataProvider>();

            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<Leave, Common.Domain.Leave>();
                cfg.CreateMap<Common.Domain.Leave, Leave>();
            });
        }

        public static void AddIdentityEfStores(this IdentityBuilder identityBuilder)
        {
            identityBuilder.AddEntityFrameworkStores<EmployeContext>();
        }
    }
}