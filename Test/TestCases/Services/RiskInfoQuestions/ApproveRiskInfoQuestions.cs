using Application.Services.RiskInfoQuestionsService;
using Core.Helpers;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.ApproveRiskInfoQuestions;

public class ApproveRiskInfoQuestionsTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<IUserHelper> _userHelper = new();

    [Fact]
    public async Task ApproveRiskInfoQuestionsRequest_Success()
    {
        var command = new ApproveRiskInfoQuestionsRequest{Id = 0};

        _unitOfWork.Setup(x => x.Context.RiskInfoGroups).ReturnsDbSet([new() { Id = 0 }]);
        _unitOfWork.Setup(x => x.Context.BankStaffs).ReturnsDbSet([new() { Id = 0, UserId = "0" }]);
        _unitOfWork.Setup(x => x.Context.RiskInfoGroups.Update(It.IsAny<Core.Entities.RiskInfoGroup>()));
        _unitOfWork.Setup(x => x.Context.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        _userHelper.Setup(x => x.GetUserFromToken()).Returns(new Core.ViewModel.UserDto { Id = "0" });

        var handler = new ApproveRiskInfoQuestionsRequestHandler(_unitOfWork.Object, _userHelper.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task ApproveRiskInfoQuestionsRequest_Fail1()
    {
        var command = new ApproveRiskInfoQuestionsRequest { Id = 0 };

        _unitOfWork.Setup(x => x.Context.RiskInfoGroups).ReturnsDbSet([]);

        var handler = new ApproveRiskInfoQuestionsRequestHandler(_unitOfWork.Object, _userHelper.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task ApproveRiskInfoQuestionsRequest_Fail2()
    {
        var command = new ApproveRiskInfoQuestionsRequest { Id = 0 };

        _unitOfWork.Setup(x => x.Context.RiskInfoGroups).ReturnsDbSet([new() { ApproverId = 0 }]);

        var handler = new ApproveRiskInfoQuestionsRequestHandler(_unitOfWork.Object, _userHelper.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task ApproveRiskInfoQuestionsRequest_Fail3()
    {
        var command = new ApproveRiskInfoQuestionsRequest { Id = 0 };

        _unitOfWork.Setup(x => x.Context.RiskInfoGroups).ReturnsDbSet([new() { Id = 0 }]);
        _unitOfWork.Setup(x => x.Context.BankStaffs).ReturnsDbSet([new() { Id = 0, UserId = "0" }]);
        _unitOfWork.Setup(x => x.Context.RiskInfoGroups.Update(It.IsAny<Core.Entities.RiskInfoGroup>()));
        _unitOfWork.Setup(x => x.Context.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
        _userHelper.Setup(x => x.GetUserFromToken()).Returns(new Core.ViewModel.UserDto { Id = "0" });

        var handler = new ApproveRiskInfoQuestionsRequestHandler(_unitOfWork.Object, _userHelper.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }
}
