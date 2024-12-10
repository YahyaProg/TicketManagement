using Application.Services.LcSettleTypeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.LcSettleType
{
    public class LcSettleTypeTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddLcSettleTypeRequest_Success() =>
            Assert.NotNull(new AddLcSettleTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetLcSettleTypeRequest_Success() =>
            Assert.NotNull(new GetLcSettleTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchLcSettleTypeRequest_Success() =>
            Assert.NotNull(new SearchLcSettleTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownLcSettleTypeRequest_Success() =>
            Assert.NotNull(new DropDownLcSettleTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateLcSettleTypeRequest_Success() =>
            Assert.NotNull(new UpdateLcSettleTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteLcSettleTypeRequest_Success() =>
            Assert.NotNull(new DeleteLcSettleTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownLcSettleTypeRequest()
        {
            var request = new DropDownLcSettleTypeRequest
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = 0
            };

            Assert.NotNull(request);
        }
    }
}
