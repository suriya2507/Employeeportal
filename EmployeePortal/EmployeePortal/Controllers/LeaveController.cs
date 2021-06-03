using System.Threading.Tasks;
using EmployeePortal.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Org.Common;
using Org.Common.Manager;
using Org.Common.Model;

namespace EmployeePortal.Controllers
{
    [ApiController]
    public class LeaveController : Controller
    {
        private readonly ILeaveManager _leaveManager;
        private readonly ILogger<LeaveController> _logger;

        public LeaveController(ILeaveManager leaveManager , ILogger<LeaveController> logger)
        {
            _leaveManager = leaveManager;
            _logger = logger;
        }

        [HttpGet]
        [Route("/leave/user/{id}")]
        [Authorize(Roles = Constants.Roles.ADMINISTRATOR)]
        public async Task<IActionResult> GetAsync(string userId)
        {
            var result = await _leaveManager.GetLeaves(userId);
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        [Route("/leave/approve/{id}")]
        public async Task<IActionResult> Approve(int leaveId)
        {
            await _leaveManager.Approve(leaveId);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("/leave")]
        public async Task<IActionResult> Create(Leave leave)
        {
            var result = await _leaveManager.Create(leave, User.GetUserId());
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        [Route("/leave")]
        public async Task<IActionResult> Update(Leave leave)
        {
          await _leaveManager.Update(leave, User.GetUserId());
            return Ok(leave);
        }

        [HttpDelete]
        [Authorize]
        [Route("/leave/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _leaveManager.Delete(id, User.GetUserId());
            return Ok();
        }
    }
}