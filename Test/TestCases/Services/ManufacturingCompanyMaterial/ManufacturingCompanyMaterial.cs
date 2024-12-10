using Application.Services.ManufacturingCompanyMaterialService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.ManufacturingCompanyMaterial
{
    public class ManufacturingCompanyMaterialRequestTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddManufacturingCompanyMaterialRequest_Success() =>
            Assert.NotNull(new AddManufacturingCompanyMaterialRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteManufacturingCompanyMaterialRequest_Success() =>
            Assert.NotNull(new DeleteManufacturingCompanyMaterialRequestHandler(_unitOfWork.Object));
        [Fact]
        public void GetManufacturingCompanyMaterialRequest_Success() =>
            Assert.NotNull(new GetManufacturingCompanyMaterialRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateManufacturingCompanyMaterialRequest_Success() =>
            Assert.NotNull(new UpdateManufacturingCompanyMaterialRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchManufacturingCompanyMaterialRequest_Success() =>
            Assert.NotNull(new SearchManufacturingCompanyMaterialRequestHandler(_unitOfWork.Object));
    }
}
