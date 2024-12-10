using Application.Services.RiskCalculation;
using Application.Services.RiskCalculationEngineService;
using Application.Services.RiskInfoService;
using Core.Entities;
using Core.GenericResultModel;
using Core.Helpers;
using Core.Logger;
using Core.ViewModel.RiskInfoPage;
using Gateway;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.EntityFrameworkCore;
using System.Net;

public class CalculateRiskRequestHandlerTests
{
    private readonly Mock<IApiClient> _apiClientMock;
    private readonly Mock<DBContext> _dbContextMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<ILoggerManager> _loggerMock;
    private readonly InitSetting _setting;
    private readonly CalculateRiskRequestHandler _handler;

    public CalculateRiskRequestHandlerTests()
    {
        _apiClientMock = new Mock<IApiClient>();
        _dbContextMock = new Mock<DBContext>();
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILoggerManager>();
        _setting = new InitSetting { RiskService = new RiskService { BaseUrl = "https://example.com" } };

        _handler = new CalculateRiskRequestHandler(_apiClientMock.Object, _setting, _dbContextMock.Object, _mediatorMock.Object, _loggerMock.Object);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task calculateRiskTest_success(bool type)
    {
        // Arrange
        var request = new CalculateRiskRequest
        {
            ProposalId = 1,
            Ver = 1,
            Year = 1403 // Missing Year
        };

        _setting.LocalEngine = type;

        _mediatorMock.Setup(x => x.Send(It.IsAny<CalculationEngineRequest>(), CancellationToken.None)).ReturnsAsync(new ApiResult(200, true));

        var mockContent = new StringContent("{\"IsSuccess\": true}");

        _apiClientMock
            .Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<RiskServiceParam>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = mockContent
            });

        _dbContextMock.Setup(x => x.CorporateCustomers).ReturnsDbSet([
            new CorporateCustomer(){Id = 1}
            ]);

        _dbContextMock.Setup(x => x.Proposals).ReturnsDbSet([
            new Proposal(){Id = 1, CustomerId = 1, CommitteeId = 1}
            ]);

        _dbContextMock.Setup(x => x.ProposalSchemes).ReturnsDbSet([
           new ProposalScheme(){Id = 1, CustomerId = 1,ProposalId = 1, ProposalUsageTypeId = 1}
           ]);

        _dbContextMock.Setup(x => x.ProposalSchemes).ReturnsDbSet([
           new ProposalScheme(){Id = 1, CustomerId = 1,ProposalId = 1, ProposalUsageTypeId = 1}
           ]);

        _dbContextMock.Setup(x => x.RiskInfos).ReturnsDbSet([
           new RiskInfo(){Id = 1, CustomerId = 1,ProposalId = 1}
           ]);


        _dbContextMock.Setup(x => x.CustomerRequestDetails).ReturnsDbSet([
           new CustomerRequestDetail(){Id = 1, CustomerId = 1,ProposalId = 1}
           ]);

        _dbContextMock.Setup(x => x.FinancialYearInfos).ReturnsDbSet([
          new FinancialYearInfo(){Id = 1, CorporateCustomerId = 1}
          ]);


        _dbContextMock.Setup(x => x.FinancialInfos).ReturnsDbSet([
            new FinancialInfo(){Id = 1, CorporateCustomerId = 1, FinancialYearInfoId = 1}
        ]);

        _dbContextMock.Setup(x => x.FinancialInfoItems).ReturnsDbSet([
            new FinancialInfoItem(){Id = 1, CorporateCustomerId = 1, FinancialInfoDefId = 1, FinancialInfoId = 1},
            new FinancialInfoItem(){Id = 2, CorporateCustomerId = 1, FinancialInfoDefId = 2, FinancialInfoId = 1}
        ]);


        _dbContextMock.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet([
           new CompanyFinancialInfo(){Id = 1, Code = "wealth"},
           new CompanyFinancialInfo(){Id = 2, Code = "lostanbashte"},
           ]);

        _dbContextMock.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(2);


        IEnumerable<RiskInfoSubItemVm> subItems = [
                new RiskInfoSubItemVm(){ Id = 1, Title = "sub3"}
                ];

        IEnumerable<RiskInfoItemVm> items = [
            new RiskInfoItemVm(){ Id = 1, Title = "laye2", SubItems = subItems, AutoCalculate = Core.Enums.ERiskInfoItem_autoCalculate.RETURNING_CHEQUE_AMOUNT_OTHER_BANKS , Answer = new() { Id = 1, RiskInfoSubItemId = 1, AutoCalculate = Core.Enums.ERiskInfoItem_autoCalculate.NON_CURRENT_CUSTOMER_DEBTS }  }
            ];
        IEnumerable<RiskInfoGroupVm> response = [
             new RiskInfoGroupVm(){Id =1, Title = "laye1" , Items = items}
        ];
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetRiskInfoAnswerRequest>(), CancellationToken.None)).ReturnsAsync(new ApiResult<IEnumerable<RiskInfoGroupVm>>()
        {
            Data = response
        });

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }




    [Fact]
    public async Task Handle_ShouldReturnError_WhenYearIsNull()
    {
        // Arrange
        var request = new CalculateRiskRequest
        {
            ProposalId = 123,
            Ver = 1,
            Year = null // Missing Year
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(400, result.Code);
        Assert.Equal("لطفا اطلاعات مربوط به سال های مالی را وارد کنید!", result.Message);
    }


    [Fact]
    public async Task Handle_ShouldReturnError_WhenProposalSchemeMissing()
    {
        // Arrange
        var request = new CalculateRiskRequest
        {
            ProposalId = 1,
            Ver = 1,
            Year = 2021
        };

        var corporateCustomers = new List<CorporateCustomer>
        {
            new() { Id = 1, CorpId = "31231", Name = "Company1" },
            new() { Id = 2, CorpId = "31123", Name = "Company2" }
        }.AsQueryable();

        var proposals = new List<Proposal>
        {
            new() { Id = 1, CustomerId =1, Status=Core.Enums.EProposal_status.todo },
            new() { Id = 2, CustomerId = 2,Status=Core.Enums.EProposal_status.todo }
        }.AsQueryable();

        var proposalSchemes = new List<ProposalScheme>
        {
            new() { Id = 1, CustomerId =1,ProposalId=6 ,Status=Core.Enums.EProposal_status.todo },
            new() { Id = 2, CustomerId = 2,ProposalId=7,Status=Core.Enums.EProposal_status.todo }
        }.AsQueryable();


        _dbContextMock.Setup(x => x.CorporateCustomers).ReturnsDbSet(corporateCustomers);
        _dbContextMock.Setup(x => x.Proposals).ReturnsDbSet(proposals);
        _dbContextMock.Setup(x => x.ProposalSchemes).ReturnsDbSet(proposalSchemes);

        // Mock answer response with failure
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetRiskInfoAnswerRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ApiResult<IEnumerable<RiskInfoGroupVm>> { });

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(400, result.Code);
        Assert.Equal("پرونده یافت نشد!", result.Message);
    }


    [Fact]
    public void Calculate_ShouldReturnCorrectValue_WhenValidInputsProvided()
    {
        // Arrange
        var riskInfo = new RiskInfo();
        var riskInfoVm = new RiskInfoVm { AutoCalculate = (Core.Enums.ERiskInfoItem_autoCalculate?)0.5 };
        double amount = 1000;

        // Act
        var result = CalculateRiskRequestHandler.Calculate(riskInfo, riskInfoVm, amount);

        // Assert
        Assert.NotNull(result);
        // Check that result contains the expected calculated value
    }
}
