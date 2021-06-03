using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmployeePortal.Extensions;
using EmployeePortal.Model.Registration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.Common.Manager;
using Org.Common.Model;
using Org.DAL.MySql;
using Org.DAL.MySql.Entities;
using Org.Portal.Model.registration;

namespace EmployeePortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        
        private readonly ILogger<UserController> _logger;
        private readonly IUserManager _userManager;
        
        public UserController(ILogger<UserController> logger, IUserManager userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("/api/user")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegistrationRequestModel model)
        {
            var user = await _userManager.Register(new RegistrationModel
            {
                FirstName=model.FirstName,
                LastName=model.LastName,
                DOB=model.DOB,
                Email = model.Email,
                Password = model.Password
            });

            return Ok(new
            {
                Token = user.GenerateToken(String.Join(',', user.Roles)),
                Email = model.Email
            });
        }

        [HttpPost]
        [Route("/api/user/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginRequestModel model)
        {
            var user = await _userManager.Login(model.Login, model.Password);

            return Ok(new
            {
                user.FirstName,
                user.LastName,
                user.DOB,
                user.Email,
                Token = user.GenerateToken("")
            });
        }

        [HttpGet]
        [Route("/api/user")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> GetUserInfo()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Post(Registration reg)
        {
            string fName = reg.FirstName;

            //RegResponse response = new RegResponse();
            //response.Status = true;
            //response.Message  ="Successfully submitted to application layer."

            //return Ok(response);

            return Ok(new RegResponse() { Status = false, Message = "Successfully submitted to application layer." });
        }
    }
}
