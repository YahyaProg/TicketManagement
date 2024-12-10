using Application.Services.BranchService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.BranchTest
{
    public class BranchRequestTes
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddBranchRequest_Success() =>
            Assert.NotNull(new AddBranchRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteBranchRequest_Success() =>
            Assert.NotNull(new DeleteBranchRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownBranchRequest_Success() =>
     Assert.NotNull(new DropDownBranchRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetBranchRequest_Success() =>
            Assert.NotNull(new GetBranchRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateBranchRequest_Success() =>
            Assert.NotNull(new UpdateBranchRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchBranchRequest_Success() =>
            Assert.NotNull(new SearchBranchRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownBranchRequest()
        {
            var request = new DropDownBranchRequest()
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = 1
            };

            Assert.NotNull(request);

        }
    }
}
