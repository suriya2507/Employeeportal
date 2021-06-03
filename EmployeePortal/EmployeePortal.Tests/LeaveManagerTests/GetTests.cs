using Moq;
using NUnit.Framework;
using Org.Common.DataProvider;
using Org.Common.Domain;
using Org.Services;
using Leave = Org.Common.Model.Leave;

namespace EmployeePortal.Tests.LeaveManagerTests
{
    public class GetTests : IdentityTestBase
    {
        [Test]
        public void Get__Success()
        {
            //SETUP
            var leaveManagerDataProviderMock = new Mock<ILeaveDataProvider>();
            leaveManagerDataProviderMock.Setup(lm => lm.Get(1))
                .ReturnsAsync(new Leave
                {
                    Id = 1,
                    Status = LeaveStatus.Submitted
                });

            var leaveManager = new LeaveManager(leaveManagerDataProviderMock.Object, UserManager);
            
            //TEST
            var result = leaveManager.Get(1).Result;

            //ASSERT
            Assert.NotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual(LeaveStatus.Submitted, result.Status);
        }
    }
}