using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Org.Common.Domain;
using Org.Common.Manager;

[assembly:InternalsVisibleTo("EmployeePortal.Tests")]
namespace Org.Services
{
    public static class RegistrationExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ILeaveManager,LeaveManager>();

        }
    }
}