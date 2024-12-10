using Application.Services.ProposalManagerLoanService;
using Infrastructure;
using Moq;
namespace Test.TestCases.Services.ProposalManagerLoan
{
    public class ProposalManagerLoanRequestTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddProposalManagerLoanServiceRequest_Success() =>
            Assert.NotNull(new AddProposalManagerLoanRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetProposalManagerLoanServiceRequest_Success() =>
            Assert.NotNull(new GetProposalManagerLoanRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateProposalManagerLoanServiceRequest_Success() =>
            Assert.NotNull(new UpdateProposalManagerLoanRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchProposalManagerLoanServiceRequest_Success() =>
            Assert.NotNull(new SearchProposalManagerLoanRequestHandler(_unitOfWork.Object));
    }
}
