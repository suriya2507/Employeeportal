using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Org.Common.DataProvider;
using Org.Common.Domain;
using Leave = Org.Common.Model.Leave;

namespace Org.DAL.MySql.DataProvider
{
    internal class LeaveDataProvider : ILeaveDataProvider
    {
        private readonly EmployeContext _context;
        private readonly IMapper _mapper;
        public LeaveDataProvider(EmployeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Leave> Create(Leave leave)
        {
            var entity = new Common.Domain.Leave
            {
                Id = leave.Id,
                From = leave.From,
                To = leave.To,
                Status = LeaveStatus.Submitted,
                LeaveUserId = leave.LeaveUserId,
                Message=leave.message

               

            };

            var result = await _context.Leaves.AddAsync(entity);
            leave.Id = result.Entity.Id;
            await _context.SaveChangesAsync();
            return leave;
        }

        public async Task<Leave> Get(int leaveId)
        {
            var leave = await _context.Leaves.AsNoTracking().FirstOrDefaultAsync(l => l.Id == leaveId);
            return _mapper.Map<Leave>(leave);
        }

        public async Task<List<Leave>> GetForUser(string userId)
        {
            var entries = await _context.Leaves
                .AsNoTracking()
                .Where(l => l.LeaveUserId == userId)
                .ToListAsync();

            return _mapper.Map<List<Leave>>(entries);
        }

        public async Task UpdateLeaveStatus(int leaveId, LeaveStatus status)
        {
            var entry = await _context.Leaves.FirstOrDefaultAsync(l => l.Id == leaveId);
            entry.Status = status;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int leaveId)
        {
            var entry = await _context.Leaves.FirstAsync(l => l.Id == leaveId);
            _context.Leaves.Remove(entry);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Leave leave)
        {
            var entry = await _context.Leaves.FirstAsync(l => l.Id == leave.Id);
            _mapper.Map(leave, entry);
            await _context.SaveChangesAsync();
        }

       
    }
}