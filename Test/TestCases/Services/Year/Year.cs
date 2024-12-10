using Application.Services.EducationTypeService;
using Application.Services.YearService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.Year
{
    public class YearRequestTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddYearRequest_Success() =>
            Assert.NotNull(new AddYearRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteYearRequest_Success() =>
            Assert.NotNull(new DeleteYearRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownYearRequest_Success() =>
            Assert.NotNull(new DropDownYearRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetYearRequest_Success() =>
            Assert.NotNull(new GetYearRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateYearRequest_Success() =>
            Assert.NotNull(new UpdateYearRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchYearRequest_Success() =>
            Assert.NotNull(new SearchYearRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownYearRequest()
        {
            var request = new DropDownYearRequest()
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = 1
            };

            Assert.NotNull(request);
        }

        [Fact]
        public void SpecialDropDownYearRequest()
        {
            var request = new SpecialDropDownYearRequest()
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = "a"
            };

            Assert.NotNull(request);
        }
    }
}
