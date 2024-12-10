using Application.Services.WorkPlaceTypeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.WorkPlaceTypeTest;

public class WorkPlaceTypeRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void GetWorkPlaceTypeRequest_Success() =>
        Assert.NotNull(new GetWorkPlaceTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchWorkPlaceTypeRequest_Success() =>
        Assert.NotNull(new SearchWorkPlaceTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownWorkPlaceTypeRequest_Success() =>
        Assert.NotNull(new DropDownWorkPlaceTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteWorkPlaceTypeRequest_Success() =>
        Assert.NotNull(new DeleteWorkPlaceTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownWorkPlaceTypeRequest()
    {
        var request = new DropDownWorkPlaceTypeRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
