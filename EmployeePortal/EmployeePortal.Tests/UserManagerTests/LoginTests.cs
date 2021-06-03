using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Moq;
using NUnit.Framework;
using Org.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Org.Common.DataProvider;
using Org.Common.Domain;
using Org.Common.Exception;
using Org.Common.Manager;
using Org.Common.Options;
using Org.DAL.MySql;
using Org.DAL.MySql.DataProvider;

namespace EmployeePortal.Tests.UserManagerTests
{
    public class LoginTests : IdentityTestBase
    {
        [Test]
        public void UserIsNull__Exception()
        {
            var databaseMigrationProviderMock = Mock.Of<IDatabaseMigrationProvider>();

            var userManager = new UserManager(UserManager, databaseMigrationProviderMock, SignInManager, RoleManager,
                Mock.Of<IOptions<AuthorizationOptions>>());
            
            Assert.ThrowsAsync(typeof(NotAuthorizedException), async () => await userManager.Login("test", "test"));
        }
    }
}