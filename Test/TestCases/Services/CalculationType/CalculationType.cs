using Application.Services.CalculationTypeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.CalculationType
{
    public class CalculationType
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddCalculationTypeRequest_Success() =>
            Assert.NotNull(new AddCalculationTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteCalculationTypeRequest_Success() =>
            Assert.NotNull(new DeleteCalculationTypeRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownCalculationTypeRequest_Success() =>
     Assert.NotNull(new DropDownCalculationTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetCalculationTypeRequest_Success() =>
            Assert.NotNull(new GetCalculationTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateCalculationTypeRequest_Success() =>
            Assert.NotNull(new UpdateCalculationTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchCalculationTypeRequest_Success() =>
            Assert.NotNull(new SearchCalculationTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownCalculationTypeRequest()
        {
            var request = new DropDownCalculationTypeRequest()
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = 123
            };

            Assert.NotNull(request);

        }
    }
}
