using Application.Services.CompanyFinancialInfoService;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CompanyFinancialTests;

public class CrudTests
{
    private readonly Mock<IUnitOfWork> unitOfWork = new();
    private readonly Mock<DBContext> context = new();
    private UpdateCompanyFinancialInfoRequest request = new UpdateCompanyFinancialInfoRequest()
    {
        Code = "reza",
        Indx = 1,
        ParentId = 1,
        Sign = Core.Enums.ECompanyFinancialInfo_sign.minus_one,
        SubType = Core.Enums.ECompanyFinancialInfo_subType.header,
        Title = "test",
        Type = Core.Enums.ECompanyFinancialInfo_type.income_statement
    };

    [Fact]
    public void Add()
        => Assert.NotNull(new AddCompanyFinancialInfoRequestHandler(unitOfWork.Object));

    [Fact]
    public void Get()
        => Assert.NotNull(new GetCompanyFinancialInfoRequestHandler(unitOfWork.Object));

    [Fact]
    public void Delete()
        => Assert.NotNull(new DeleteCompanyFinancialInfoRequestHandler(unitOfWork.Object));

    [Fact]
    public void DropDown()
        => Assert.NotNull(new DropDownCompanyFinancialInfoRequestHandler(unitOfWork.Object));

    [Theory]
    [InlineData(1,true)]
    [InlineData(2,false)]
    public async Task Update(long id,bool expected)
    {
        request.Id = id;
        context.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet([
            new(){
                Id = 1,
                Code = "test",
                Sign = Core.Enums.ECompanyFinancialInfo_sign.one,
            }
            ]);

        context.Setup(x => x.RelCalFinancialInfo_CompanyFinancialInfos).ReturnsDbSet([
            new(){
                CompanyFinancialInfoId = 1,
            }
            ]);

        context.Setup(x => x.CalcFinancialInfos).ReturnsDbSet([
            new(){
                Formula = "test"
            }]);

        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

        var handler = new UpdateCompanyFinancialInfoRequestHandler(context.Object);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.Equal(expected, res.IsSuccess);
    }
}
