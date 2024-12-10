using Application.Services.Auth;
using Core.Helpers;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.AuthServiceTest;

public class GetTokenDataTest
{
    [Fact]
    public async Task Success()
    {
        var context = new Mock<DBContext>();
        var helper = new Mock<IUserHelper>();

        context.Setup(x => x.Branches).ReturnsDbSet([new Core.Entities.Branch() { Id = 1, Code = "" }]);
        helper.Setup(x => x.GetUserFromToken()).Returns(new Core.ViewModel.UserDto() { BranchCode = "" });

        var request = new GetTokenDataRequest();

        var handler = new GetTokenDataRequestHandler(context.Object, helper.Object);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.True(res.IsSuccess);
    }
}
