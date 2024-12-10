using Moq.EntityFrameworkCore;
using Test.Helper;
using Core.Entities;
using MediatR;
using Moq;
using Application.Services.FinancialService;
using Application.Services.Report;

namespace Test.TestCases.Services.Report
{
    public class GetproposalReportPart2Test
    {
        public readonly Mock<IMediator> _mediator = new();

        [Fact]
        public async Task GetproposalReportPart2_Test_Success()
        {
            // Arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();

            collection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet([
                new Core.Entities.ProposalScheme(){Id = 1, CustomerId = 1, ProposalId = 1}
                ]);

            collection.Context.Setup(x => x.CustomerSchemes).ReturnsDbSet([
              new Core.Entities.CustomerScheme(){Id = 1, ProposalId = 1, CustomerId = 1, ProposalSchemeId = 1}
              ]);


            collection.Context.Setup(x => x.FinancialYearInfos).ReturnsDbSet([
              new Core.Entities.FinancialYearInfo(){Id = 1, CorporateCustomerId  = 1}
              ]);

            collection.Context.Setup(x => x.FinancialInfoItems).ReturnsDbSet([
              new Core.Entities.FinancialInfoItem(){Id = 1, CorporateCustomerId  = 1, FinancialInfoDefId = 1, FinancialInfoId = 1},
              new Core.Entities.FinancialInfoItem(){Id = 0, CorporateCustomerId  = 0, FinancialInfoDefId = 0, FinancialInfoId = 0}
              ]);

            collection.Context.Setup(x => x.FinancialInfos).ReturnsDbSet([
             new Core.Entities.FinancialInfo(){Id = 1, CorporateCustomerId  = 1, CurrencyId = 1, FinancialYearInfoId = 1},
             new Core.Entities.FinancialInfo(){Id = 0, CorporateCustomerId  = 0, CurrencyId = 0, FinancialYearInfoId = 0}
             ]);


            collection.Context.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet([
              new Core.Entities.CompanyFinancialInfo(){Id = 1, ParentId = 1, FinancialInfoItems = [
                  new FinancialInfoItem(){Id = 1, CorporateCustomerId = 1, FinancialInfoDefId = 1, FinancialInfoId = 1, FinancialInfo = new Core.Entities.FinancialInfo(){
                          Id = 1,
                          CorporateCustomerId = 1,
                          CurrencyId = 1,
                          FinancialYearInfoId = 1
                      }
                  },
                  new FinancialInfoItem(){Id = 1, CorporateCustomerId = 1, FinancialInfoDefId = 1, FinancialInfoId = 1, FinancialInfo = new Core.Entities.FinancialInfo(){
                          Id = 1,
                          CorporateCustomerId = 1,
                          CurrencyId = 1,
                          FinancialYearInfoId = 1
                        }}
                  ]}
              ]);


            collection.Context.Setup(x => x.CalcFinancialInfos).ReturnsDbSet([]);
            collection.Context.Setup(x => x.MajorItems).ReturnsDbSet([
              new Core.Entities.MajorItems(){Id = 1, ProposalSchemeId = 1, CustomerId = 1, ProposalId = 1}
              ]);

            collection.Context.Setup(x => x.ProposalDescriptions).ReturnsDbSet([
            new Core.Entities.ProposalDescription(){Id = 1, ProposalId = 1, Category = "financial_me"}
            ]);

            _mediator.Setup(x => x.Send(It.IsAny<GetCalcFinancialRequest>(), CancellationToken.None)).ReturnsAsync(new Core.GenericResultModel.ApiResult<Core.ViewModel.FinancialModels.SearchCalcFinancialVM>()
            {
                Data = new()
            });


            var handler = new GetProposalInfoReportPart2RequestHandler(collection.Context.Object, _mediator.Object);

            // Act
            var result = await handler.Handle(new GetProposalInfoReportPart2Request() { ProposalSchemeId = 1 }, CancellationToken.None);


            // Assert
            Assert.True(result.IsSuccess);

        }

    }
}
