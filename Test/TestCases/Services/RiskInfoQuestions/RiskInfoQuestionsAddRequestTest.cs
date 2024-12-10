using Application.Services.RiskInfoQuestionsService;
using Core.Helpers;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.RiskInfoQuestions;

public class RiskInfoQuestionsAddRequestTest
{
    private readonly Mock<IUserHelper> UserHelper = new();
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task RiskInfoQuestionsAddRequest_Fail()
    {
        UserHelper.Setup(x => x.GetUserFromToken()).Returns(new Core.ViewModel.UserDto { Id = "1" });

        moq.Context.Setup(x => x.BankStaffs).ReturnsDbSet([new() { UserId = "1" }]);
        moq.Context.Setup(x => x.RiskInfoGroups.AddAsync(It.IsAny<Core.Entities.RiskInfoGroup>(), CancellationToken.None));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new RiskInfoQuestionsAddRequestHandler(moq.UnitOfWork.Object, UserHelper.Object);

        var request = new RiskInfoQuestionsAddRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task RiskInfoQuestionsAddRequest_Error()
    {
        UserHelper.Setup(x => x.GetUserFromToken()).Returns(new Core.ViewModel.UserDto { Id = "1" });

        moq.Context.Setup(x => x.BankStaffs).ReturnsDbSet([new() { UserId = "1" }]);
        moq.Context.Setup(x => x.RiskInfoGroups.AddAsync(It.IsAny<Core.Entities.RiskInfoGroup>(), CancellationToken.None));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        moq.Context.Setup(x => x.RiskInfoGroups).ReturnsDbSet([]);

        moq.Context.Setup(x => x.RiskInfoItems.AddAsync(It.IsAny<Core.Entities.RiskInfoItem>(), CancellationToken.None));
        moq.Context.Setup(x => x.RiskInfoSubItems.AddRangeAsync(It.IsAny<IEnumerable<Core.Entities.RiskInfoSubItem>>(), CancellationToken.None));

        var handler = new RiskInfoQuestionsAddRequestHandler(moq.UnitOfWork.Object, UserHelper.Object);

        var request = new RiskInfoQuestionsAddRequest { Items = [new() { Items = [new() { Title = "" }], Weight = 100, AutoCalculate = Core.Enums.ERiskInfoItem_autoCalculate.LAST_BALANCE_SCORE_IN_PARSIAN }], Category = Core.Enums.ERiskInfoGroup_category.I };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task RiskInfoQuestionsAddRequest_Success()
    {
        UserHelper.Setup(x => x.GetUserFromToken()).Returns(new Core.ViewModel.UserDto { Id = "1" });

        moq.Context.Setup(x => x.BankStaffs).ReturnsDbSet([new() { UserId = "1" }]);
        moq.Context.Setup(x => x.RiskInfoGroups.AddAsync(It.IsAny<Core.Entities.RiskInfoGroup>(), CancellationToken.None));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        moq.Context.Setup(x => x.RiskInfoGroups).ReturnsDbSet([]);
        moq.Context.Setup(x => x.RiskInfoItems.AddAsync(It.IsAny<Core.Entities.RiskInfoItem>(), CancellationToken.None));
        moq.Context.Setup(x => x.RiskInfoSubItems.AddRangeAsync(It.IsAny<IEnumerable<Core.Entities.RiskInfoSubItem>>(), CancellationToken.None));

        var handler = new RiskInfoQuestionsAddRequestHandler(moq.UnitOfWork.Object, UserHelper.Object);

        var request = new RiskInfoQuestionsAddRequest { Items = [new() { Items = [new() { Title = "" }], Weight = 100, AutoCalculate = Core.Enums.ERiskInfoItem_autoCalculate.HISTORY_OF_CREDIT_TRANSACTIONS }], Category = Core.Enums.ERiskInfoGroup_category.C };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
