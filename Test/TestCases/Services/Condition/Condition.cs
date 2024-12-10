
using Application.Services.ConditionService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.Condition;


public class ConditionRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<DBContext> _context = new();

    [Fact]
    public void AddConditionRequest_Success() =>
        Assert.NotNull(new AddConditionRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteConditionRequest_Success() =>
        Assert.NotNull(new DeleteConditionRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateConditionRequest_Success() =>
        Assert.NotNull(new UpdateConditionRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchConditionRequest_Success() =>
        Assert.NotNull(new SearchConditionRequestHandler(_context.Object));
}