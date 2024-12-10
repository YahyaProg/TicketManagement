using Application.Services.Inquery186resultService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.Inquery186result;
public class Inquery186resultServiceTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddInquery186resultRequest_Success() =>
        Assert.NotNull(new AddInquery186ResultRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SoftDeleteInquery186resultRequest_Success() =>
        Assert.NotNull(new DeleteInquery186ResultRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetInquery186resultRequest_Success() =>
        Assert.NotNull(new GetInquery186ResultRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateInquery186resultRequest_Success() =>
        Assert.NotNull(new UpdateInquery186ResultRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchInquery186resultRequest_Success() =>
        Assert.NotNull(new SearchInquery186ResultRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownInquery186resultRequest_Success() =>
        Assert.NotNull(new DropDownInquery186ResultRequestHandler(_unitOfWork.Object));
}
