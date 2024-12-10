using Application.Services.BrokersActivityService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.BrokersActivity
{
    public class BrokersActivityRequestTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddBrokersActivityRequest_Success() =>
            Assert.NotNull(new AddBrokersActivityRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetBrokersActivityRequest_Success() =>
            Assert.NotNull(new GetBrokersActivityRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchBrokersActivityRequest_Success() =>
            Assert.NotNull(new SearchBrokersActivityRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownBrokersActivityRequest_Success() =>
   Assert.NotNull(new DropDownBrokersActivityRequestHandler(_unitOfWork.Object));
        [Fact]
        public void UpdateBrokersActivityRequest_Success() =>
            Assert.NotNull(new UpdateBrokersActivityRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteBrokersActivityRequest_Success() =>
            Assert.NotNull(new DeleteBrokersActivityRequestHandler(_unitOfWork.Object));
    }
}
