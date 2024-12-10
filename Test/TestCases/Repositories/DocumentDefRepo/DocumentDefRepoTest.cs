using Core.Entities;
using Core.ViewModel.DocumentDef;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Repositories.DocumentDefRepo;

public class DocumentDefRepoTest
{
    private readonly Mock<DBContext> _contextMoq = new();

    [Fact]
    public async Task GetDocumentDef()
    {
        _contextMoq.Setup(x => x.DocumentDefs).ReturnsDbSet(
            new List<DocumentDef>
            {
                new()
                {
                    Id = 0,
                    DocumentGroupId = 0
                }
            });
        _contextMoq.Setup(x => x.DocumentGroups).ReturnsDbSet(new List<DocumentGroup> { new() { Id = 0 } });

        var getDocumentDefRepo = new Infrastructure.Repositories.DocumentDefRepository.DocumentDefRepo(_contextMoq.Object);

        var result = await getDocumentDefRepo.GetDocumentDef(0);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task SearchDocumentDef()
    {
        _contextMoq.Setup(x => x.DocumentDefs).ReturnsDbSet(
            new List<DocumentDef>
            {
                new()
                {
                    DocumentGroupId = 0
                }
            });
        _contextMoq.Setup(x => x.DocumentGroups).ReturnsDbSet(new List<DocumentGroup> { new(){ Id = 0 } });

        var searchDocumentDefRepo = new Infrastructure.Repositories.DocumentDefRepository.DocumentDefRepo(_contextMoq.Object);

        var result = await searchDocumentDefRepo.SearchDocumentDef(new DocumentDefIM { Page = 1, Size = 1 });

        Assert.NotEmpty(result.Items);
    }
}
