using Application.Services.OrganizationService;
using Core.ViewModel;
using Infrastructure;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Services.OrganizationService
{
    public class OrganizationServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddOrganizationRequest_Success() =>
            Assert.NotNull(new AddOrganizationRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetOrganizationRequest_Success() =>
            Assert.NotNull(new GetOrganizationRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchOrganizationRequest_Success()
        {
            var handler = new SearchOrganizationRequestHandler(_unitOfWork.Object);
            var request = new SearchOrganizationRequest();
            var res = new PaginatedList<OrganizationVM>(new List<OrganizationVM>()
                {
                new ()
                    {
                        Manager = "Manager",
                        ParentOrganization = "ParentOrganization",
                        BranchOrganizationType = "BranchOrganizationType",
                    }
                }, 1, 1, 1);
            _unitOfWork.Setup(m => m.OrganizationRepo.Search(It.IsAny<OrganizationIM>())).ReturnsAsync(res);
            var result = handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);
        }
        [Fact]
        public void DropDownOrganizationRequest_Success() =>
   Assert.NotNull(new DropDownOrganizationRequestHandler(_unitOfWork.Object));
        [Fact]
        public void UpdateOrganizationRequest_Success() =>
            Assert.NotNull(new UpdateOrganizationRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteOrganizationRequest_Success() =>
            Assert.NotNull(new DeleteOrganizationRequestHandler(_unitOfWork.Object));
    }
}
