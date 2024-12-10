using Application.Services.IndividualFormulaService;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.IndividualFormulaServiceTest;

public class IndividualFormulaSearchRequestHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPaginatedList_WhenMatchingFormulasExist()
    {
        // Arrange
        var mockContext = new Mock<DBContext>();
        var formulas = new List<IndividualFormula>
        {
            new IndividualFormula { Id = 1, Title = "Formula 1" },
            new IndividualFormula { Id = 2, Title = "Formula 2" }
        }.AsQueryable();

        mockContext.Setup(c => c.IndividualFormula).ReturnsDbSet(formulas);

        var handler = new IndividualFormulaSearchRequestHandler(mockContext.Object);
        var request = new IndividualFormulaSearchRequest
        {
            Page = 1,
            Size = 10
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data.TotalItems); // Assuming 2 matching formulas
    }

}