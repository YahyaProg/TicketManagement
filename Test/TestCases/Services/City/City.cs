using Application.Services.CityService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.CityTest;

public class CityRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddCityRequest_Success() =>
        Assert.NotNull(new AddCityRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetCityRequest_Success() =>
        Assert.NotNull(new GetCityRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownCityRequest_Success() =>
        Assert.NotNull(new DropDownCityRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateCityRequest_Success() =>
        Assert.NotNull(new UpdateCityRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteCityRequest_Success() =>
        Assert.NotNull(new DeleteCityRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownCityRequest()
    {
        var request = new DropDownCityRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0,
            ProvinceId = 1,
        };

        Assert.NotNull(request);
    }
}
