using Application.Services.OtheracctchqcolatralService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.Otheracctchqcolatral;

public class OtheracctchqcolatralRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddOtheracctchqcolatralRequest_Success() =>
        Assert.NotNull(new AddOtheracctchqcolatralRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetOtheracctchqcolatralRequest_Success() =>
        Assert.NotNull(new GetOtheracctchqcolatralRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchOtheracctchqcolatralRequest_Success() =>
        Assert.NotNull(new SearchOtheracctchqcolatralRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateOtheracctchqcolatralRequest_Success() =>
        Assert.NotNull(new UpdateOtheracctchqcolatralRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteOtheracctchqcolatralRequest_Success() =>
        Assert.NotNull(new DeleteOtheracctchqcolatralRequestHandler(_unitOfWork.Object));
}
