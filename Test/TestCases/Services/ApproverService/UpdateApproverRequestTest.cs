using Application.Services.ApproverService;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.ApproverServiceTest;

public class UpdateApproverRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task UpdateApproverRequest_Fail1()
    {
        moq.Context.Setup(x => x.Approvers).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new UpdateApproverRequestHandler(moq.Context.Object);

        var request = new UpdateApproverRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateApproverRequest_Fail2()
    {
        moq.Context.Setup(x => x.Approvers).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.BankStaffs).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new UpdateApproverRequestHandler(moq.Context.Object);

        var request = new UpdateApproverRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateApproverRequest_Fail3()
    {
        moq.Context.Setup(x => x.Approvers).ReturnsDbSet([new() { Id = 1 }, new() { Id = 2, Title = "a b" }]);
        moq.Context.Setup(x => x.BankStaffs).ReturnsDbSet([new() { Id = 1, FirstName = "a", LastName = "b" }]);

        var handler = new UpdateApproverRequestHandler(moq.Context.Object);

        var request = new UpdateApproverRequest { Id = 1, BankStaffId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateApproverRequest_Fail4()
    {
        moq.Context.Setup(x => x.Approvers).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.BankStaffs).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.Approvers.Update(It.IsAny<Core.Entities.Approver>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new UpdateApproverRequestHandler(moq.Context.Object);

        var request = new UpdateApproverRequest { Id = 1, BankStaffId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateApproverRequest_Success()
    {
        moq.Context.Setup(x => x.Approvers).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.BankStaffs).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.Approvers.Update(It.IsAny<Core.Entities.Approver>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new UpdateApproverRequestHandler(moq.Context.Object);

        var request = new UpdateApproverRequest { Id = 1, BankStaffId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
