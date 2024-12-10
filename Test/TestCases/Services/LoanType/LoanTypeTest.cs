using Application.Services.LoanTypeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.LoanType
{
    public class LoanTypeTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddLoanTypeRequest_Success() =>
            Assert.NotNull(new AddLoanTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetLoanTypeRequest_Success() =>
            Assert.NotNull(new GetLoanTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchLoanTypeRequest_Success() =>
            Assert.NotNull(new SearchLoanTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownLoanTypeRequest_Success() =>
            Assert.NotNull(new DropDownLoanTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateLoanTypeRequest_Success() =>
            Assert.NotNull(new UpdateLoanTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteLoanTypeRequest_Success() =>
            Assert.NotNull(new DeleteLoanTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownLoanTypeRequest()
        {
            var request = new DropDownLoanTypeRequest
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = 0
            };

            Assert.NotNull(request);
        }
    }
}
