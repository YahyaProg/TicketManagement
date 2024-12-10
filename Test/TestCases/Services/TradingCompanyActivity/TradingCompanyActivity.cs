using Application.Services.TradingCompanyActivityService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.TradingCompanyActivity
{
    public class TradingCompanyActivityRequestTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddTradingCompanyActivityRequest_Success() =>
            Assert.NotNull(new AddTradingCompanyActivityRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteTradingCompanyActivityRequest_Success() =>
            Assert.NotNull(new DeleteTradingCompanyActivityRequestHandler(_unitOfWork.Object));
        [Fact]
        public void GetTradingCompanyActivityRequest_Success() =>
            Assert.NotNull(new GetTradingCompanyActivityRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateTradingCompanyActivityRequest_Success() =>
            Assert.NotNull(new UpdateTradingCompanyActivityRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchTradingCompanyActivityRequest_Success() =>
            Assert.NotNull(new SearchTradingCompanyActivityRequestHandler(_unitOfWork.Object));
    }
}
