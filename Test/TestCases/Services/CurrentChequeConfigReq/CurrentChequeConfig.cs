using Application.Services.CurrentChequeConfigService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.CurrentChequeConfigReq
{
    public class CurrentChequeConfigRequestTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddCurrentChequeConfigRequest_Success() =>
            Assert.NotNull(new AddCurrentChequeConfigRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteCurrentChequeConfigRequest_Success() =>
            Assert.NotNull(new DeleteCurrentChequeConfigRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetCurrentChequeConfigRequest_Success() =>
            Assert.NotNull(new GetCurrentChequeConfigRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateCurrentChequeConfigRequest_Success() =>
            Assert.NotNull(new UpdateCurrentChequeConfigRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchCurrentChequeConfigRequest_Success() =>
            Assert.NotNull(new SearchCurrentChequeConfigRequestHandler(_unitOfWork.Object));
    }
}
