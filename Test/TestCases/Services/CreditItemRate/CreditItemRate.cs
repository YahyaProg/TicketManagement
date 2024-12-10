
using Application.Services.CreditItemRateService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.CreditItemRate;


public class CreditItemRateRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddCreditItemRateRequest_Success() =>
        Assert.NotNull(new AddCreditItemRateRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteCreditItemRateRequest_Success() =>
        Assert.NotNull(new DeleteCreditItemRateRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetByIdCreditItemRateRequest_Success() =>
        Assert.NotNull(new GetCreditItemRateRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateCreditItemRateRequest_Success() =>
        Assert.NotNull(new UpdateCreditItemRateRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchCreditItemRateRequest_Success() =>
        Assert.NotNull(new SearchCreditItemRateRequestHandler(_unitOfWork.Object));

}