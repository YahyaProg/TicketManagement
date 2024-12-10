using Application.Services.BaseService;
using Application.Services.CustomerAddressService;
using Core.Entities;
using Core.Enums;
using Core.GenericResultModel;
using Infrastructure;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;


namespace Test.TestCases.Services.CustomerAddressServiceTest;
//Test For Address
public class AddAddressRequestHandlerTests
{
    private readonly Mock<DBContext> _mockDbContext;
    private readonly Mock<IMediator> _mockMediator;
    private readonly AddAddressRequestHandler _handler;

    public AddAddressRequestHandlerTests()
    {
        _mockDbContext = new Mock<DBContext>();
        _mockMediator = new Mock<IMediator>();
        _handler = new AddAddressRequestHandler(_mockDbContext.Object, _mockMediator.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenAddressIsAddedSuccessfully()
    {

        var customerSchemes = new List<CustomerScheme>
            {
              new CustomerScheme { Id = 1, CustomerId = 100,}
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.CustomerSchemes).ReturnsDbSet(customerSchemes);

        _mockDbContext.Setup(x => x.CustomerAddresses.Add(It.IsAny<CustomerAddress>()));
        _mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var request = new AddAddressRequest
        {
            CustomerSchemeId = 1,            
            ProvinceId = 1,
            CityId = 1,
            Address = "Test Address",
            PostalCode = "123456",
            OwnershipType = EAddress_ownerShip.Owner,
            RelationId = 1
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenCustomerSchemeDoesNotExist()
    {
        // Arrange
        var customerSchemes = new List<CustomerScheme>
            {
              new CustomerScheme { Id = 1, CustomerId = 100,}
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.CustomerSchemes).ReturnsDbSet(customerSchemes);

        var request = new AddAddressRequest
        {
            CustomerSchemeId = 99
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("اسکیما مشتری یافت نشد", result.Message);
        _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenInquiryCustomerFails()
    {
        // Arrange
        var customerSchemes = new List<CustomerScheme>
            {
              new CustomerScheme { Id = 1, CustomerId = 100,}
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.CustomerSchemes).ReturnsDbSet(customerSchemes);

        _mockMediator.Setup(m => m.Send(It.IsAny<InquiryCustomerV2Request>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new ApiResult<long> { IsSuccess = false });

        var request = new AddAddressRequest
        {
            CustomerSchemeId = 1,
            OwnershipType = EAddress_ownerShip.ThirdPerson
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("استعلام با خطا مواجه شد", result.Message);
        _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldUpdateCustomerId_WhenOwnershipTypeIsThirdPerson()
    {
        // Arrange
        var customerSchemes = new List<CustomerScheme>
            {
              new CustomerScheme { Id = 1, CustomerId = 100,}
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.CustomerSchemes).ReturnsDbSet(customerSchemes);

        _mockDbContext.Setup(x => x.CustomerAddresses.Add(It.IsAny<CustomerAddress>()));
      
        _mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
       
        _mockMediator.Setup(m => m.Send(It.IsAny<InquiryCustomerV2Request>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new ApiResult<long> { IsSuccess = true, Data = 200 });

        var request = new AddAddressRequest
        {
            CustomerSchemeId = 1,
            OwnershipType = EAddress_ownerShip.ThirdPerson,
            ProvinceId = 1,
            CityId = 1,
            Address = "Test Address",
            PostalCode = "123456",
            RelationId = 1
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }


}