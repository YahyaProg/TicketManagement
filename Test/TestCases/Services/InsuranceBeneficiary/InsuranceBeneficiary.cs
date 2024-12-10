using Application.Services.InsuranceBeneficiaryService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.InsuranceBeneficiary;
public class InsuranceBeneficiaryServiceTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddInsuranceBeneficiaryRequest_Success() =>
        Assert.NotNull(new AddInsuranceBeneficiaryRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SoftDeleteInsuranceBeneficiaryRequest_Success() =>
        Assert.NotNull(new DeleteInsuranceBeneficiaryRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetInsuranceBeneficiaryRequest_Success() =>
        Assert.NotNull(new GetInsuranceBeneficiaryRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateInsuranceBeneficiaryRequest_Success() =>
        Assert.NotNull(new UpdateInsuranceBeneficiaryRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchInsuranceBeneficiaryRequest_Success() =>
        Assert.NotNull(new SearchInsuranceBeneficiaryRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownInsuranceBeneficiaryRequest_Success() =>
        Assert.NotNull(new DropDownInsuranceBeneficiaryRequestHandler(_unitOfWork.Object));
}
