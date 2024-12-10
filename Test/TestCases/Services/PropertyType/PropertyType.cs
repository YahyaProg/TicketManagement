using Application.Services.AccountModuleDescService;
using Application.Services.PropertyTypeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.PropertyType;

public class PropertyTypeRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddPropertyTypeRequest_Success() =>
        Assert.NotNull(new AddPropertyTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetPropertyTypeRequest_Success() =>
        Assert.NotNull(new GetPropertyTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchPropertyTypeRequest_Success() =>
        Assert.NotNull(new SearchPropertyTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownPropertyTypeRequest_Success() =>
        Assert.NotNull(new DropDownPropertyTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdatePropertyTypeRequest_Success() =>
        Assert.NotNull(new UpdatePropertyTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeletePropertyTypeRequest_Success() =>
        Assert.NotNull(new DeletePropertyTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownPropertyTypeRequest()
    {
        var request = new DropDownPropertyTypeRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
