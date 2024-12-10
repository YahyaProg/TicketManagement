using Application.Services.CompanyRelationService;
using Infrastructure;
using Moq;



namespace Test.TestCases.Services.CompanyRelation
{
    public class CompanyRelationTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        [Fact]
        public void AddCompanyRelationRequest_Success() =>
            Assert.NotNull(new AddCompanyRelationRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteCompanyRelationRequest_Success() =>
            Assert.NotNull(new DeleteCompanyRelationRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownCompanyRelationRequest_Success() =>
            Assert.NotNull(new DropDownCompanyRelationRequestHandler(_unitOfWork.Object));
        [Fact]
        public void GetByIdCompanyRelationRequest_Success() =>
            Assert.NotNull(new GetCompanyRelationRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateCompanyRelationRequest_Success() =>
            Assert.NotNull(new UpdateCompanyRelationRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchCompanyRelationRequest_Success() =>
            Assert.NotNull(new SearchCompanyRelationRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownCompanyRelationRequest()
        {
            var request = new DropDownCompanyRelationRequest()
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = 0
            };

            Assert.NotNull(request);
        }
        
    }
}
