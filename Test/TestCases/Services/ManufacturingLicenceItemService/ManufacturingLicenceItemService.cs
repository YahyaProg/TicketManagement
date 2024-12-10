using Application.Services.ManufacturingLicenceItemService;
using Application.Services.OrganizationService;
using Core.ViewModel;
using Core.ViewModel.ManufacturingLicenceItemItem;
using Infrastructure;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Services.ManufacturingLicenceItemService
{
    public class ManufacturingLicenceItemServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddManufacturingLicenceItemRequest_Success() =>
            Assert.NotNull(new AddManufacturingLicenceItemRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetManufacturingLicenceItemRequest_Success() =>
            Assert.NotNull(new GetManufacturingLicenceItemRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchManufacturingLicenceItemRequest_Success()
        {
            var handler = new SearchManufacturingLicenceItemRequestHandler(_unitOfWork.Object);
            var request = new SearchManufacturingLicenceItemRequest();
            var res = new PaginatedList<ManufacturingLicenceItemVM>(new List<ManufacturingLicenceItemVM>()
                {
                new ()
                    {
                        IssuerOrganization = "IssuerOrganization",
                        LicenceNo = "LicenceNo",
                        UnitTypeTitle = "UnitTypeTitle",
                    }
                }, 1, 1, 1);
            _unitOfWork.Setup(m => m.ManufacturingLicenceItemRepo.Search(It.IsAny<ManufacturingLicenceItemIM>())).ReturnsAsync(res);
            var result = handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);

        }
        [Fact]
        public void DropDownManufacturingLicenceItemRequest_Success() =>
            Assert.NotNull(new DropDownManufacturingLicenceItemRequestHandler(_unitOfWork.Object));
        [Fact]
        public void UpdateManufacturingLicenceItemRequest_Success() =>
            Assert.NotNull(new UpdateManufacturingLicenceItemRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteManufacturingLicenceItemRequest_Success() =>
            Assert.NotNull(new DeleteManufacturingLicenceItemRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownManufacturingLicenceItemRequest()
        {
            var request = new DropDownManufacturingLicenceItemRequest()
            {
                KeyWord = "a"
            };
            Assert.NotNull(request);

        }

    }
}
