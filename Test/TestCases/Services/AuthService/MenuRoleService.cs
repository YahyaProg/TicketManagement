using Application.Services.Auth.MenuRoleService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.AuthServiceTest
{
    public class MenuRoleServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddMenuRoleRequest_Success() =>
            Assert.NotNull(new AddMenuRoleRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetMenuRoleRequest_Success() =>
            Assert.NotNull(new GetMenuRoleRequestHandler(_unitOfWork.Object));


        [Fact]
        public void UpdateMenuRoleRequest_Success() =>
            Assert.NotNull(new UpdateMenuRoleRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteMenuRoleRequest_Success() =>
            Assert.NotNull(new DeleteMenuRoleRequestHandler(_unitOfWork.Object));
    }
}
