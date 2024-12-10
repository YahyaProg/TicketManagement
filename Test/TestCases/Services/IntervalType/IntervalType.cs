using Application.Services.IntervalTypeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.IntervalType
{
    public class IntervalTypeRequestTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddIntervalTypeRequest_Success() =>
            Assert.NotNull(new AddIntervalTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteIntervalTypeRequest_Success() =>
            Assert.NotNull(new DeleteIntervalTypeRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownIntervalTypeRequest_Success() =>
     Assert.NotNull(new DropDownIntervalTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetIntervalTypeRequest_Success() =>
            Assert.NotNull(new GetIntervalTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateIntervalTypeRequest_Success() =>
            Assert.NotNull(new UpdateIntervalTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchIntervalTypeRequest_Success() =>
            Assert.NotNull(new SearchIntervalTypeRequestHandler(_unitOfWork.Object));
    }
}
