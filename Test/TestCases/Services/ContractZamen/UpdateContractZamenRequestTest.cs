using Application.Services.BaseService;
using Application.Services.ContractZamenService;
using Core.GenericResultModel;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.ContractZamen;

public class UpdateContractZamenRequestTest
{
    private readonly Mock<IMediator> mockMediator = new();
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task UpdateContractZamenRequest_Fail1()
    {
        moq.Context.Setup(x => x.ContractZamens).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new UpdateContractZamenRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateContractZamenRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateContractZamenRequest_Fail2()
    {
        moq.Context.Setup(x => x.ContractZamens).ReturnsDbSet([new() { Id = 1 }]);

        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(400, false) { Message = "a" });

        var handler = new UpdateContractZamenRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateContractZamenRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateContractZamenRequest_Fail3()
    {
        moq.Context.Setup(x => x.ContractZamens).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.ContractZamens.Update(It.IsAny<Core.Entities.ContractZamen>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        var handler = new UpdateContractZamenRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateContractZamenRequest { Id = 1, ContractCollateralId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateContractZamenRequest_Success()
    {
        moq.Context.Setup(x => x.ContractZamens).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.ContractZamens.Update(It.IsAny<Core.Entities.ContractZamen>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        var handler = new UpdateContractZamenRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateContractZamenRequest { Id = 1, ContractCollateralId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
