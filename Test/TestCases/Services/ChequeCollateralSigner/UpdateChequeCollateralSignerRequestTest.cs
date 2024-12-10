﻿using Application.Services.BaseService;
using Application.Services.ChequeCollateralSignerService;
using Core.GenericResultModel;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.ChequeCollateralSigner;

public class UpdateChequeCollateralSignerRequestTest
{
    private readonly Mock<IMediator> mockMediator = new();
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task UpdateChequeCollateralSignerRequest_Fail1()
    {
        moq.Context.Setup(x => x.ChequeCollateralSigners).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new UpdateChequeCollateralSignerRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateChequeCollateralSignerRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateChequeCollateralSignerRequest_Fail2()
    {
        moq.Context.Setup(x => x.ChequeCollateralSigners).ReturnsDbSet([new() { Id = 1 }]);

        mockMediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(400, false) { Message = "a" });

        var handler = new UpdateChequeCollateralSignerRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateChequeCollateralSignerRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateChequeCollateralSignerRequest_Fail3()
    {
        moq.Context.Setup(x => x.ChequeCollateralSigners).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.ChequeCollateralSigners.Update(It.IsAny<Core.Entities.ChequeCollateralSigner>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        mockMediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        var handler = new UpdateChequeCollateralSignerRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateChequeCollateralSignerRequest { Id = 1, ChequeCollateralId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateChequeCollateralSignerRequest_Success()
    {
        moq.Context.Setup(x => x.ChequeCollateralSigners).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.ChequeCollateralSigners.Update(It.IsAny<Core.Entities.ChequeCollateralSigner>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        mockMediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>(200, true) { Data = 1 });

        var handler = new UpdateChequeCollateralSignerRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateChequeCollateralSignerRequest { Id = 1, ChequeCollateralId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}