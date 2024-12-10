
using Application.Services.SecurityCodeMappingService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.SecurityCodeMapping;


public class SecurityCodeMappingRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddSecurityCodeMappingRequest_Success() =>
        Assert.NotNull(new AddSecurityCodeMappingRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteSecurityCodeMappingRequest_Success() =>
        Assert.NotNull(new DeleteSecurityCodeMappingRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetByIdSecurityCodeMappingRequest_Success() =>
        Assert.NotNull(new GetSecurityCodeMappingRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateSecurityCodeMappingRequest_Success() =>
        Assert.NotNull(new UpdateSecurityCodeMappingRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchSecurityCodeMappingRequest_Success() =>
        Assert.NotNull(new SearchSecurityCodeMappingRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownSecurityCodeMappingRequest_Success() =>
        Assert.NotNull(new DropDownSecurityCodeMappingRequestHandler(_unitOfWork.Object));
    [Fact]
    public void DropDownCompanyRelationRequest()
    {
        var request = new DropDownSecurityCodeMappingRequest()
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}