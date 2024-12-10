using Application.Services.AccountModuleDescService;
using Application.Services.CreditStatusService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.CreditStatus;

public class CreditStatusRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddCreditStatusRequest_Success() =>
        Assert.NotNull(new AddCreditStatusRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetCreditStatusRequest_Success() =>
        Assert.NotNull(new GetCreditStatusRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchCreditStatusRequest_Success() =>
        Assert.NotNull(new SearchCreditStatusRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownCreditStatusRequest_Success() =>
        Assert.NotNull(new DropDownCreditStatusRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateCreditStatusRequest_Success() =>
        Assert.NotNull(new UpdateCreditStatusRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteCreditStatusRequest_Success() =>
        Assert.NotNull(new DeleteCreditStatusRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownCreditStatusRequest()
    {
        var request = new DropDownCreditStatusRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
