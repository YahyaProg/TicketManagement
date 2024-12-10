using Application.Services.UnitTypeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.UnitType
{
    public class UnitTypeRequestTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddUnitTypeRequest_Success() =>
            Assert.NotNull(new AddUnitTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteUnitTypeRequest_Success() =>
            Assert.NotNull(new DeleteUnitTypeRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownUnitTypeRequest_Success() =>
  Assert.NotNull(new DropDownUnitTypeRequestHandler(_unitOfWork.Object));
        [Fact]
        public void GetUnitTypeRequest_Success() =>
            Assert.NotNull(new GetUnitTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateUnitTypeRequest_Success() =>
            Assert.NotNull(new UpdateUnitTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchUnitTypeRequest_Success() =>
            Assert.NotNull(new SearchUnitTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownUnitTypeRequest()
        {
            var request = new DropDownUnitTypeRequest()
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = 123
            };

            Assert.NotNull(request);

        }
    }
}
