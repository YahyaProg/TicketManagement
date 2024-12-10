using Application.Services.FininfoChartService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.FininfoChart
{
    public class FininfoChartRequestTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddFininfoChartRequest_Success() =>
            Assert.NotNull(new AddFininfoChartRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownFininfoChartRequest_Success() =>
   Assert.NotNull(new DropDownFininfoChartRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetFininfoChartRequest_Success() =>
            Assert.NotNull(new GetFininfoChartRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchFininfoChartRequest_Success() =>
            Assert.NotNull(new SearchFininfoChartRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateFininfoChartRequest_Success() =>
            Assert.NotNull(new UpdateFininfoChartRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteFininfoChartRequest_Success() =>
            Assert.NotNull(new DeleteFininfoChartRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownFininfoChartRequest()
        {
            var request = new DropDownFininfoChartRequest()
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = 123
            };

            Assert.NotNull(request);

        }
    }
}
