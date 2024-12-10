using Application.Services.CalcFinancialService;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services;

public class CalcFinancialCrudTests
{
    private readonly Mock<DBContext> context = new();
    private readonly Mock<IUnitOfWork> unitOfWork = new();

    public CalcFinancialCrudTests()
    {
        context.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet([new() { Code = "ajab", Id = 1, Sign = Core.Enums.ECompanyFinancialInfo_sign.minus_one }]);
        context.Setup(x => x.RelCalFinancialInfo_CompanyFinancialInfos).ReturnsDbSet([]);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(0, false)]
    public async Task Add(int result, bool expected)
    {
        var request = new AddCalcFinancialRequest()
        {
            Code = "reza",
            Formula = "ajab*2"
        };

        context.Setup(x => x.CalcFinancialInfos).ReturnsDbSet([]);

        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(result);

        var handler = new AddCalcFinancialRequestHandler(context.Object);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.Equal(expected, res.IsSuccess);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(0, false)]
    public async Task FillRelations(int result, bool expected)
    {
        var request = new FillCalcFinancialRelationRequest();

        context.Setup(x => x.CalcFinancialInfos).ReturnsDbSet([]);

        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(result);

        var handler = new FillCalcFinancialRelationRequestHandler(context.Object);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.Equal(expected, res.IsSuccess);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(0, false)]
    public async Task Delete(int result, bool expected)
    {
        var request = new DeleteCalcFinancialRequest()
        {
            Id = 1,
        };

        context.Setup(x => x.CalcFinancialInfos).ReturnsDbSet([]);

        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(result);

        var handler = new DeleteCalcFinancialRequestHandler(context.Object);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.Equal(expected, res.IsSuccess);
    }

    [Theory]
    [InlineData(1, 1, true)]
    [InlineData(0, 1, false)]
    [InlineData(0, 2, false)]
    public async Task Update(int result, long id, bool expected)
    {
        var request = new UpdateCalcFinancialRequest()
        {
            Id = id,
            Code = "reza",
            Formula = "ajab*2",
            Percent = true,
            Indx = 1,
            NeedCurrencyCalc = true,
            Title = "Title",
            Type = Core.Enums.ECalcFinancialInfo_type.estekhraj
        };

        context.Setup(x => x.CalcFinancialInfos).ReturnsDbSet([new() { Id = id }]);

        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(result);

        var handler = new UpdateCalcFinancialRequestHandler(context.Object);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.Equal(expected, res.IsSuccess);
    }

    [Fact]
    public void Search()
        => Assert.NotNull(new SearchCalcFinancialRequestHandler(unitOfWork.Object));
}
