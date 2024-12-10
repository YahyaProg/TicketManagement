
using Application.Services.PorterItemService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.PorterItem;


public class PorterItemRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddPorterItemRequest_Success() =>
        Assert.NotNull(new AddPorterItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeletePorterItemRequest_Success() =>
        Assert.NotNull(new DeletePorterItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetByIdPorterItemRequest_Success() =>
        Assert.NotNull(new GetPorterItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdatePorterItemRequest_Success() =>
        Assert.NotNull(new UpdatePorterItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownPorterItemRequest_Success() =>
        Assert.NotNull(new DropDownPorterItemRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownPorterItemRequest()
    {
        var request = new DropDownPorterItemRequest()
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}