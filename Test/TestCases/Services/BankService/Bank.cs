using Application.Services.BankService;
using Application.Services.CompanyRelationService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.BankServiceTest
{
    public class BankRequestTes
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddBankRequest_Success() =>
            Assert.NotNull(new AddBankRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteBankRequest_Success() =>
            Assert.NotNull(new DeleteBankRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownBankRequest_Success() =>
     Assert.NotNull(new DropDownBankRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetBankRequest_Success() =>
            Assert.NotNull(new GetBankRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateBankRequest_Success() =>
            Assert.NotNull(new UpdateBankRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchBankRequest_Success() =>
            Assert.NotNull(new SearchBankRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownBankRequest()
        {
            var request = new DropDownBankRequest()
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = 0
            };

            Assert.NotNull(request);
        }
    }
}
