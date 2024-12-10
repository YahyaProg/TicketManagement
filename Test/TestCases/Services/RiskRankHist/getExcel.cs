using Application.Services.RiskRankHistService;
using Core.ViewModel.RiskRankHistModel;
using MediatR;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Services.RiskRankHistTest
{
    public class GetExcel
    {
        private readonly Mock<IMediator> mediator = new();
        [Fact]
        public async Task GetRiskRankHistExcel()
        {
            // Arrange
            var system = new GetRiskRankHistExcelRequestHandler(mediator.Object);

            var response = new RiskRankHistCorporateSearchVM()
            {
                ChangeDate = DateTime.Now,
                Id = 1,
                BankStaffName = "Test",
                BasicRate = "232",
                BasicRateRisk = "23"
            };



            mediator.Setup(x => x.Send(It.IsAny<RiskRankHistSearchRequest>(), CancellationToken.None)).ReturnsAsync(
                new Core.GenericResultModel.ApiResult<PaginatedList<RiskRankHistCorporateSearchVM>>()
                {
                    Data = new PaginatedList<RiskRankHistCorporateSearchVM>(new List<RiskRankHistCorporateSearchVM>
                        {
                            new RiskRankHistCorporateSearchVM
                            {
                                ChangeDate = DateTime.Now,
                                Id = 1,
                                BankStaffName = "Test",
                                BasicRate = "232",
                                BasicRateRisk = "23"
                            }
                        }, 1, 1, 1)
                });


            // Act
            var res = await system.Handle(new GetRiskRankHistExcelRequest(), CancellationToken.None);



            // Assert
            Assert.True(res.IsSuccess);

        }
    }
}
