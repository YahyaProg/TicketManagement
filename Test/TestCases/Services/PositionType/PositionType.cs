using Application.Services.PositionTypeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.PositionType;

public class PositionTypeRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddPositionTypeRequest_Success() =>
        Assert.NotNull(new AddPositionTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetPositionTypeRequest_Success() =>
        Assert.NotNull(new GetPositionTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchPositionTypeRequest_Success() =>
        Assert.NotNull(new SearchPositionTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownPositionTypeRequest_Success() =>
        Assert.NotNull(new DropDownPositionTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdatePositionTypeRequest_Success() =>
        Assert.NotNull(new UpdatePositionTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeletePositionTypeRequest_Success() =>
        Assert.NotNull(new DeletePositionTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownPositionTypeRequest()
    {
        var request = new DropDownPositionTypeRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
