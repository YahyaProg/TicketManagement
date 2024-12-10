using Application.Services.WebServiceService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.WebService;


public class WebServiceRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddWebServiceRequest_Success() =>
        Assert.NotNull(new AddWebServiceRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteWebServiceRequest_Success() =>
        Assert.NotNull(new DeleteWebServiceRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetByIdWebServiceRequest_Success() =>
        Assert.NotNull(new GetWebServiceRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateWebServiceRequest_Success() =>
        Assert.NotNull(new UpdateWebServiceRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchWebServiceRequest_Success() =>
        Assert.NotNull(new SearchWebServiceRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDowndWebServiceRequest_Success() =>
        Assert.NotNull(new DropDownWebServiceRequestHandler(_unitOfWork.Object));
    [Fact]
    public void DropDownWebServiceRequest()
    {
        var request = new DropDownWebServiceRequest()
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}