using System.Collections.Generic;
using System.Threading.Tasks;
using Org.Common.Model;

namespace Org.Common.Manager
{
    public interface ILeaveManager
    {
        Task<Leave> Create(Leave leave, string userId);
        Task Approve(int leaveId);
        Task Delete(int leaveId, string userId);
        Task Update(Leave leave, string userId);
        Task<List<Leave>> GetLeaves(string userId);
    }
}