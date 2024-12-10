using Application.Services.RiskInfoService;
using Core.GenericResultModel;
using Core.ViewModel.RiskInfoGroup;
using Core.ViewModel.RiskInfoPage;
using DocumentFormat.OpenXml.VariantTypes;
using MediatR;
using Moq;

namespace Test.TestCases.Services.RiskInfoAnswerTests;

public class GetRiskInfoAnswerExcelTests
{
    private readonly GetRiskInfoAnswerExcelRequest request = new() { ProposalSchemeId = 1 };
    private ApiResult<IEnumerable<RiskInfoGroupVm>> response = new()
    {
        Message = "123",
        MessageEn = "123",
        Data =
        [
            new()
            {
                Id = 1,
                Title = "Test",
                Items = [
                    new(){
                        Answer = null,
                    },
                    new(){
                        Answer = new(){
                            AutoCalculate = Core.Enums.ERiskInfoItem_autoCalculate.CREDITOR_LAST_TRANSACTIONS,
                            Response = 123
                        }
                    },
                    new(){
                        Answer = new(){
                            AutoCalculate = null,
                            Response = null,
                            RiskInfoSubItemId = 1
                        },
                        SubItems = [
                            new(){
                                Id = 1
                            }
                            ]
                    }
                    ]
            }
        ]
    };
    private readonly Mock<IMediator> _mediator = new();

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task GetExcel_Test(bool isSuccess)
    {
        response.IsSuccess = isSuccess;
        _mediator.Setup(x => x.Send(It.IsAny<GetRiskInfoAnswerRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);

        var handler = new GetRiskInfoAnswerExcelRequestHandler(_mediator.Object);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.Equal(isSuccess, res.IsSuccess);
    }


}
