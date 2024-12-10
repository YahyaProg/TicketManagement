using Application.Services.ContractZamenService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.ContractZamen;

public class ContractZamenRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void DeleteContractZamenRequest_Success() =>
        Assert.NotNull(new DeleteContractZamenRequestHandler(_unitOfWork.Object));
}
