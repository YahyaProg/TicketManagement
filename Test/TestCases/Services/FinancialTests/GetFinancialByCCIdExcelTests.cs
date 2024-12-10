using Application.Services.FinancialService;
using Core.GenericResultModel;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.FinancialTests
{
    public class GetFinancialByCCIdExcelTests
    {
        private readonly GetFinancialByCCIdExcelRequest request = new()
        {
            CorporateCustomerId = 10,
            Type = Core.Enums.ECompanyFinancialInfo_type.balance_sheet
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
        public async Task RowsNotFound()
        {
            var context = new Mock<DBContext>();

            context.Setup(x => x.FinancialYearInfos).ReturnsDbSet([new() { Id = 1, CorporateCustomerId = request.CorporateCustomerId }]);
            context.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet([]);
            context.Setup(x => x.FinancialInfoItems).ReturnsDbSet([]);

            var res = await getRes(context.Object);

            Assert.False(res.IsSuccess);
        }

        [Fact]
        public async Task Success()
        {
            var context = new Mock<DBContext>();
            context.Setup(x => x.ProposalDescriptions).ReturnsDbSet([]);
            context.Setup(x => x.FinancialYearInfos).ReturnsDbSet([new() { Id = 1, ToDate = DateTime.Now, CorporateCustomerId = request.CorporateCustomerId }]);
            context.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet([new() { Id = 1, Title = "", Type = request.Type }]);
            context.Setup(x => x.FinancialInfoItems).ReturnsDbSet([
                new() {
                FinancialInfoDefId = 1,
                FinancialInfo = new() {
                    FinancialYearInfoId = 1
                },
                CorporateCustomerId = request.CorporateCustomerId
            }]);

            var res = await getRes(context.Object);

            Assert.True(res.IsSuccess);
        }

        private async Task<ApiResult<FileContentResult>> getRes(DBContext context)
        {
            var handler = new GetFinancialByCCIdExcelRequestHandler(context);

            return await handler.Handle(request, CancellationToken.None);
        }
    }
}
