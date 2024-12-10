using Application.Services.CbiRegInfoService;
using Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.CbiRegInfo
{
    public class CbiRegInfo
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddIsicGroupRequest_Success() =>
            Assert.NotNull(new AddCbiRegInfoRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DeleteIsicGroupRequest_Success() =>
            Assert.NotNull(new DeleteCbiRegInfoRequestHandler(_unitOfWork.Object));
        [Fact]
        public void GetByIdIsicGroupRequest_Success() =>
            Assert.NotNull(new GetCbiRegInfoRequestHandler(_unitOfWork.Object));
        [Fact]
        public void UpdateCorporateSubTypeRequest_Success() =>
            Assert.NotNull(new UpdateCbiRegInfoRequestHandler(_unitOfWork.Object));
        [Fact]
        public void SearchCorporateSubTypeRequest_Success() =>
            Assert.NotNull(new AdvanceSearchCbiRegInfoRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownCorporateSubTypeRequest_Success() =>
            Assert.NotNull(new DropDownSearchCbiRegInfoRequestHandler(_unitOfWork.Object));
        [Fact]
        public void KeyWord_SetAndGet_ReturnsSameValue()
        {
            // Arrange
            var request = new DropDownSearchCbiRegInfoRequest();
            var keyword = "test keyword";

            // Act
            request.KeyWord = keyword;

            // Assert
            Assert.Equal(keyword, request.KeyWord);
        }
    }
}
