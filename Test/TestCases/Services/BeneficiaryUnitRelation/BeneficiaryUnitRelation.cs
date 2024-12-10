using Application.Services.BeneficiaryUnitRelationService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.BeneficiaryUnitRelationTest;

public class BeneficiaryUnitRelationRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddBeneficiaryUnitRelationRequest_Success() =>
        Assert.NotNull(new AddBeneficiaryUnitRelationRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetBeneficiaryUnitRelationRequest_Success() =>
        Assert.NotNull(new GetBeneficiaryUnitRelationRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchBeneficiaryUnitRelationRequest_Success() =>
        Assert.NotNull(new SearchBeneficiaryUnitRelationRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownBeneficiaryUnitRelationRequest_Success() =>
        Assert.NotNull(new DropDownBeneficiaryUnitRelationRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateBeneficiaryUnitRelationRequest_Success() =>
        Assert.NotNull(new UpdateBeneficiaryUnitRelationRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteBeneficiaryUnitRelationRequest_Success() =>
        Assert.NotNull(new DeleteBeneficiaryUnitRelationRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownBeneficiaryUnitRelationRequest()
    {
        var request = new DropDownBeneficiaryUnitRelationRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
