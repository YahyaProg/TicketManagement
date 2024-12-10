using Application.Services.RiskInfoGroupService;
using Core.Helpers;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.RiskInfoGroup;

public class AddRiskInfoGroupTest
{
    private readonly Mock<IUserHelper> UserHelper = new();
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task AddRiskInfoGroup_Fail()
    {
        UserHelper.Setup(x => x.GetUserFromToken()).Returns(new Core.ViewModel.UserDto { Id = "1" });

        moq.Context.Setup(x => x.BankStaffs).ReturnsDbSet([new() { UserId = "1" }]);
        moq.Context.Setup(x => x.RiskInfoGroups.AddAsync(It.IsAny<Core.Entities.RiskInfoGroup>(), CancellationToken.None));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new AddRiskInfoGroupRequestHandler(moq.UnitOfWork.Object, UserHelper.Object);

        var request = new AddRiskInfoGroupRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddRiskInfoGroup_Success()
    {
        UserHelper.Setup(x => x.GetUserFromToken()).Returns(new Core.ViewModel.UserDto { Id = "1" });

        moq.Context.Setup(x => x.BankStaffs).ReturnsDbSet([new() { UserId = "1" }]);
        moq.Context.Setup(x => x.RiskInfoGroups.AddAsync(It.IsAny<Core.Entities.RiskInfoGroup>(), CancellationToken.None));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddRiskInfoGroupRequestHandler(moq.UnitOfWork.Object, UserHelper.Object);

        var request = new AddRiskInfoGroupRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
