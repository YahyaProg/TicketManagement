using Application.Services.FinancialService;
using Core.Entities;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Helper;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.FinancialTests
{
    public class GetFinancialByCCIdTest
    {
        private MoqCollection mocks = GetUnitOfWorkMoqCollection();

        private readonly GetFinancialByCCIdRequest request = new()
        {
            CorporateCustomerId = 1122,
            Type = Core.Enums.ECompanyFinancialInfo_type.balance_sheet
        };

        [Fact]
        public async Task YearNotFound()
        {
            mocks.Context.Setup(x => x.FinancialYearInfos).ReturnsDbSet(new List<Core.Entities.FinancialYearInfo>());

            var handler = new GetFinancialByCCIdRequestHandler(mocks.UnitOfWork.Object);

            var res = await handler.Handle(request, CancellationToken.None);

            Assert.False(res.IsSuccess);
        }

        [Fact]
        public async Task SaveChangeFailed()
        {
            #region arrange

            mocks.Context.Setup(x => x.FinancialYearInfos).ReturnsDbSet([new() { Id = 1, CorporateCustomerId = request.CorporateCustomerId, Deleted = false }]);

            mocks.Context.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet([new() { Id = 1, Title = "test" }]);

            mocks.Context.Setup(x => x.FinancialInfos).ReturnsDbSet([]);

            mocks.Context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

            var handler = new GetFinancialByCCIdRequestHandler(mocks.UnitOfWork.Object);

            #endregion

            var res = await handler.Handle(request, CancellationToken.None);

            Assert.False(res.IsSuccess);
        }

        [Fact]
        public async Task Success()
        {
            #region arrange

            mocks.Context.Setup(x => x.FinancialYearInfos).ReturnsDbSet([new() { Id = 1, CorporateCustomerId = request.CorporateCustomerId, Deleted = false }]);

            mocks.Context.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet(
                    [
                        new() {
                            Id = 1,
                            Title = "test",
                            InverseParent = [
                                new(){
                                    Id = 1,
                                    FinancialInfoItems = [
                                        new(){
                                            Id = 12,
                                            FinancialInfo = new(){
                                                FinancialYearInfoId = 1,
                                                CurrencyId = 12
                                            }
                                        }
                                    ]
                                }
                            ],
                            Type = request.Type,
                            Indx = 1,
                            ParentId = null
                        }
                    ]
                );

            mocks.Context.Setup(x => x.FinancialInfos).ReturnsDbSet([new() { FinancialYearInfoId = 1, Id = 11 }]);
            mocks.Context.Setup(x => x.FinancialInfoItems).ReturnsDbSet([new() { FinancialInfoId = 11, FinancialInfoDefId = 1 }]);
            mocks.Context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2000);

            var handler = new GetFinancialByCCIdRequestHandler(mocks.UnitOfWork.Object);

            #endregion

            var res = await handler.Handle(request, CancellationToken.None);

            Assert.True(res.IsSuccess);
            Assert.NotNull(res.Data);
        }
    }
}
