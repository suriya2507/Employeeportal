using System.Collections.Generic;
using System.Threading.Tasks;
using Org.Common.Domain;
using Leave = Org.Common.Model.Leave;

namespace Org.Common.DataProvider
{
    public interface ILeaveDataProvider
    {
        Task<Leave> Create(Leave leave);
        Task<Leave> Get(int leaveId);
        Task<List<Leave>> GetForUser(string userId);
        Task UpdateLeaveStatus(int leaveId, LeaveStatus status);
        Task Delete(int leaveId);
        Task Update(Leave leave);
    }
}