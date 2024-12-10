using Application.Services.BaseService;
using Application.Services.Manager;
using Core.GenericResultModel;
using FluentValidation.TestHelper;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.Managers;

public class UpdateBoardMemberAndManagerRequestTest
{
    private readonly Mock<IMediator> mediator = new();

    [Fact]
    public async Task ValidationSuccess()
    {
        var request = new UpdateBoardMemberAndManagerRequest { CorporateAgent = false };

        var validator = new UpdateBoardMemberAndManagerValidation();

        var res = await validator.TestValidateAsync(request);

        res.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task UpdateBoardMemberAndManager_Fail1()
    {
        var moq = GetUnitOfWorkMoqCollection();

        moq.UnitOfWork.Setup(x => x.Context.Managers).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new UpdateBoardMemberAndManagerRequestHandler(moq.UnitOfWork.Object, mediator.Object);

        var request = new UpdateBoardMemberAndManagerRequest { ManagerId = 2 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateBoardMemberAndManager_Fail2()
    {
        var moq = GetUnitOfWorkMoqCollection();

        moq.UnitOfWork.Setup(x => x.Context.Managers).ReturnsDbSet([new() { Id = 1, CorporateAgent = false }]);
        mediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None))
            .ReturnsAsync(new ApiResult<long>(400,false));

        var handler = new UpdateBoardMemberAndManagerRequestHandler(moq.UnitOfWork.Object, mediator.Object);

        var request = new UpdateBoardMemberAndManagerRequest { ManagerId = 1, CorporateAgent = true };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateBoardMemberAndManager_Fail3()
    {
        var moq = GetUnitOfWorkMoqCollection();

        moq.UnitOfWork.Setup(x => x.Context.Managers).ReturnsDbSet([new() { Id = 1, CorporateAgent = false }]);
        mediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>
            {
                IsSuccess = true,
                Data = 1
            });
        moq.UnitOfWork.Setup(x => x.Context.Managers.Update(It.IsAny<Core.Entities.Manager>()));
        moq.UnitOfWork.Setup(x => x.Context.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new UpdateBoardMemberAndManagerRequestHandler(moq.UnitOfWork.Object, mediator.Object);

        var request = new UpdateBoardMemberAndManagerRequest { ManagerId = 1, CorporateAgent = true };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateBoardMemberAndManager_Success()
    {
        var moq = GetUnitOfWorkMoqCollection();

        moq.UnitOfWork.Setup(x => x.Context.Managers).ReturnsDbSet([new() { Id = 1, CorporateAgent = false }]);
        mediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(
            new ApiResult<long>
            {
                IsSuccess = true,
                Data = 1
            });
        moq.UnitOfWork.Setup(x => x.Context.Managers.Update(It.IsAny<Core.Entities.Manager>()));
        moq.UnitOfWork.Setup(x => x.Context.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new UpdateBoardMemberAndManagerRequestHandler(moq.UnitOfWork.Object, mediator.Object);

        var request = new UpdateBoardMemberAndManagerRequest { ManagerId = 1, CorporateAgent = true };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
