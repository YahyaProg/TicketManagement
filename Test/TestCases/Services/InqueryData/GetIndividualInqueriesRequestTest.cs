using Application.Services.InqueryData;
using Core.Enums;
using Core.GenericResultModel;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.InqueryData;

public class GetIndividualInqueriesRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();
    private readonly Mock<IMediator> mockMediator = new();

    [Theory]
    [InlineData(EInqueryType.ValidationOfIranians)]
    [InlineData(EInqueryType.Individual_RequestedAmount)]
    public async Task GetIndividualInqueriesRequest_Line35_41(EInqueryType inqueryType)
    {
        mockMediator.Setup(x => x.Send(It.IsAny<GetInqueriesRequest>(), CancellationToken.None))
            .ReturnsAsync(new ApiResult<Core.ViewModel.InqueryDataVM>(400, false));

        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet([new() { Id = 2 }]);

        var handler = new GetIndividualInqueriesRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new GetIndividualInqueriesRequest
        {
            ProposalSchemeId = 1,
            InqueryType = inqueryType
        };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData(EInqueryType.Individual_RequestedAmount)]
    public async Task GetInqueriesRequest_Line61(EInqueryType inqueryType)
    {
        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet([new() { Id = 1, CustomerId = 1, ProposalId = 1 }]);
        moq.Context.Setup(x => x.CustomerRequestDetails).ReturnsDbSet([new() { CustomerId = 1, ProposalId = 1, Amount = 100 }]);

        var handler = new GetIndividualInqueriesRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new GetIndividualInqueriesRequest
        {
            ProposalSchemeId = 1,
            InqueryType = inqueryType
        };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
