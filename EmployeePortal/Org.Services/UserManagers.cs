using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Org.Common;
using Org.Common.DataProvider;
using Org.Common.Domain;
using Org.Common.Exception;
using Org.Common.Manager;
using Org.Common.Model;
using Org.Common.Options;

namespace Org.Services
{
    internal class UserManager : IUserManager
    {
        private readonly UserManager<EmployeePortalUser> _userManager;
        private readonly IDatabaseMigrationProvider _migrationProvider;
        private readonly SignInManager<EmployeePortalUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthorizationOptions _authorizationOptions;

        public UserManager(UserManager<EmployeePortalUser> userManager, IDatabaseMigrationProvider migrationProvider, SignInManager<EmployeePortalUser> signInManager,
            RoleManager<IdentityRole> roleManager, IOptions<AuthorizationOptions> options)
        {
            _userManager = userManager;
            _migrationProvider = migrationProvider;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _authorizationOptions = options.Value;
        }

        public async Task<User> Register(RegistrationModel model)
        {
            await _migrationProvider.MigrateDb();

            EmployeePortalUser user = null;
            
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await _userManager.CreateAsync(new EmployeePortalUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DOB=model.DOB,
                }, model.Password);

                if (!result.Succeeded)
                {
                    throw new BadRegistrationRequestException(result.Errors);
                }

                user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == model.Email);

                if (model.AdminKey == _authorizationOptions.AdminCreationKey)
                {
                    if (!await _roleManager.Roles.AnyAsync(r => r.Name == Constants.Roles.ADMINISTRATOR))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Constants.Roles.ADMINISTRATOR));
                    }

                    //This is adding user to role, i e making him an admin
                    await _userManager.AddToRoleAsync(user, Constants.Roles.ADMINISTRATOR);
                }

                scope.Complete();
            }

            var roles = await _userManager.GetRolesAsync(user);
            return new User
            {
                Roles = roles.ToList(),
                Email = user.Email,
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DOB=user.DOB,
            };
        }

        public async Task<User> Login(string login, string password)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == login);
            if (user == null)
            {
                throw new NotAuthorizedException("There is no user with provided login and password");
            }
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);
            if (!result.Succeeded)
            {
                throw new NotAuthorizedException("There is no user with provided login and password");
            }

            var roles = await _userManager.GetRolesAsync(user);
            return new User
            {
                Email = user.Email,
                Id = user.Id,
                Roles = roles.ToList(),
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}