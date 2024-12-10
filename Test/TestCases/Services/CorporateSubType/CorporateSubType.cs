using Application.Services.CorporateSubTypeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.CorporateSubType
{
    public class CorporateSubType
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddCorporateSubTypeRequest_Success() =>
            Assert.NotNull(new AddCorporateSubTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteCorporateSubTypeRequest_Success() =>
            Assert.NotNull(new DeleteCorporateSubTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetByIdCorporateSubTypeRequest_Success() =>
            Assert.NotNull(new GetCorporateSubTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateCorporateSubTypeRequest_Success() =>
            Assert.NotNull(new UpdateCorporateSubTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchCorporateSubTypeRequest_Success() =>
            Assert.NotNull(new SearchCorporateSubTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownSearchCorporateSubTypeRequest_Success() =>
            Assert.NotNull(new DropDownSearchCorporateSubTypeRequestHandler(_unitOfWork.Object));


        [Fact]
        public void DropDownSearchCorporateSubTypeRequest()
        {
            var request = new DropDownSearchCorporateSubTypeRequest()
            {
                KeyWord = "a"
            };

            Assert.NotNull(request);

        }
    }
}
