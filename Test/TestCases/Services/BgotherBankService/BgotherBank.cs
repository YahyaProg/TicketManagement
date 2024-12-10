using Application.Services.BgotherBankService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.BgotherBgotherBankService
{
    public class BgotherBgotherBankRequestTes
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddBgotherBankRequest_Success() =>
            Assert.NotNull(new AddBgotherBankRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteBgotherBankRequest_Success() =>
            Assert.NotNull(new DeleteBgotherBankRequestHandler(_unitOfWork.Object));
        [Fact]
        public void GetBgotherBankRequest_Success() =>
            Assert.NotNull(new GetBgotherBankRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateBgotherBankRequest_Success() =>
            Assert.NotNull(new UpdateBgotherBankRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchBgotherBankRequest_Success() =>
            Assert.NotNull(new SearchBgotherBankRequestHandler(_unitOfWork.Object));

    }
}
