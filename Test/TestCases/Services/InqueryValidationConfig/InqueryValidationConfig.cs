using Application.Services.InqueryValidationConfigService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.InqueryValidationConfig;

public class InqueryValidationConfigRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddInqueryValidationConfigRequest_Success() =>
        Assert.NotNull(new AddInqueryValidationConfigRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetInqueryValidationConfigRequest_Success() =>
        Assert.NotNull(new GetInqueryValidationConfigRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchInqueryValidationConfigRequest_Success() =>
        Assert.NotNull(new SearchInqueryValidationConfigRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateInqueryValidationConfigRequest_Success() =>
        Assert.NotNull(new UpdateInqueryValidationConfigRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteInqueryValidationConfigRequest_Success() =>
        Assert.NotNull(new DeleteInqueryValidationConfigRequestHandler(_unitOfWork.Object));
}
