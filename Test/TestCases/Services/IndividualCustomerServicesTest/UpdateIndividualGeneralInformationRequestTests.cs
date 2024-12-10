using Application.Services.CustomerService;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;
namespace Test.TestCases.Services.IndividualCustomerServicesTest;

public class UpdateIndividualGeneralInformationRequestHandlerTests
{


    [Fact]
    public async Task Handle_ShouldUpdateModel_WhenValidRequest()
    {
        // Arrange
        var mockContext = new Mock<DBContext>();
        var individualCustomer =  new IndividualCustomer
        {
            Id = 1,
            FatherName = "OldName",
            BirthDate = DateTime.Parse("1979/09/11"),
            ShPlaceId = 1,
            BirthPlaceId = 1,
            ShId = "123456789"
        };
        mockContext.Setup(c => c.IndividualCustomers).ReturnsDbSet([individualCustomer]);

        mockContext.Setup(x => x.IndividualCustomers.Update(It.IsAny<IndividualCustomer>()));
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);


        var handler = new UpdateIndividualGeneralInformationRequest.UpdateIndividualGeneralInformationRequestHandler(mockContext.Object);

        var request = new UpdateIndividualGeneralInformationRequest
        {
            Id = 1,
            FatherName = "NewName",
            BirthDate = DateTime.Parse("1979/09/11"),
            ShPlaceId = 2,
            BirthPlaceId = 2,
            ShId = "987654321"
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_ShouldReturn404_WhenRecordNotFound()
    {
        // Arrange
        var mockContext = new Mock<DBContext>();
        mockContext.Setup(c => c.IndividualCustomers).ReturnsDbSet([]);
        var handler = new UpdateIndividualGeneralInformationRequest.UpdateIndividualGeneralInformationRequestHandler(mockContext.Object);

        var request = new UpdateIndividualGeneralInformationRequest
        {
            Id = 99, // Nonexistent ID
            FatherName = "NewName",
            BirthDate = DateTime.Parse("1979/09/11"),
            ShPlaceId = 2,
            BirthPlaceId = 2,
            ShId = "987654321"
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Code);
    }

    [Fact]
    public async Task Handle_ShouldReturn400_WhenSaveFails()
    {
        // Arrange
        var mockContext = new Mock<DBContext>();
        var individualCustomer = new IndividualCustomer
        {
            Id = 1,
            FatherName = "OldName",
            BirthDate = DateTime.Parse("1979/09/11"),
            ShPlaceId = 1,
            BirthPlaceId = 1,
            ShId = "123456789"
        };
        mockContext.Setup(c => c.IndividualCustomers).ReturnsDbSet([individualCustomer]);
        mockContext.Setup(x => x.IndividualCustomers.Update(It.IsAny<IndividualCustomer>()));
        mockContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                 .ReturnsAsync(0); // Simulate failure to save changes

        var handler = new UpdateIndividualGeneralInformationRequest.UpdateIndividualGeneralInformationRequestHandler(mockContext.Object);

        var request = new UpdateIndividualGeneralInformationRequest
        {
            Id = 1,
            FatherName = "NewName",
            BirthDate = DateTime.Parse("1979/09/11"),
            ShPlaceId = 2,
            BirthPlaceId = 2,
            ShId = "987654321"
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
    }
}

