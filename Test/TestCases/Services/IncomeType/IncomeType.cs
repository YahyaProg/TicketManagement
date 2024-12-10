using Application.Services.IncomeTypeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.IncomeType
{
    public class IncomeTypeRequestTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddIncomeTypeRequest_Success() =>
            Assert.NotNull(new AddIncomeTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteIncomeTypeRequest_Success() =>
            Assert.NotNull(new DeleteIncomeTypeRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownIncomeTypeRequest_Success() =>
     Assert.NotNull(new DropDownIncomeTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetIncomeTypeRequest_Success() =>
            Assert.NotNull(new GetIncomeTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateIncomeTypeRequest_Success() =>
            Assert.NotNull(new UpdateIncomeTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchIncomeTypeRequest_Success() =>
            Assert.NotNull(new SearchIncomeTypeRequestHandler(_unitOfWork.Object));
    }
}
