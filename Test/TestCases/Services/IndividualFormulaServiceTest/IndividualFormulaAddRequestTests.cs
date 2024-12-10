using Application.Services.IndividualFormulaService;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.IndividualFormulaServiceTest;


public class IndividualFormulaAddRequestHandlerTests
{
    [Fact]
    public async Task Handle_WhenCodeExistsInParams_ReturnsBadRequest()
    {
        // Arrange
        var mockDbContext = new Mock<DBContext>();
        var cancellationToken = CancellationToken.None;

        mockDbContext.Setup(c => c.IndividualParam).ReturnsDbSet([
               new(){Id=1,Code= "existing_code"},
            ]);

        var request = new IndividualFormulaAddRequest
        {
            Code = "existing_code",
            Formula = "a + b",
            FrontFormula = "a + b",
            Title = "Test Formula"
        };

        var handler = new IndividualFormulaAddRequestHandler(mockDbContext.Object);

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
        Assert.Equal("این کد در پارامتر ها وجود دارد", result.Message);
    }

    [Fact]
    public async Task Handle_WhenCodeExistsInFormulas_ReturnsBadRequest()
    {
        // Arrange
        var mockDbContext = new Mock<DBContext>();
        var cancellationToken = CancellationToken.None;

        mockDbContext.Setup(c => c.IndividualParam).ReturnsDbSet([
            new(){Id=1,Code= "new_code"},
            ]);

        mockDbContext.Setup(c => c.IndividualFormula).ReturnsDbSet([
            new(){Id=1,Code= "existing_code",Formula="a + b"},
            ]);

        var request = new IndividualFormulaAddRequest
        {
            Code = "existing_code",
            Formula = "a + b",
            FrontFormula = "a + b",
            Title = "Test Formula"
        };

        var handler = new IndividualFormulaAddRequestHandler(mockDbContext.Object);

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
        Assert.Equal("این کد در فرمول ها وجود دارد", result.Message);
    }

    [Fact]
    public async Task Handle_WhenNewCode_AddsFormulaAndRelations()
    {
        // Arrange
        var mockDbContext = new Mock<DBContext>();
        var cancellationToken = CancellationToken.None;

        mockDbContext.Setup(c => c.IndividualParam).ReturnsDbSet([
                new IndividualParam { Code = "a" },
                new IndividualParam { Code = "b" }
            ]);

        mockDbContext.Setup(c => c.IndividualFormula).ReturnsDbSet([
            new(){Id=1,Code= "c",Formula="a+b"},
            new(){Id=2,Code= "a",Formula="a+d"},
            ]);

        mockDbContext.Setup(x => x.IndividualRelations.AddRange(It.IsAny<List<IndividualRelations>>()));
        mockDbContext
            .Setup(x => x.SaveChangesAsync(cancellationToken)).ReturnsAsync(1);

        var request = new IndividualFormulaAddRequest
        {
            Code = "new_code",
            Formula = "a+b",
            FrontFormula = "a+b",
            Title = "New Formula"
        };

        var handler = new IndividualFormulaAddRequestHandler(mockDbContext.Object);

        // Act
        var result = await handler.Handle(request, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(200, result.Code);
        mockDbContext.Verify(x => x.IndividualFormula.Add(It.IsAny<IndividualFormula>()), Times.Once);
        mockDbContext.Verify(x => x.IndividualRelations.AddRange(It.IsAny<IEnumerable<IndividualRelations>>()), Times.Once);
        mockDbContext.Verify(x => x.SaveChangesAsync(cancellationToken), Times.Once);
    }


}