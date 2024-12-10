using Application.Services.AccountModuleDescService;
using Application.Services.LoanStatusService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.LoanStatus;

public class LoanStatusRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddLoanStatusRequest_Success() =>
        Assert.NotNull(new AddLoanStatusRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetLoanStatusRequest_Success() =>
        Assert.NotNull(new GetLoanStatusRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchLoanStatusRequest_Success() =>
        Assert.NotNull(new SearchLoanStatusRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownLoanStatusRequest_Success() =>
        Assert.NotNull(new DropDownLoanStatusRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateLoanStatusRequest_Success() =>
        Assert.NotNull(new UpdateLoanStatusRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteLoanStatusRequest_Success() =>
        Assert.NotNull(new DeleteLoanStatusRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownLoanStatusRequest()
    {
        var request = new DropDownLoanStatusRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
