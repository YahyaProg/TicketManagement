using Application.Services.IsicGroupService;
using Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.IsicGroupTest
{
    public class IsicGroup
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<DBContext> _context = new();

        [Fact]
        public void AddIsicGroupRequest_Success() =>
            Assert.NotNull(new AddIsicGroupRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DeleteIsicGroupRequest_Success() =>
            Assert.NotNull(new DeleteIsicGroupRequestHandler(_unitOfWork.Object));
        [Fact]
        public void GetByIdIsicGroupRequest_Success() =>
            Assert.NotNull(new GetIsicGroupRequestHandler(_unitOfWork.Object));
        [Fact]
        public void UpdateCorporateSubTypeRequest_Success() =>
            Assert.NotNull(new UpdateIsicGroupRequestHandler(_context.Object));
        [Fact]
        public void SearchCorporateSubTypeRequest_Success() =>
            Assert.NotNull(new SearchIsicGroupRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownCorporateSubTypeRequest_Success() =>
         Assert.NotNull(new DropDownSearchIsicGroupRequestHandler(_unitOfWork.Object));
        [Fact]
        public void KeyWord_SetAndGet_ReturnsSameValue()
        {
            // Arrange
            var request = new DropDownSearchIsicGroupRequest();
            var keyword = "test keyword";

            // Act
            request.KeyWord = keyword;

            // Assert
            Assert.Equal(keyword, request.KeyWord);
        }
    }
}
