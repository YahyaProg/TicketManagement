using Application.Services.AccountModuleDescService;
using Application.Services.BranchOrganizationTypeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.BranchOrganizationType;

public class BranchOrganizationTypeRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddBranchOrganizationTypeRequest_Success() =>
        Assert.NotNull(new AddBranchOrganizationTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetBranchOrganizationTypeRequest_Success() =>
        Assert.NotNull(new GetBranchOrganizationTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchBranchOrganizationTypeRequest_Success() =>
        Assert.NotNull(new SearchBranchOrganizationTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownBranchOrganizationTypeRequest_Success() =>
        Assert.NotNull(new DropDownBranchOrganizationTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateBranchOrganizationTypeRequest_Success() =>
        Assert.NotNull(new UpdateBranchOrganizationTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteBranchOrganizationTypeRequest_Success() =>
        Assert.NotNull(new DeleteBranchOrganizationTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownBranchOrganizationTypeRequest()
    {
        var request = new DropDownBranchOrganizationTypeRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
