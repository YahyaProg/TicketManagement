using Application.Services.CreditTypeService;
using Infrastructure;
using Moq;


namespace Test.TestCases.Services.CreditTypeTest
{
    public class CreditType
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddCreditTypeRequest_Success() =>
        Assert.NotNull(new AddCreditTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateCreditTypeRequest_Success() =>
        Assert.NotNull(new UpdateCreditTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteCreditTypeRequest_Success() =>
            Assert.NotNull(new DeleteCreditTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetByIdCreditTypeRequest_Success() =>
            Assert.NotNull(new GetCreditTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchCreditTypeRequest_Success() =>
            Assert.NotNull(new SearchCreditTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownCreditTypeRequest_Success() =>
            Assert.NotNull(new DropDownSearchCreditTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownSearchCreditTypeRequest()
        {
            var request = new DropDownSearchCreditTypeRequest()
            {
                KeyWord = "a"
            };

            Assert.NotNull(request);

        }

    }
}
