using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services.LoanMebService;
using Core.Entities;
using Core.GenericResultModel;
using Core.Helpers;
using Gateway.External.IServices;
using Gateway.Model.External.Requests;
using Gateway.Model.External.Responses;
using Moq;
using Moq.EntityFrameworkCore;
using Test.Helper;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.LoanMebTest
{
    public class EstelamLoanMebRequestHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenLoanMebCountIsUpdated()
        {
            // Arrange
            var request = new EstelamLoanMebRequest
            {
                ProposalId = 1,
                CustomerId = 1
            };

            var externalServicesMock = new Mock<IExternalServices>();
            var moqCollection = GetUnitOfWorkMoqCollection();
            var initSetting = new InitSetting { ExternalSettings = new ExternalSettings { Rate = 10 } };

            var handler = new EstelamLoanMebRequestHandler(externalServicesMock.Object, moqCollection.UnitOfWork.Object, initSetting);

            // Mocking Proposal Data
            moqCollection.Context.Setup(x => x.Proposals)
                .ReturnsDbSet(new List<Proposal>
                {
                    new Proposal
                    {
                        Id = request.ProposalId,
                        Customer = new Customer
                        {
                            Id = request.CustomerId,
                            CorporateCustomer = new CorporateCustomer { CorpId = "CORP123" }
                        }
                    }
                });

            // Mocking ProposalSchemes, CustomerSchemes, and Managers
            moqCollection.Context.Setup(x => x.ProposalSchemes)
                .ReturnsDbSet(new List<Core.Entities.ProposalScheme>
                {
                    new Core.Entities.ProposalScheme
                    {
                        Id = 1,
                        Proposal = new Proposal { Id = request.ProposalId },
                        Deleted = false
                    }
                });

            moqCollection.Context.Setup(x => x.CustomerSchemes)
                .ReturnsDbSet(new List<Core.Entities.CustomerScheme>
                {
                    new Core.Entities.CustomerScheme { Id = 1, ProposalScheme = new Core.Entities.ProposalScheme { Id = 1 } }
                });

            moqCollection.Context.Setup(x => x.LoanMebs).ReturnsDbSet([
                new Core.Entities.LoanMeb(){
                    CustomerId = 1,
                }
                ]);

            moqCollection.Context.Setup(x => x.LoanTypes).ReturnsDbSet([
             new Core.Entities.LoanType(){
                    Id = 1
                }
             ]);

            moqCollection.Context.Setup(x => x.ProposalManagerLoans).ReturnsDbSet([
           new Core.Entities.ProposalManagerLoan(){
                    Id = 1
                }
           ]);

            moqCollection.Context.Setup(x => x.Currencies).ReturnsDbSet([
           new Core.Entities.Currency(){
                    Id = 1,
                    Code = "2"
                }
           ]);

            moqCollection.Context.Setup(x => x.LoanStatuses).ReturnsDbSet([
           new Core.Entities.LoanStatus(){
                    Id = 1,
                    Code= "2"
                }
           ]);

            moqCollection.Context.Setup(x => x.Managers)
                .ReturnsDbSet(new List<Manager>
                {
                    new Manager { Id = 1, CustomerScheme = new Core.Entities.CustomerScheme { Id = 1 }, PersonId = 2 }
                });

            // Mocking Customers with both corporate and individual profiles
            moqCollection.Context.Setup(x => x.Customers)
                .ReturnsDbSet(new List<Customer>
                {
                    new Customer { Id = 1, CorporateCustomer = new CorporateCustomer { CorpId = "CORP123" } },
                    new Customer { Id = 2, IndividualCustomer = new IndividualCustomer { NationalId = "NID123" } }
                });


            var cbCustomerRes = MoqHelper.GetExternalResponseMoq(new CbCustomerResponse()
            {
                CustomerId = "23",

            });

            var cbloanRes = MoqHelper.GetExternalResponseMoq(new List<CbLoanResponse> {
                new CbLoanResponse() { Id = 23, Accountno = "sd", LocalLedgerBalance = 1, NpaClassCode = "100", Scheme = "amir", SchemeDesc = "salam"}
            });

            externalServicesMock.Setup(x => x.CbCustomer(It.IsAny<CbCustomerRequest>())).ReturnsAsync(cbCustomerRes);
            externalServicesMock.Setup(x => x.CbLoan(It.IsAny<CbLoanRequest>())).ReturnsAsync(cbloanRes);


            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
