using Application.Services.BankGuaranteeTypeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.BankGuaranteeTypeGuaranteeTypeService
{
    public class BankGuaranteeTypeGuaranteeType
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddBankGuaranteeTypeRequest_Success() =>
            Assert.NotNull(new AddBankGuaranteeTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteBankGuaranteeTypeRequest_Success() =>
            Assert.NotNull(new DeleteBankGuaranteeTypeRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownBankGuaranteeTypeRequest_Success() =>
     Assert.NotNull(new DropDownBankGuaranteeTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetBankGuaranteeTypeRequest_Success() =>
            Assert.NotNull(new GetBankGuaranteeTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateBankGuaranteeTypeRequest_Success() =>
            Assert.NotNull(new UpdateBankGuaranteeTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchBankGuaranteeTypeRequest_Success() =>
            Assert.NotNull(new SearchBankGuaranteeTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownBankGuaranteeTypeRequest()
        {
            var request = new DropDownBankGuaranteeTypeRequest()
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = 0
            };

            Assert.NotNull(request);
        }
    }
}
