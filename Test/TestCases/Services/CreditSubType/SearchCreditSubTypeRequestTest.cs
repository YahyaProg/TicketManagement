using Application.Services.CreditSubTypeService;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.CreditSubTypeTest;

public class SearchCreditSubTypeRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task SearchCreditSubTypeRequest_Success()
    {
        moq.Context.Setup(x => x.CreditSubTypes).ReturnsDbSet([new() { CreditTypeId = 1 }]);
        moq.Context.Setup(x => x.CreditTypes).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new SearchCreditSubTypeRequestHandler(moq.Context.Object);

        var request = new SearchCreditSubTypeRequest { Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotEmpty(result.Data.Items);
    }
}
