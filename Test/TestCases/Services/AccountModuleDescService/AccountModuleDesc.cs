using Application.Services.AccountModuleDescService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.AccountModuleDescTest;

public class AccountModuleDescRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddAccountModuleDescRequest_Success() =>
        Assert.NotNull(new AddAccountModuleDescRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetAccountModuleDescRequest_Success() =>
        Assert.NotNull(new GetAccountModuleDescRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchAccountModuleDescRequest_Success() =>
        Assert.NotNull(new SearchAccountModuleDescRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownAccountModuleDescRequest_Success() =>
        Assert.NotNull(new DropDownAccountModuleDescRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateAccountModuleDescRequest_Success() =>
        Assert.NotNull(new UpdateAccountModuleDescRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteAccountModuleDescRequest_Success() =>
        Assert.NotNull(new DeleteAccountModuleDescRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownAccountModuleDescRequest()
    {
        var request = new DropDownAccountModuleDescRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
