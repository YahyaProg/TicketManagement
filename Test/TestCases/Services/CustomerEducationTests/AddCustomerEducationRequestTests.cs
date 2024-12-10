using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services.CustomerEducationService;
using Core.Entities;
using Core.GenericResultModel;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CustomerEducationTests;

public class AddCustomerEducationRequestHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_AddsEducation_ReturnsSuccess()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();

        dbContextMock.Setup(c => c.Educations).ReturnsDbSet([]);
        dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new AddCustomerEducationRequestHandler(dbContextMock.Object);
        var request = new AddCustomerEducationRequest
        {
            Edate = new DateTime(2023, 1, 1),
            EducationPlace = "Test University",
            EducationTypeId = 1,
            CustomerId = 1001,
            Description = "Graduated with honors"
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_SaveChangesFails_ReturnsBadRequest()
    {
        // Arrange
        var dbContextMock = new Mock<DBContext>();

        dbContextMock.Setup(c => c.Educations).ReturnsDbSet([]);
        dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0); // Simulate save failure

        var handler = new AddCustomerEducationRequestHandler(dbContextMock.Object);
        var request = new AddCustomerEducationRequest
        {
            Edate = new DateTime(2023, 1, 1),
            EducationPlace = "Test University",
            EducationTypeId = 1,
            CustomerId = 1001,
            Description = "Graduated with honors"
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
    }
}