using Application.Services.InqueryData;
using Core.Entities;
using Gateway.External.IServices;
using Gateway.Model.External.Requests;
using Gateway.Model.External.Responses;
using Moq;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.InqueryData
{
    public class GetIranianValidationRequest2Test
    {


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetIranianValidationRequestTest(int type)
        {
            // arrange
            var external = new Mock<IExternalServices>();
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();

            collection.Context.Setup(x => x.InqueryDatas).ReturnsDbSet([
                new Core.Entities.InqueryData(){
                    Id = 1,
                    ProposalSchemeId = 1,
                    ProposalId = 1,
                    InqueryType = Core.Enums.EInqueryType.ValidationOfIranians,
                    ExpireDate = type == 1 ? DateTime.Now.AddDays(3) : null
                }
                ]);

            collection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet([
                new ProposalScheme(){Id = 1, ProposalId = 1, CustomerRequestId = 1, CustomerId = 1}
                ]);

            collection.Context.Setup(x => x.Customers).ReturnsDbSet([
                new Customer(){Id = 1,
                  IndividualCustomer = new(){
                    Id = 1,
                    NationalId = "1235"
                   },
                  CorporateCustomer = new(){
                      Id = 1,
                      CorpId = "2342"
                  }
                }
            ]);

            var externaliranianRes = MoqHelper.GetExternalResponseMoq<GetIranianReportResponse>(new()
            {
                RiskScore = "235",

            });

            external.Setup(x => x.IranianGetReport(It.IsAny<GetIranianReportRequest>())).ReturnsAsync(externaliranianRes);

            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

            var handler = new GetIranianValidationRequest2Handler(collection.Context.Object, external.Object);

            // act
            var result = await handler.Handle(new() { ProposalSchemeId = 1 }, CancellationToken.None);


            // assert
            Assert.True(result.IsSuccess);
        }
    }
}
