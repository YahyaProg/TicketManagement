using Moq.EntityFrameworkCore;
using Test.Helper;
using Core.Entities;
using MediatR;
using Moq;
using Application.Services.FinancialService;
using Application.Services.Report;
using Gateway.External.IServices;
using Core.Helpers;
using Gateway.Model.External.Responses;
using Core.Logger;
using Gateway.Model.External.Requests;

namespace Test.TestCases.Services.Report
{
    public class GetProposalReportPart4Test
    {
        public readonly Mock<IMediator> _mediator = new();
        private readonly Mock<IExternalServices> _exService = new();
        private readonly Mock<ILoggerManager> _logger = new();
        

        [Fact]
        public async Task GetProposalReportPart4_Test_Success()
        {
            // Arrange
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

            collection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet([
                new ProposalScheme(){Id = 1, CustomerId = 1, ProposalId = 1}
                ]);

            collection.Context.Setup(x => x.Customers).ReturnsDbSet([
              new Customer(){Id = 1, ClientNo="123"}
              ]);

            collection.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet([
             new CorporateCustomer(){Id = 1, CorpId="123"}
             ]);

            collection.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet([
               new IndividualCustomer(){Id = 5, NationalId="123"}
               ]);

            var cbCustomerRes = MoqHelper.GetExternalResponseMoq(new CbCustomerResponse() {CustomerId="123",NationalidNo="12345555",FirstName="Ali",LastName="Rezai" });
            _exService.Setup(x => x.CbCustomer(It.IsAny<CbCustomerRequest>())).ReturnsAsync(cbCustomerRes);

            var cbLcResponseList = new List<CbLcResponse>() {
                new CbLcResponse() {Amount=1000,Customerid="123",Accountno="1", Samat= new Samat(){SamatCollateral=[new SamatCollateral() {CollTypeCode="60" },] } },
                new CbLcResponse() {Amount=5000,Customerid="123",Accountno="2", Samat= new Samat(){SamatCollateral=[new SamatCollateral() {CollTypeCode="11" },] }  },
            }; 
            var cbLcRes = MoqHelper.GetExternalResponseMoq(cbLcResponseList);
            _exService.Setup(x => x.CbLc(It.IsAny<CbLcRequest>())).ReturnsAsync(cbLcRes);



            var handler = new GetProposalInfoReportPart4RequestHandler(collection.Context.Object, _exService.Object, _logger.Object);

            // Act
            var result = await handler.Handle(new GetProposalInfoReportPart4Request() { ProposalSchemeId = 1 }, CancellationToken.None);


            // Assert
            Assert.True(result.IsSuccess);

        }

    }
}
