using Application.Services.RiskRankHistService;
using Core.Entities;
using Core.Enums;
using Core.ViewModel.Base;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

public class AutomaticQuestionsInqueryRequestHandlerTests
{
    private readonly Mock<DBContext> _mockDbContext;
    private readonly AutomaticQuestionsInqueryRequestHandler _handler;

    public AutomaticQuestionsInqueryRequestHandlerTests()
    {
        _mockDbContext = new Mock<DBContext>();
        _handler = new AutomaticQuestionsInqueryRequestHandler(_mockDbContext.Object);
    }

    [Fact]
    public async Task Handle_ExistingInqueryData_ReturnsApiResultWithData()
    {
        // Arrange
        var request = new AutomaticQuestionsInqueryRequest
        {
            ProposalSchemeId = 1,
            InqueryType = EInqueryType.AccountAge
        };

        var inqueryData = new InqueryData
        {
            Id = 1,
            ProposalSchemeId = 1,
            InqueryType = EInqueryType.AccountAge,
            InqueryValue = 500,
            NormalizeValue = 500,
            InqueryTitle = "Test Title",
            ProposalId = 10,
            InqueryDate = DateTime.Now
        };

        _mockDbContext.Setup(db => db.InqueryDatas).ReturnsDbSet([inqueryData]);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(inqueryData.Id, result.Data.Id);
        Assert.Equal(inqueryData.InqueryType, result.Data.InqueryType);
    }

    [Fact]
    public async Task Handle_ProposalSchemeFound_CustomerDataExists_ReturnsMockedApiResult()
    {
        // Arrange
        var request = new AutomaticQuestionsInqueryRequest
        {
            ProposalSchemeId = 1,
            InqueryType = EInqueryType.AccountAge
        };

        var proposalScheme = new ProposalScheme { Id = 1, ProposalId = 10, CustomerId = 1 };

        var customers = new List<Customer>()
        {
             new (){ Id=1,ClientNo="123"},
             new (){ Id=2,ClientNo="456"}
        };

        var corpCustomers = new List<CorporateCustomer>()
        {
             new (){ Id=1,CorpId="1",Name="CompanyName1"},
             new (){ Id=2,CorpId="2",Name="CompanyName1"}
        };

        var customerData = new CustomerVMBaseModel
        {
            NationalId = "111",
            CorpId="1"
        };

        _mockDbContext.Setup(db => db.InqueryDatas).ReturnsDbSet([]);

        _mockDbContext.Setup(db => db.ProposalSchemes).ReturnsDbSet([proposalScheme]);

        _mockDbContext.Setup(db => db.Customers).ReturnsDbSet(customers);

        _mockDbContext.Setup(db => db.IndividualCustomers).ReturnsDbSet([]);

        _mockDbContext.Setup(db => db.CorporateCustomers).ReturnsDbSet(corpCustomers);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(request.ProposalSchemeId, result.Data.ProposalSchemeId);
        Assert.Equal(proposalScheme.CustomerId, result.Data.CustomerId);
    }

    [Fact]
    public async Task Handle_ProposalSchemeNotFound_ReturnsBadRequest()
    {
        // Arrange
        var request = new AutomaticQuestionsInqueryRequest
        {
            ProposalSchemeId = 1,
            InqueryType = EInqueryType.AccountAge
        };

        var inqueryData = new InqueryData
        {
            Id = 1,
            ProposalSchemeId = 1,
            InqueryType = EInqueryType.Debt,
            InqueryValue = 500,
            NormalizeValue = 500,
            InqueryTitle = "Test Title",
            ProposalId = 10,
            InqueryDate = DateTime.Now
        };


        var proposalScheme = new ProposalScheme { Id = 1, ProposalId = 10, CustomerId = 8 };

        var customers = new List<Customer>()
        {
             new (){ Id=1,ClientNo="123"},
             new (){ Id=2,ClientNo="456"}
        };

        var corpCustomers = new List<CorporateCustomer>()
        {
             new (){ Id=1,CorpId="1",Name="CompanyName1"},
             new (){ Id=2,CorpId="2",Name="CompanyName1"}
        };

        var customerData = new CustomerVMBaseModel
        {
            NationalId = "111",
            CorpId = "1"
        };

        _mockDbContext.Setup(db => db.InqueryDatas).ReturnsDbSet([inqueryData]);

        _mockDbContext.Setup(db => db.ProposalSchemes).ReturnsDbSet([proposalScheme]);

        _mockDbContext.Setup(db => db.Customers).ReturnsDbSet(customers);

        _mockDbContext.Setup(db => db.IndividualCustomers).ReturnsDbSet([]);

        _mockDbContext.Setup(db => db.CorporateCustomers).ReturnsDbSet(corpCustomers);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
    }
}