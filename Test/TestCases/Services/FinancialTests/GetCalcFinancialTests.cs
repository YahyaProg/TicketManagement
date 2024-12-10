using Application.Services.FinancialService;
using Core.GenericResultModel;
using Core.ViewModel.FinancialModels;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.FinancialTests
{
    public class GetCalcFinancialTests
    {
        private readonly GetCalcFinancialRequest request = new()
        {
            CorporateCustomerId = 1,
            Type = Core.Enums.ECalcFinancialInfo_type.estekhraj

        };

        [Fact]
        public async Task YearNotFound()
        {
            var context = new Mock<DBContext>();

            context.Setup(x => x.FinancialYearInfos).ReturnsDbSet([]);

            var res = await getRes(context.Object);

            Assert.False(res.IsSuccess);
        }

        [Fact]
        public async Task Success()
        {
            var context = new Mock<DBContext>();

            context.Setup(x => x.FinancialYearInfos).ReturnsDbSet([
                new() { Deleted = false,
                    CorporateCustomerId = request.CorporateCustomerId,
                    Id = 1,
                    ToDate = DateTime.Now.AddYears(-1),
                },
                new() { Deleted = false,
                    CorporateCustomerId = request.CorporateCustomerId,
                    Id = 2 ,
                    ToDate = DateTime.Now.AddYears(-2),
                }
            ]);

            context.Setup(x => x.CalcFinancialInfos).ReturnsDbSet([
                    new(){
                        Id = 1,
                        Code = "ajab",
                        Title = "Title",
                        Indx = 1,
                        Type = request.Type,
                        Formula = "ajab+last_ajab2+last_ajab"
                    }
                ]);

            context.Setup(x => x.RelCalFinancialInfo_CompanyFinancialInfos).ReturnsDbSet([new() {
                CalcFinancialInfoId = 1,
                CompanyFinancialInfoId = 1,
                CompanyFinancialInfoCode = "ajab",
                Sign = Core.Enums.ECompanyFinancialInfo_sign.minus_one,
            },new() {
                CalcFinancialInfoId = 1,
                CompanyFinancialInfoId = 2,
                CompanyFinancialInfoCode = "ajab2",
                Sign = Core.Enums.ECompanyFinancialInfo_sign.one,
            }]);

            context.Setup(x => x.FinancialInfoItems).ReturnsDbSet([new() {
                CorporateCustomerId = request.CorporateCustomerId,
                Value = 100,
                FinancialInfoDefId = 1,
                FinancialInfo = new(){
                    FinancialYearInfoId = 1,
                }
            },
                new() {
                CorporateCustomerId = request.CorporateCustomerId,
                FinancialInfoDefId = 1,
                Value = 100,
                FinancialInfo = new(){
                    FinancialYearInfoId = 2,
                }
            },new() {
                CorporateCustomerId = request.CorporateCustomerId,
                FinancialInfoDefId = 2,
                Value = 100,
                FinancialInfo = new(){
                    FinancialYearInfoId = 2,
                }
            }]);

            var res = await getRes(context.Object);

            Assert.True(res.IsSuccess);
        }

        private async Task<ApiResult<SearchCalcFinancialVM>> getRes(DBContext context)
        {
            var handler = new GetCalcFinancialRequestHandler(context);

            return await handler.Handle(request, CancellationToken.None);
        }
    }
}
