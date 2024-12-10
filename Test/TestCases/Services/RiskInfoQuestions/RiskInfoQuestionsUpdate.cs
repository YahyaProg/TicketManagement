using Application.Services.RiskInfoQuestionsService;
using Infrastructure;
using Moq;
using Test.Helper;

namespace Test.TestCases.Services.RiskInfoQuestions;

public class RiskInfoQuestionsTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task RiskInfoQuestionsRequest_Success()
    {
        var command = new RiskInfoQuestionsUpdateRequest
        {
            Id = 0,
            Cutoffjson= "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
            Category = Core.Enums.ERiskInfoGroup_category.C,
            Items =
            [
                new()
                {
                    Weight = 100,
                    AutoCalculate = Core.Enums.ERiskInfoItem_autoCalculate.HISTORY_OF_CREDIT_TRANSACTIONS,
                    Items =
                    [
                        new()
                        {
                            Id = 0
                        }
                    ]
                }
            ]
        };

        var collection = MoqHelper.GetUnitOfWorkMoqCollection();

        collection.Context.Setup(x => x.RiskInfoGroups.Update(It.IsAny<Core.Entities.RiskInfoGroup>()));
        collection.Context.Setup(x => x.RiskInfoItems.Update(It.IsAny<Core.Entities.RiskInfoItem>()));
        collection.Context.Setup(x => x.RiskInfoSubItems.Update(It.IsAny<Core.Entities.RiskInfoSubItem>()));
        collection.Context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        //collection.Context.Setup(x => x.Database);
        var handler = new RiskInfoQuestionsUpdateRequestHandler(collection.UnitOfWork.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task RiskInfoQuestionsRequest_Fail()
    {
        var command = new RiskInfoQuestionsUpdateRequest
        {
            Id = 0,
            Cutoffjson = "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
            Category = Core.Enums.ERiskInfoGroup_category.I,
            Items =
            [
                new()
                {
                    AutoCalculate = 0,
                    Items =
                    [
                        new()
                        {
                            Id = 0,
                            Condition = "incalculable"
                        }
                    ]
                }
            ]
        };

        var collection = MoqHelper.GetUnitOfWorkMoqCollection();

        collection.Context.Setup(x => x.RiskInfoGroups.Update(It.IsAny<Core.Entities.RiskInfoGroup>()));
        collection.Context.Setup(x => x.RiskInfoItems.Update(It.IsAny<Core.Entities.RiskInfoItem>()));
        collection.Context.Setup(x => x.RiskInfoSubItems.Update(It.IsAny<Core.Entities.RiskInfoSubItem>()));
        collection.Context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

        var handler = new RiskInfoQuestionsUpdateRequestHandler(collection.UnitOfWork.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }
}
