using Application.Services.BaseService;
using Application.Services.OccupationPlaceService;
using Core.Entities;
using Core.Enums;
using Core.GenericResultModel;
using Infrastructure;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.OccupationPlaceServiceTest;
public class AddOccupationPlaceRequestHandlerTests
{
    private readonly Mock<DBContext> _mockDbContext;
    private readonly Mock<IMediator> _mockMediator;
    private readonly AddOccupationPlaceRequestHandler _handler;

    public AddOccupationPlaceRequestHandlerTests()
    {
        _mockDbContext = new Mock<DBContext>();
        _mockMediator = new Mock<IMediator>();
        _handler = new AddOccupationPlaceRequestHandler(_mockDbContext.Object, _mockMediator.Object);
    }

    [Fact]
    public async Task Handle_CustomerSchemeNotFound_Returns404()
    {
        // Arrange
        var request = new AddOccupationPlaceRequest { CustomerSchemeId = 1 };
        _mockDbContext.Setup(db => db.CustomerSchemes).ReturnsDbSet([]);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Code);
        Assert.Equal("اسکیما مشتری یافت نشد", result.Message);
    }

    [Fact]
    public async Task Handle_InquiryCustomerFails_Returns400()
    {
        // Arrange
        var request = new AddOccupationPlaceRequest
        {
            OwnershipType = EOccupationPlace_ownershipType.ThirdPerson,
            CustomerSchemeId = 1
        };

        var customerSchemes = new List<CustomerScheme>() {
           new() { Id = 1, CustomerId = 10 }
        };
        _mockDbContext.Setup(db => db.CustomerSchemes).ReturnsDbSet(customerSchemes);

        _mockMediator.Setup(m => m.Send(
            It.IsAny<InquiryCustomerV2Request>(),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(new ApiResult<long> { IsSuccess = false });

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
        Assert.Equal("استعلام با خطا مواجه شد", result.Message);
    }

    [Fact]
    public async Task Handle_Success_ReturnsSuccess()
    {
        // Arrange
        var request = new AddOccupationPlaceRequest
        {
            CustomerSchemeId = 1,
            Fax = "12345",
            Phone = "54321",
            OwnershipType = EOccupationPlace_ownershipType.Owner,
            ProvinceId = 10,
            CityId = 20,
            Address = "Test Address",
            PostalCode = "123456",
            BuildingFootprint = "100",
            BuildingWholeArea = "200",
            WorkPlaceTypeId = 2,
            RelationId = 3
        };

        var customerSchemes = new List<CustomerScheme>() {
           new() { Id = 1, CustomerId = 10 }
        };
        _mockDbContext.Setup(db => db.CustomerSchemes).ReturnsDbSet(customerSchemes);
        _mockDbContext.Setup(x => x.OccupationPlaces.Add(It.IsAny<OccupationPlace>()));
        _mockDbContext.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }
}
