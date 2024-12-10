using Application.Services.CreditPeriodService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.CreditPeriod
{
    public class CreditPeriodRequestTes
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddCreditPeriodRequest_Success() =>
            Assert.NotNull(new AddCreditPeriodRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteCreditPeriodRequest_Success() =>
            Assert.NotNull(new DeleteCreditPeriodRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownCreditPeriodRequest_Success() =>
    Assert.NotNull(new DropDownCreditPeriodRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetCreditPeriodRequest_Success() =>
            Assert.NotNull(new GetCreditPeriodRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateCreditPeriodRequest_Success() =>
            Assert.NotNull(new UpdateCreditPeriodRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchCreditPeriodRequest_Success() =>
            Assert.NotNull(new SearchCreditPeriodRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownCreditPeriodRequest()
        {
            var request = new DropDownCreditPeriodRequest()
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = 123
            };

            Assert.NotNull(request);

        }
    }
}
