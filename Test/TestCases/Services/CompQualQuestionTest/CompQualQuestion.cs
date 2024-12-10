using Application.Services.CompQualQuestionService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.CompQualQuestionTest
{
    public class CompQualQuestionTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddCompQualQuestionRequest_Success() =>
            Assert.NotNull(new AddCompQualQuestionRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteCompQualQuestionRequest_Success() =>
            Assert.NotNull(new DeleteCompQualQuestionRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetByIdCompQualQuestionRequest_Success() =>
            Assert.NotNull(new GetCompQualQuestionRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateCompQualQuestionRequest_Success() =>
            Assert.NotNull(new UpdateCompQualQuestionRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchCompQualQuestionRequest_Success() =>
            Assert.NotNull(new SearchCompQualQuestionRequestHandler(_unitOfWork.Object));
    }
}
