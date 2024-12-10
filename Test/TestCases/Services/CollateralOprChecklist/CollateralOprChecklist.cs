using Application.Services.CollateralOprChecklistService;
using Application.Services.ConditionDefService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.CollateralOprChecklist
{
    public class CollateralOprChecklistRequestTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddCollateralOprChecklistRequest_Success() =>
            Assert.NotNull(new AddCollateralOprChecklistRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownCollateralOprChecklistRequest_Success() =>
            Assert.NotNull(new DropDownCollateralOprChecklistRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetCollateralOprChecklistRequest_Success() =>
            Assert.NotNull(new GetCollateralOprChecklistRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchCollateralOprChecklistRequest_Success() =>
            Assert.NotNull(new SearchCollateralOprChecklistRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateCollateralOprChecklistRequest_Success() =>
            Assert.NotNull(new UpdateCollateralOprChecklistRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteCollateralOprChecklistRequest_Success() =>
            Assert.NotNull(new DeleteCollateralOprChecklistRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownCollateralOprChecklistRequest()
        {
            var request = new DropDownCollateralOprChecklistRequest()
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = 123
            };

            Assert.NotNull(request);

        }
    }
}
