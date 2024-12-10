using Application.Services.ApproverService;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.ApproverServiceTest;

public class AddApproverRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task AddApproverRequest_Fail1()
    {
        moq.Context.Setup(x => x.BankStaffs).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new AddApproverRequestHandler(moq.Context.Object);

        var request = new AddApproverRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddApproverRequest_Fail2()
    {
        moq.Context.Setup(x => x.BankStaffs).ReturnsDbSet([new() { Id = 1, FirstName = "a", LastName = "b" }]);
        moq.Context.Setup(x => x.Approvers).ReturnsDbSet([new() { Title = "a b" }]);

        var handler = new AddApproverRequestHandler(moq.Context.Object);

        var request = new AddApproverRequest { BankStaffId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddApproverRequest_Fail3()
    {
        moq.Context.Setup(x => x.BankStaffs).ReturnsDbSet([new() { Id = 1, FirstName = "a", LastName = "b" }]);
        moq.Context.Setup(x => x.Approvers).ReturnsDbSet([new() { Title = "" }]);
        moq.Context.Setup(x => x.Approvers.Add(It.IsAny<Core.Entities.Approver>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new AddApproverRequestHandler(moq.Context.Object);

        var request = new AddApproverRequest { BankStaffId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddApproverRequest_Success()
    {
        moq.Context.Setup(x => x.BankStaffs).ReturnsDbSet([new() { Id = 1, FirstName = "a", LastName = "b" }]);
        moq.Context.Setup(x => x.Approvers).ReturnsDbSet([new() { Title = "" }]);
        moq.Context.Setup(x => x.Approvers.Add(It.IsAny<Core.Entities.Approver>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddApproverRequestHandler(moq.Context.Object);

        var request = new AddApproverRequest { BankStaffId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
