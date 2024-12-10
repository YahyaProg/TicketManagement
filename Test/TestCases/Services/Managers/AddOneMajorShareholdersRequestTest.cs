using Application.Services.BaseService;
using Application.Services.Manager;
using Core.GenericResultModel;
using MediatR;
using Moq;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.Managers;

public class AddOneMajorShareholdersRequestTest
{
    private readonly Mock<IMediator> mockMediator = new();
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task AddOneMajorShareholdersRequest_Fail1()
    {
        moq.UnitOfWork.Setup(x => x.CompanyMembersInfoRepo.SumOfStocksCount(It.IsAny<long>())).ReturnsAsync(0);

        var handler = new AddOneMajorShareholdersRequestHandler(moq.UnitOfWork.Object, mockMediator.Object);

        var request = new AddOneMajorShareholdersRequest { StocksCount = 101, ProposalSchemeId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddOneMajorShareholdersRequest_Fail2()
    {
        moq.UnitOfWork.Setup(x => x.CompanyMembersInfoRepo.SumOfStocksCount(It.IsAny<long>())).ReturnsAsync(0);
        moq.UnitOfWork.Setup(x => x.CustomerSchemeRepo.getCustomerSchemeByProposalSchemeId(It.IsAny<long>()));

        var handler = new AddOneMajorShareholdersRequestHandler(moq.UnitOfWork.Object, mockMediator.Object);

        var request = new AddOneMajorShareholdersRequest { ProposalSchemeId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddOneMajorShareholdersRequest_Fail3()
    {
        moq.UnitOfWork.Setup(x => x.CompanyMembersInfoRepo.SumOfStocksCount(It.IsAny<long>())).ReturnsAsync(0);
        moq.UnitOfWork.Setup(x => x.CustomerSchemeRepo.getCustomerSchemeByProposalSchemeId(It.IsAny<long>())).ReturnsAsync(
            new Core.Entities.CustomerScheme { Id = 1 });
        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(400, false) { Message = "a" });

        var handler = new AddOneMajorShareholdersRequestHandler(moq.UnitOfWork.Object, mockMediator.Object);

        var request = new AddOneMajorShareholdersRequest { ProposalSchemeId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddOneMajorShareholdersRequest_Fail4()
    {
        moq.UnitOfWork.Setup(x => x.CompanyMembersInfoRepo.SumOfStocksCount(It.IsAny<long>())).ReturnsAsync(0);
        moq.UnitOfWork.Setup(x => x.CustomerSchemeRepo.getCustomerSchemeByProposalSchemeId(It.IsAny<long>())).ReturnsAsync(
            new Core.Entities.CustomerScheme { Id = 1 });
        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>());
        moq.UnitOfWork.Setup(x => x.Context.MajorStocksHolders.Add(It.IsAny<Core.Entities.MajorStocksHolder>()));
        moq.UnitOfWork.Setup(x => x.Context.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new AddOneMajorShareholdersRequestHandler(moq.UnitOfWork.Object, mockMediator.Object);

        var request = new AddOneMajorShareholdersRequest { ProposalSchemeId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddOneMajorShareholdersRequest_Success()
    {
        moq.UnitOfWork.Setup(x => x.CompanyMembersInfoRepo.SumOfStocksCount(It.IsAny<long>())).ReturnsAsync(0);
        moq.UnitOfWork.Setup(x => x.CustomerSchemeRepo.getCustomerSchemeByProposalSchemeId(It.IsAny<long>())).ReturnsAsync(
            new Core.Entities.CustomerScheme { Id = 1 });
        mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>());
        moq.UnitOfWork.Setup(x => x.Context.MajorStocksHolders.Add(It.IsAny<Core.Entities.MajorStocksHolder>()));
        moq.UnitOfWork.Setup(x => x.Context.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddOneMajorShareholdersRequestHandler(moq.UnitOfWork.Object, mockMediator.Object);

        var request = new AddOneMajorShareholdersRequest { ProposalSchemeId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
