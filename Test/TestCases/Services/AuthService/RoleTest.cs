using Application.Services.Auth.RoleService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.AuthServiceTest
{
    public class RoleTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddRoleRequest_Success() =>
            Assert.NotNull(new AddRoleRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetRoleRequest_Success() =>
            Assert.NotNull(new GetRoleRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchRoleRequest_Success() =>
            Assert.NotNull(new SearchRoleRequestHandler(_unitOfWork.Object));
        [Fact]
        public void UpdateRoleRequest_Success() =>
            Assert.NotNull(new UpdateRoleRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteRoleRequest_Success() =>
            Assert.NotNull(new DeleteRoleRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownRoleRequsest_Success() =>
             Assert.NotNull(new DropDownRoleRequestHandler(_unitOfWork.Object));
    }
}
