using Application.Services.Auth.MenuService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.AuthServiceTest
{
    public class MenuServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddMenuRequest_Success() =>
            Assert.NotNull(new AddMenuRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetMenuRequest_Success() =>
            Assert.NotNull(new GetMenuRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchMenuRequest_Success() =>
            Assert.NotNull(new SearchMenuRequestHandler(_unitOfWork.Object));
        [Fact]
        public void UpdateMenuRequest_Success() =>
            Assert.NotNull(new UpdateMenuRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteMenuRequest_Success() =>
            Assert.NotNull(new DeleteMenuRequestHandler(_unitOfWork.Object));
    }
}
