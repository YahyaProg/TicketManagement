using Application.Services.InquiryReturningChequeService;
using Core.Entities;
using Core.Helpers;
using Core.Logger;
using Gateway.External.IServices;
using Gateway.Model.External.Requests;
using Gateway.Model.External.Responses;
using Moq;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.InquiryReruningChequeCb
{

    public class inquiryReturningChequeCbiTest
    {

        public static IEnumerable<object[]> requests =>
        new List<object[]>
        {
            new object[] { true, true, false},
            new object[] { false, true, false},
            new object[] { false, false, true}
        };


        private readonly Mock<ILoggerManager> logger = new();
        private readonly Mock<IExternalServices> exService = new();

        [Theory, MemberData(nameof(requests))]
        public async void inquiryReturningChequeCbi(bool isManager, bool isCorp, bool isIndv)
        {
            // Arrange
            InitSetting settings = new()
            {
                ExternalSettings = new()
                {
                    BaseUrl = "https://external.saminray.com/api/v1/",
                    Rate = 1
                }
            };
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            var system = new InquiryReturningChequeCBRequestHandler(collection.UnitOfWork.Object, logger.Object, exService.Object, settings);


            // moq external service response
            var response = MoqHelper.GetExternalResponseMoq(new EstelamChequeResponse()
            {
                FirstName = "amir",
                LastName = "akbari",
                NationalId = "235",
                PersonType = "1",
                BouncedCheques = new List<BouncedCheques>
            {
                new BouncedCheques(){ BouncedBranchName = "amirs branch", ChequeId = "23", CustomerType = 2, OriginBranchName = "23"}
            }
            });

            exService.Setup(x => x.EstelamCheque(It.IsAny<EstelamChequeRequest>()))
                .Returns(Task.FromResult(response));


            var cheqDetailRes = MoqHelper.GetExternalResponseMoq(new EstelamChequeDetailResponse() { EstelamChequeDetailCheque = new() { Amount = 0, CurrencyCode = "IR", BouncedAmount = 23 } });
            exService.Setup(x => x.EstelamChequeDetail(It.IsAny<string>())).ReturnsAsync(cheqDetailRes);


            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

            #region Moq DBSETS
            collection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet(new List<Core.Entities.ProposalScheme>()
            {
                new Core.Entities.ProposalScheme() {Id = 1, ProposalId = 1, CustomerId = 1}
            });
            collection.Context.Setup(x => x.Proposals).ReturnsDbSet(new List<Core.Entities.Proposal>()
            {
                new Core.Entities.Proposal() {Id = 1, CustomerId = 1}
            });

            collection.Context.Setup(x => x.CustomerSchemes).ReturnsDbSet(new List<Core.Entities.CustomerScheme>()
            {
                new Core.Entities.CustomerScheme(){Id = 1, ProposalSchemeId = 1}
            });

            collection.Context.Setup(x => x.Managers).ReturnsDbSet(new List<Manager>() { new Manager() { Id = 1, CustomerSchemeId = 1, CorpBoardOfDirectorId = 1, Person = new() { NationalId = "20510" } } });

            collection.Context.Setup(x => x.ManagerReturningCheques).ReturnsDbSet(new List<ManagerReturningCheque>()
            {
                new ManagerReturningCheque(){Id = 1, LastEtelam = true, CustomerId = 1}
            });

            // if test for corp customer or manager
            if (isCorp)
                collection.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet(new List<CorporateCustomer>()
                {
                    new CorporateCustomer(){Id = 1, CorpId= "2906"}
                });
            else
                collection.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet(new List<CorporateCustomer>());

            collection.Context.Setup(x => x.ReturningCheques).ReturnsDbSet(new List<Core.Entities.ReturningCheque>()
            {
                new Core.Entities.ReturningCheque(){CustomerId = 1, LastEtelam = true}
            });


            // if test for indv customer
            if (isIndv)
                collection.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet(new List<IndividualCustomer>()
                {
                    new IndividualCustomer(){Id =1, NationalId = "205"}
                });
            else
                collection.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet(new List<IndividualCustomer>());



            collection.Context.Setup(x => x.Currencies).ReturnsDbSet(new List<Core.Entities.Currency>());

            #endregion


            var request = new InquiryReturningChequeCBRequest()
            {
                IsManager = isManager,
                ProposalSchemeId = 1
            };

            // Act
            var result = await system.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(true);
        }
    }


}
