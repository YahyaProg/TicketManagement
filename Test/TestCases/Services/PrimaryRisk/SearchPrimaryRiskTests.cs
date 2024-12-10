using Application.Services.PrimaryRiskService;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.PrimaryRisk
{
    public class SearchPrimaryRiskTests
    {

        [Fact]
        public async Task Success()
        {
            #region arrange

            var request = new SearchPrimaryRiskRequest
            {
                Category = Core.Enums.EPrimaryRisk_category.I,
                RiskCustomerGroupId = 1,
                Version = 1
            };

            var context = new Mock<DBContext>();

            context.Setup(x => x.PrimaryRisks).ReturnsDbSet([
                    new(){
                        Approver = new(){FirstName = "",LastName = ""},
                        Creator = new(){FirstName = "",LastName = ""},
                        RiskCustomerGroup = new(){Title = ""},
                        RiskInfoGroup = new(){Title ="" },
                        Formula = new(){Title = ""},
                        InverseParent = [
                                new(){
                                    RiskCustomerGroup = new(){Title = ""},
                                    RiskInfoGroup = new(){Title = ""},
                                    Formula = new(){Title = "" }
                                }]

                    }
                ]);


            var handler = new SearchPrimaryRiskRequestHandler(context: context.Object);

            #endregion

            var res = await handler.Handle(request, CancellationToken.None);

            Assert.True(res.IsSuccess);
        }
    }
}
