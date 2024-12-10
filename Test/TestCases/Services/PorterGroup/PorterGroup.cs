using Application.Services.PorterGroupService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.PorterGroup;

public class PorterGroupRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<DBContext> _dbContext = new();
    [Fact]
    public void AddPorterGroupRequest_Success() =>
        Assert.NotNull(new AddPorterGroupRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetPorterGroupRequest_Success() =>
        Assert.NotNull(new GetPorterGroupRequestHandler(_dbContext.Object));

    [Fact]
    public void SearchPorterGroupRequest_Success() =>
        Assert.NotNull(new SearchPorterGroupRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownPorterGroupRequest_Success() =>
        Assert.NotNull(new DropDownPorterGroupRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdatePorterGroupRequest_Success() =>
        Assert.NotNull(new UpdatePorterGroupRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeletePorterGroupRequest_Success() =>
        Assert.NotNull(new DeletePorterGroupRequestHandler(_dbContext.Object));

    [Fact]
    public void DropDownPorterGroupRequest()
    {
        var request = new DropDownPorterGroupRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
