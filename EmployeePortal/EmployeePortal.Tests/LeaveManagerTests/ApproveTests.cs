using System;
using System.Threading.Tasks;
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
using Org.Services;
using Leave = Org.Common.Model.Leave;

namespace EmployeePortal.Tests.LeaveManagerTests
{
    public class ApproveTests : IdentityTestBase
    {
        /// <summary>
        /// This is a test for negative branch of the code, when the code has to throw exception due to incorrect input data
        /// Also System.Task will wrap the exception in System.AggregatedException, so we have to expect it instead of System.InvalidOperationException
        /// </summary>
        [Test]
        public void ApproveRejected__ThrowsException()
        {

            var leaveManagerDataProviderMock = new Mock<ILeaveDataProvider>();
            leaveManagerDataProviderMock.Setup(lm => lm.Get(1))
                .ReturnsAsync(new Leave
                {
                    Id = 1,
                    Status = LeaveStatus.Rejected
                });
            
            var leaveManager = new LeaveManager(leaveManagerDataProviderMock.Object, UserManager);

            Assert.Throws<AggregateException>(() => leaveManager.Approve(1).Wait());
        }

        [Test]
        public void ApprovePending__Success()
        {
            var message = Guid.NewGuid().ToString();
            
            var leaveManagerDataProviderMock = new Mock<ILeaveDataProvider>();
            leaveManagerDataProviderMock.Setup(lm => lm.Get(1))
                .ReturnsAsync(new Leave
                {
                    Id = 1,
                    Status = LeaveStatus.Submitted
                });

            leaveManagerDataProviderMock.Setup(lm => lm.UpdateLeaveStatus(1, LeaveStatus.Approved))
                .Throws(new Exception(message));

            var leaveManager = new LeaveManager(leaveManagerDataProviderMock.Object, UserManager);

            Action runningDelegate = () => leaveManager.Approve(1).Wait();
            
            Assert.Throws<AggregateException>(runningDelegate.Invoke, message);
        }
    }
}