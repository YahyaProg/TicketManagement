using Application.Services.ServiceCompanyRankService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.ServiceCompanyRankTest
{
    public class ServiceCompanyRank
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        [Fact]
        public void AddServiceCompanyRankRequest_Success() =>
            Assert.NotNull(new AddServiceCompanyRankRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteServiceCompanyRankRequest_Success() =>
            Assert.NotNull(new DeleteServiceCompanyRankRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetByIdServiceCompanyRankRequest_Success() =>
            Assert.NotNull(new GetServiceCompanyRankRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateServiceCompanyRankRequest_Success() =>
            Assert.NotNull(new UpdateServiceCompanyRankRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchServiceCompanyRankRequest_Success() =>
            Assert.NotNull(new SearchServiceCompanyRankRequestHandler(_unitOfWork.Object));


        [Fact]
        public void DropDownServiceCompanyRankRequest_Success() =>
            Assert.NotNull(new DropDownServiceCompanyRankRequestHandler(_unitOfWork.Object));


        [Fact]
        public void DropDownServiceCompanyRankRequest()
        {
            var request = new DropDownServiceCompanyRankRequest()
            {
                KeyWord = "a"
            };

            Assert.NotNull(request);

        }
    }
}
