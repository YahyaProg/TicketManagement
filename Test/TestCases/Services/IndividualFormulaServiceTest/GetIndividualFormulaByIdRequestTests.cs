using Application.Services.IndividualFormulaService;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.IndividualFormulaServiceTest;



public class GetIndividualFormulaByIdRequestHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnFormula_WhenFormulaExists()
    {
        // Arrange
        var mockContext = new Mock<DBContext>();
        var formulaId = 1L;
        var formula = new IndividualFormula { Id = formulaId, Title = "Test Formula" };


        mockContext.Setup(c => c.IndividualFormula).ReturnsDbSet([formula]);

        var handler = new GetIndividualFormulaByIdRequestHandler(mockContext.Object);
        var request = new GetIndividualFormulaByIdRequest { Id = formulaId };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(formulaId, result.Data.Id);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenFormulaDoesNotExist()
    {
        // Arrange
        var mockContext = new Mock<DBContext>();
        var formulaId = 1L;


        mockContext.Setup(c => c.IndividualFormula).ReturnsDbSet([]);

        var handler = new GetIndividualFormulaByIdRequestHandler(mockContext.Object);
        var request = new GetIndividualFormulaByIdRequest { Id = formulaId };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
        Assert.Equal(404, result.Code);
    }
}