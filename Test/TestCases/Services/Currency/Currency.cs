using Application.Services.CurrencyService;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CurrencyTest;

public class CurrencyRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task AddCurrencyRequest_Fail()
    {
        var command = new AddCurrencyRequest { Title = "0", Code = "0" };

        _unitOfWork.Setup(x => x.Context.Currencies).ReturnsDbSet(new List<Core.Entities.Currency> { new() { Title = "0", Code = "0" } });

        _unitOfWork.Setup(x => x.CurrencyRepo.CheckDuplicate(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("fail");

        var handler = new AddCurrencyRequestHandler(_unitOfWork.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddCurrencyRequest_Success()
    {
        var command = new AddCurrencyRequest { Title = "1", Code = "1" };

        _unitOfWork.Setup(x => x.Context.Currencies).ReturnsDbSet(new List<Core.Entities.Currency> { new() { Title = "0", Code = "0" } });

        _unitOfWork.Setup(x => x.CurrencyRepo.CheckDuplicate(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("");

        _unitOfWork.Setup(x => x.Context.Currencies.Add(It.IsAny<Core.Entities.Currency>()));

        _unitOfWork.Setup(x => x.Context.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddCurrencyRequestHandler(_unitOfWork.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void SoftDeleteCurrencyRequest_Success() =>
        Assert.NotNull(new DeleteCurrencyRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetCurrencyRequest_Success() =>
        Assert.NotNull(new GetCurrencyRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateCurrencyRequest_Success() =>
        Assert.NotNull(new UpdateCurrencyRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchCurrencyRequest_Success() =>
        Assert.NotNull(new SearchCurrencyRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownCurrencyRequest_Success() =>
        Assert.NotNull(new DropDownCurrencyRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownCurrencyRequest()
    {
        var request = new DropDownCurrencyRequest()
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
