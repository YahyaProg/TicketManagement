using Application.Services.CmntService;
using Core.ViewModel;
using Moq;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.Cmnt;


public class AdvancedSearchCmntRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task AdvancedSearchCmntRequest_Success2()
    {
        moq.UnitOfWork.Setup(x => x.CmntRepo.AdvancedSearch(It.IsAny<CmntIM>()));

        var handler = new AdvancedSearchRequestCmntHandler(moq.UnitOfWork.Object);

        var request = new AdvancedSearchCmntRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
