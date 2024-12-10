using Application.Services.ConditionDefService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.ConditionDef
{
    public class ConditionDef
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddConditionDefRequest_Success() =>
            Assert.NotNull(new AddConditionDefRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteConditionDefRequest_Success() =>
            Assert.NotNull(new DeleteConditionDefRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownConditionDefRequest_Success() =>
     Assert.NotNull(new DropDownConditionDefRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetConditionDefRequest_Success() =>
            Assert.NotNull(new GetConditionDefRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateConditionDefRequest_Success() =>
            Assert.NotNull(new UpdateConditionDefRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchConditionDefRequest_Success() =>
            Assert.NotNull(new SearchConditionDefRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownConditionDefRequest()
        {
            var request = new DropDownConditionDefRequest()
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = 123
            };

            Assert.NotNull(request);

        }
    }
}
