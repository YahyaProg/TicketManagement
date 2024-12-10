using Application.Services.ApproverService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.ApproverServiceTest
{
    public class Approver
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void SoftDeleteApproverRequest_Success() =>
            Assert.NotNull(new DeleteApproverRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownApproverRequest_Success() =>
       Assert.NotNull(new DropDownApproverRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetApproverRequest_Success() =>
            Assert.NotNull(new GetApproverRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchApproverRequest_Success() =>
            Assert.NotNull(new SearchApproverRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownApproverRequest()
        {
            var request = new DropDownApproverRequest()
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = 123
            };

            Assert.NotNull(request);

        }
    }
}
