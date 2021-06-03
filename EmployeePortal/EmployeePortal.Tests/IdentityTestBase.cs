using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Org.Common.DataProvider;
using Org.Common.Domain;
using Org.Common.Manager;
using Org.DAL.MySql;

namespace EmployeePortal.Tests
{
    public abstract class IdentityTestBase
    {
        protected static readonly UserManager<EmployeePortalUser> UserManager;
        protected static readonly SignInManager<EmployeePortalUser> SignInManager;
        protected static readonly RoleManager<IdentityRole> RoleManager;
        
        static IdentityTestBase()
        {
            var testServer = new TestServer(new WebHostBuilder().UseStartup<ApiService>()
                .ConfigureServices(s =>
                {
                    s.AddIdentityCore<EmployeePortalUser>()
                        .AddSignInManager()
                        .AddEntityFrameworkStores<EmployeContext>();
                    s.AddDbContext<EmployeContext>(o => { o.UseInMemoryDatabase("ec"); });
                }));
            
            UserManager = testServer.Services.GetService<UserManager<EmployeePortalUser>>();
            SignInManager = testServer.Services.GetService<SignInManager<EmployeePortalUser>>();
            RoleManager = testServer.Services.GetService<RoleManager<IdentityRole>>();
        }
    }
}