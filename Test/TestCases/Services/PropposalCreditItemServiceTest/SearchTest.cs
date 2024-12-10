using Application.Services.ProposalCreditItemService;
using Core.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Test.TestCases.Services.ProposalCreditItemServiceTest
{
    public class SearchProposalCreditItemRequestHandlerTests
    {
        private readonly Mock<DBContext> _contextMock;
        private readonly SearchProposalCreditItemRequestHandler _handler;

        public SearchProposalCreditItemRequestHandlerTests()
        {
            _contextMock = new Mock<DBContext>();
            _handler = new SearchProposalCreditItemRequestHandler(_contextMock.Object);
        }




        [Fact]
        public void GetUsedAmount_CalculatesExpectedValues()
        {
            // Arrange
            SetupMockData();
            var pci = new ProposalCreditItem { Id = 1, PrevItemId = null, ParentItemId = null };
            var proposalCreditItems = new List<ProposalCreditItem>
            {
                new ProposalCreditItem { Id = 1, Amount = 1000 },
                new ProposalCreditItem { Id = 2, ParentItemId = 1, Amount = 500 }
            };
            var loanMebs = new List<LoanMeb> { new LoanMeb { ProposalCreditItemId = 1, PrincipalOutStanding = 100 } };
            var lcMebs = new List<Lcmeb> { new Lcmeb { ProposalCreditItemId = 1, PrincipalOutStanding = 200 } };
            var bgmebs = new List<Bgmeb> { new Bgmeb { ProposalCreditItemId = 1, UsedAmount = 300 } };

            // Act
            var usedAmount = SearchProposalCreditItemRequestHandler.GetUsedAmount(pci, proposalCreditItems, loanMebs, lcMebs, bgmebs, _contextMock.Object);

            // Assert
            Assert.Equal(600, usedAmount); // 100 + 200 + 300 from loanMebs, lcMebs, bgmebs
        }



        private void SetupMockData()
        {
            var proposalCreditItems = new List<ProposalCreditItem>
            {
                new ProposalCreditItem { Id = 1, ProposalId = 1, CreditTypeId = 1, Amount = 1000, ParentItemId = null, PrevItemId = null },
                new ProposalCreditItem { Id = 2, ProposalId = 1, CreditTypeId = 2, Amount = 2000, ParentItemId = 1, PrevItemId = null }
            }.AsQueryable();

            var creditTypes = new List<CreditType>
            {
                new CreditType { Id = 1, Title = "Credit Type 1" },
                new CreditType { Id = 2, Title = "Credit Type 2" }
            }.AsQueryable();

            // Mock the DbSets
            var proposalCreditItemsDbSetMock = new Mock<DbSet<ProposalCreditItem>>();
            proposalCreditItemsDbSetMock.As<IQueryable<ProposalCreditItem>>().Setup(m => m.Provider).Returns(proposalCreditItems.Provider);
            proposalCreditItemsDbSetMock.As<IQueryable<ProposalCreditItem>>().Setup(m => m.Expression).Returns(proposalCreditItems.Expression);
            proposalCreditItemsDbSetMock.As<IQueryable<ProposalCreditItem>>().Setup(m => m.ElementType).Returns(proposalCreditItems.ElementType);
            proposalCreditItemsDbSetMock.As<IQueryable<ProposalCreditItem>>().Setup(m => m.GetEnumerator()).Returns(proposalCreditItems.GetEnumerator());

            var creditTypesDbSetMock = new Mock<DbSet<CreditType>>();
            creditTypesDbSetMock.As<IQueryable<CreditType>>().Setup(m => m.Provider).Returns(creditTypes.Provider);
            creditTypesDbSetMock.As<IQueryable<CreditType>>().Setup(m => m.Expression).Returns(creditTypes.Expression);
            creditTypesDbSetMock.As<IQueryable<CreditType>>().Setup(m => m.ElementType).Returns(creditTypes.ElementType);
            creditTypesDbSetMock.As<IQueryable<CreditType>>().Setup(m => m.GetEnumerator()).Returns(creditTypes.GetEnumerator());

            _contextMock.Setup(c => c.ProposalCreditItems).Returns(proposalCreditItemsDbSetMock.Object);
            _contextMock.Setup(c => c.CreditTypes).Returns(creditTypesDbSetMock.Object);
        }
    }

}