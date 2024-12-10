using Application.Services.ManufacturingCompanyProductService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.ManufacturingCompanyProduct
{
    public class ManufacturingCompanyProductRequestTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddManufacturingCompanyProductRequest_Success() =>
            Assert.NotNull(new AddManufacturingCompanyProductRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteManufacturingCompanyProductRequest_Success() =>
            Assert.NotNull(new DeleteManufacturingCompanyProductRequestHandler(_unitOfWork.Object));
        [Fact]
        public void GetManufacturingCompanyProductRequest_Success() =>
            Assert.NotNull(new GetManufacturingCompanyProductRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateManufacturingCompanyProductRequest_Success() =>
            Assert.NotNull(new UpdateManufacturingCompanyProductRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchManufacturingCompanyProductRequest_Success() =>
            Assert.NotNull(new SearchManufacturingCompanyProductRequestHandler(_unitOfWork.Object));
    }
}
