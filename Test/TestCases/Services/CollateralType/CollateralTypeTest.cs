using Application.Services.CollateralTypeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.CollateralType
{
    public class CollateralTypeTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddCollateralTypeRequest_Success() =>
            Assert.NotNull(new AddCollateralTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetCollateralTypeRequest_Success() =>
            Assert.NotNull(new GetCollateralTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchCollateralTypeRequest_Success() =>
            Assert.NotNull(new SearchCollateralTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateCollateralTypeRequest_Success() =>
            Assert.NotNull(new UpdateCollateralTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteCollateralTypeRequest_Success() =>
            Assert.NotNull(new DeleteCollateralTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownCollateralTypeRequest()
        {
            var request = new DropDownCollateralTypeRequest
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = 0
            };

            Assert.NotNull(request);
        }
    }
}
