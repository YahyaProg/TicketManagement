using Application.Services.FininfoChartconfigconfigService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.FininfoChartconfigconfig
{
    public class FininfoChartconfigconfigRequestTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddFininfoChartconfigRequest_Success() =>
            Assert.NotNull(new AddFininfoChartconfigRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownFininfoChartconfigRequest_Success() =>
            Assert.NotNull(new DropDownFininfoChartconfigRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetFininfoChartconfigRequest_Success() =>
            Assert.NotNull(new GetFininfoChartconfigRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateFininfoChartconfigRequest_Success() =>
            Assert.NotNull(new UpdateFininfoChartconfigRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteFininfoChartconfigRequest_Success() =>
            Assert.NotNull(new DeleteFininfoChartconfigRequestHandler(_unitOfWork.Object));
    }
}
