using Application.Services.BranchService;
using Application.Services.EducationTypeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.EducationType
{
    public class EducationTypeRequestTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddEducationTypeRequest_Success() =>
            Assert.NotNull(new AddEducationTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteEducationTypeRequest_Success() =>
            Assert.NotNull(new DeleteEducationTypeRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownEducationTypeRequest_Success() =>
   Assert.NotNull(new DropDownEducationTypeRequestHandler(_unitOfWork.Object));
        [Fact]
        public void GetEducationTypeRequest_Success() =>
            Assert.NotNull(new GetEducationTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateEducationTypeRequest_Success() =>
            Assert.NotNull(new UpdateEducationTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchEducationTypeRequest_Success() =>
            Assert.NotNull(new SearchEducationTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownEducationTypeRequest()
        {
            var request = new DropDownEducationTypeRequest()
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = 1
            };

            Assert.NotNull(request);

        }
    }
}
