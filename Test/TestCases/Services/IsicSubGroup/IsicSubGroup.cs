using Application.Services.IsicSubGroupService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.IsicSubGroupTests
{
    public class IsicSubGroupTe
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddIsicSubGroupRequest_Success() =>
            Assert.NotNull(new AddIsicSubGroupRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteIsicGroupRequest_Success() =>
            Assert.NotNull(new DeleteIsicSubGroupRequestHandler(_unitOfWork.Object));
        [Fact]
        public void GetByIdIsicGroupRequest_Success() =>
            Assert.NotNull(new GetIsicSubGroupRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownCorporateSubTypeRequest_Success() =>
                 Assert.NotNull(new DropDownSearchIsicSubGroupRequestHandler(_unitOfWork.Object));
        [Fact]
        public void KeyWord_SetAndGet_ReturnsSameValue()
        {
            // Arrange
            var request = new DropDownSearchIsicSubGroupRequest();
            var keyword = "test keyword";
            var isicGroupId = 10;

            // Act
            request.KeyWord = keyword;
            request.IsicGroupId = isicGroupId;

            // Assert
            Assert.Equal(keyword, request.KeyWord);
        }
    }
}
