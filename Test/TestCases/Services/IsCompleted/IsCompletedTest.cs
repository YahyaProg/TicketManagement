using Application.Services.IsCompletedService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.IsCompleted
{
    public class IsCompletedTest
    {

        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddIsCompletedRequest_Success() =>
            Assert.NotNull(new AddIsCompletedRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DeleteIsCompletedRequest_Success() =>
            Assert.NotNull(new DeleteIsCompletedRequestHandler(_unitOfWork.Object));
        [Fact]
        public void GetByIdIsCompletedRequest_Success() =>
            Assert.NotNull(new GetIsCompletedRequestHandler(_unitOfWork.Object));
        [Fact]
        public void UpdateCorporateSubTypeRequest_Success() =>
            Assert.NotNull(new UpdateIsCompletedRequestHandler(_unitOfWork.Object));
        [Fact]
        public void SearchCorporateSubTypeRequest_Success() =>
            Assert.NotNull(new SearchIsCompletedRequestHandler(_unitOfWork.Object));
    }
}
