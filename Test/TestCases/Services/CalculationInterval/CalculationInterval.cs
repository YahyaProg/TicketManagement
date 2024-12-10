using Application.Services.CalculationIntervalService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.CalculationInterval
{
    public class CalculationInterval
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddCalculationIntervalRequest_Success() =>
            Assert.NotNull(new AddCalculationIntervalRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteCalculationIntervalRequest_Success() =>
            Assert.NotNull(new DeleteCalculationIntervalRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownCalculationIntervalRequest_Success() =>
     Assert.NotNull(new DropDownCalculationIntervalRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetCalculationIntervalRequest_Success() =>
            Assert.NotNull(new GetCalculationIntervalRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateCalculationIntervalRequest_Success() =>
            Assert.NotNull(new UpdateCalculationIntervalRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchCalculationIntervalRequest_Success() =>
            Assert.NotNull(new SearchCalculationIntervalRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownCalculationIntervalRequest()
        {
            var request = new DropDownCalculationIntervalRequest()
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = 123
            };

            Assert.NotNull(request);

        }
    }
}
