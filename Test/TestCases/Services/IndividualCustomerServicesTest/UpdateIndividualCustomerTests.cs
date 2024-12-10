using Application.Services.BaseService;
using Application.Services.IndividualCustomerService;
using Core.GenericResultModel;
using Infrastructure;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.IndividualCustomerServicesTest;

public class UpdateIndividualCustomerTests
{
    private readonly UpdateIndividualCustomerRequest request = new()
    {
        BirthDate = DateTime.Now,
        BranchId = 1,
        ClientNo = "1",
        FirstName = "1",
        LastName = "1",
        Foreign = false,
        NationalId = "1",
        Vip = true,
        Id = 1
    };

    private readonly Mock<IMediator> mediator = new();
    private readonly Mock<DBContext> context = new();

    private readonly UpdateIndividualCustomerRequestHandler handler;
    public UpdateIndividualCustomerTests()
    {
        handler = new(context.Object, mediator.Object);
    }

    [Fact]
    public async Task InquiryFailed()
    {
        mediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ApiResult<long>(400, false) { Message = "123", MessageEn = "123" });

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.False(res.IsSuccess);
    }

    [Fact]
    public async Task CustomerNotFound()
    {
        mediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ApiResult<long>() { Data = 1, Message = "123", MessageEn = "123" });

        context.Setup(x => x.Customers).ReturnsDbSet([]);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.False(res.IsSuccess);
    }

    [Fact]
    public async Task BranchNotFound()
    {
        mediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ApiResult<long>() { Data = 1, Message = "123", MessageEn = "123" });

        context.Setup(x => x.Customers).ReturnsDbSet([new() {
            Id = 1,
            IndividualCustomer = new(){
                HasInquiry = false,
            }
        }]);

        context.Setup(x => x.Branches).ReturnsDbSet([]);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.False(res.IsSuccess);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    public async Task Final(int modifiedRowCount)
    {
        mediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ApiResult<long>() { Data = 1, Message = "123", MessageEn = "123" });

        context.Setup(x => x.Customers).ReturnsDbSet([new() {
            Id = 1,
            IndividualCustomer = new(){
                HasInquiry = false,
            }
        }]);

        context.Setup(x => x.Branches).ReturnsDbSet([new() {
            Id = request.BranchId.Value,
        }]);

        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(modifiedRowCount);
        var res = await handler.Handle(request, CancellationToken.None);

        Assert.Equal(modifiedRowCount > 0, res.IsSuccess);
    }
}
