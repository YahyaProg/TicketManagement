using Application.Services.CreditSubTypeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.CreditSubTypeTest
{
    public class CreditSubTypeTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddCreditSubTypeRequest_Success() =>
        Assert.NotNull(new AddCreditSubTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateCreditSubTypeRequest_Success() =>
            Assert.NotNull(new UpdateCreditSubTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteCreditSubTypeRequest_Success() =>
            Assert.NotNull(new DeleteCreditSubTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetByIdCreditSubTypeRequest_Success() =>
            Assert.NotNull(new GetCreditSubTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownSearchCreditSubTypeRequest_Success() =>
            Assert.NotNull(new DropDownSearchCreditSubTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownSearchCreditSubTypeRequest()
        {
            var request = new DropDownSearchCreditSubTypeRequest()
            {
                KeyWord = "a",
                CreditTypeId = 1,
            };

            Assert.NotNull(request);
        }
    }
}
