using Application.Services.OtheracctchqcfgService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.Otheracctchqcfg
{
    public class OtheracctchqcfgRequestTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddOtheracctchqcfgRequest_Success() =>
            Assert.NotNull(new AddOtheracctchqcfgRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteOtheracctchqcfgRequest_Success() =>
            Assert.NotNull(new DeleteOtheracctchqcfgRequestHandler(_unitOfWork.Object));
        [Fact]
        public void GetOtheracctchqcfgRequest_Success() =>
            Assert.NotNull(new GetOtheracctchqcfgRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateOtheracctchqcfgRequest_Success() =>
            Assert.NotNull(new UpdateOtheracctchqcfgRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchOtheracctchqcfgRequest_Success() =>
            Assert.NotNull(new SearchOtheracctchqcfgRequestHandler(_unitOfWork.Object));
    }
}
