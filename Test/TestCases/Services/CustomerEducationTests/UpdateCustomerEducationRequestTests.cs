using Application.Services.CustomerEducationService;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CustomerEducationTests;


public class UpdateCustomerEducationRequestHandlerTests
{
    [Fact]
    public async Task Handle_CustomerEducationExists_ReturnsSuccess()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();
        var fakeEducation = new Education
        {
            Id = 1,
            Edate = DateTime.UtcNow,
            Description = "Old Description",
            EducationPlace = "Old Place",
            EducationTypeId = 1
        };

        dbContextMock.Setup(c => c.Educations).ReturnsDbSet([fakeEducation]);

        dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new UpdateCustomerPhoneRequestHandler(dbContextMock.Object);
        var request = new UpdateCustomerEducationRequest
        {
            Id = 1,
            Edate = DateTime.UtcNow.AddDays(1),
            Description = "Updated Description",
            EducationPlace = "Updated Place",
            EducationTypeId = 2
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_CustomerEducationNotFound_ReturnsNotFound()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();
        dbContextMock.Setup(c => c.Educations).ReturnsDbSet([]);

        var handler = new UpdateCustomerPhoneRequestHandler(dbContextMock.Object);
        var request = new UpdateCustomerEducationRequest
        {
            Id = 99, // Non-existent ID
            Edate = DateTime.UtcNow,
            Description = "Test Description",
            EducationPlace = "Test Place",
            EducationTypeId = 1
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Code);
    }

    [Fact]
    public async Task Handle_SaveChangesFails_ReturnsBadRequest()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();
        var fakeEducation = new Education
        {
            Id = 1,
            Edate = DateTime.UtcNow,
            Description = "Old Description",
            EducationPlace = "Old Place",
            EducationTypeId = 1
        };

        dbContextMock.Setup(c => c.Educations).ReturnsDbSet([fakeEducation]);
        dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

        var handler = new UpdateCustomerPhoneRequestHandler(dbContextMock.Object);
        var request = new UpdateCustomerEducationRequest
        {
            Id = 1,
            Edate = DateTime.UtcNow,
            Description = "Updated Description",
            EducationPlace = "Updated Place",
            EducationTypeId = 2
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
    }
}