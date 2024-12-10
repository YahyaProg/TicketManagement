
using Application.Services.CmntService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.Cmnt;


public class CmntRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddCmntRequest_Success() =>
        Assert.NotNull(new AddCmntRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteCmntRequest_Success() =>
        Assert.NotNull(new DeleteCmntRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetByIdCmntRequest_Success() =>
        Assert.NotNull(new GetCmntRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateCmntRequest_Success() =>
        Assert.NotNull(new UpdateCmntRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchCmntRequest_Success() =>
        Assert.NotNull(new SearchCmntRequestHandler(_unitOfWork.Object));



}