using Application.Services.SchemeTitleService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.SchemeTitle;

public class SchemeTitleRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddSchemeTitleRequest_Success() =>
        Assert.NotNull(new AddSchemeTitleRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetSchemeTitleRequest_Success() =>
        Assert.NotNull(new GetSchemeTitleRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchSchemeTitleRequest_Success() =>
        Assert.NotNull(new SearchSchemeTitleRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownSchemeTitleRequest_Success() =>
        Assert.NotNull(new DropDownSchemeTitleRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateSchemeTitleRequest_Success() =>
        Assert.NotNull(new UpdateSchemeTitleRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteSchemeTitleRequest_Success() =>
        Assert.NotNull(new DeleteSchemeTitleRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownSchemeTitleRequest()
    {
        var request = new DropDownSchemeTitleRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
