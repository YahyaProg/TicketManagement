
using Application.Services.FinancialYearInfoService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.FinancialYearInfoTest;


public class FinancialYearInfoRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddFinancialYearInfoRequest_Success() =>
        Assert.NotNull(new AddFinancialYearInfoRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteFinancialYearInfoRequest_Success() =>
        Assert.NotNull(new DeleteFinancialYearInfoRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetByIdFinancialYearInfoRequest_Success() =>
        Assert.NotNull(new GetFinancialYearInfoRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateFinancialYearInfoRequest_Success() =>
        Assert.NotNull(new UpdateFinancialYearInfoRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchFinancialYearInfoRequest_Success() =>
        Assert.NotNull(new SearchFinancialYearInfoRequestHandler(_unitOfWork.Object));
}