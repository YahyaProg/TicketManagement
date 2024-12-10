using Application.Services.AccountModuleDescService;
using Application.Services.CompanyTypeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.CompanyType;

public class CompanyTypeRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddCompanyTypeRequest_Success() =>
        Assert.NotNull(new AddCompanyTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetCompanyTypeRequest_Success() =>
        Assert.NotNull(new GetCompanyTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchCompanyTypeRequest_Success() =>
        Assert.NotNull(new SearchCompanyTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownCompanyTypeRequest_Success() =>
        Assert.NotNull(new DropDownCompanyTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateCompanyTypeRequest_Success() =>
        Assert.NotNull(new UpdateCompanyTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteCompanyTypeRequest_Success() =>
        Assert.NotNull(new DeleteCompanyTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownCompanyTypeRequest()
    {
        var request = new DropDownCompanyTypeRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
