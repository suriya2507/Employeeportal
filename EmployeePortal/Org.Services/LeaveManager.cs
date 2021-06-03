using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.Common;
using Org.Common.DataProvider;
using Org.Common.Domain;
using Org.Common.Manager;
using Leave = Org.Common.Model.Leave;

namespace Org.Services
{
    internal class LeaveManager : ILeaveManager
    {
        private readonly ILeaveDataProvider _leaveDataProvider;
        private readonly UserManager<EmployeePortalUser> _userManager;

        public LeaveManager(ILeaveDataProvider leaveDataProvider, UserManager<EmployeePortalUser> userManager)
        {
            _leaveDataProvider = leaveDataProvider;
            _userManager = userManager;
        }

        public async Task<Leave> Create(Leave leave, string userId)
        {
            return await _leaveDataProvider.Create(leave);
        }

        public async Task<Leave> Get(int leaveId)
        {
            return await _leaveDataProvider.Get(leaveId);
        }

        public async Task Approve(int leaveId)
        {
            var leave = await Get(leaveId);
            if (leave == null)
            {
                throw new ArgumentNullException(nameof(leave));
            }
            
            if (leave.Status == LeaveStatus.Rejected)
            {
                throw new InvalidOperationException("Leave has been already rejected");
            }
            
            await _leaveDataProvider.UpdateLeaveStatus(leaveId, LeaveStatus.Approved);
        }

        public async Task Delete(int leaveId, string userId)
        {
            var leave = await Get(leaveId);
            if (leave == null)
            {
                throw new ArgumentNullException(nameof(leave));
            }
            
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (leave.LeaveUserId != userId && !await _userManager.IsInRoleAsync(user, Constants.Roles.ADMINISTRATOR))
            {
                throw new UnauthorizedAccessException();
            }
            
            await _leaveDataProvider.Delete(leaveId);
        }

        public async Task Update(Leave leave, string userId)
        {
            var databaseLeave = await Get(leave.Id);
            if (databaseLeave == null)
            {
                throw new ArgumentNullException(nameof(databaseLeave));
            }
            
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (databaseLeave.LeaveUserId != userId && !await _userManager.IsInRoleAsync(user, Constants.Roles.ADMINISTRATOR))
            {
                throw new UnauthorizedAccessException();
            }

            await _leaveDataProvider.Update(leave);
            
        }

        public async Task<List<Leave>> GetLeaves(string userId)
        {
            return await _leaveDataProvider.GetForUser(userId);
        }
    }
}