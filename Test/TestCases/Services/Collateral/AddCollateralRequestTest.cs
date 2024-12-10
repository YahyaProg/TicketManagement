using Application.Services.BaseService;
using Application.Services.CollateralService;
using Core.Enums;
using Core.GenericResultModel;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.Collateral;

public class AddCollateralRequestTest
{
    private readonly Mock<IMediator> mockMediator = new();
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    //Handle

    [Fact]
    public async Task Handle_Fail()
    {
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = 0 }]);

        var handler = new AddCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddCollateralRequest { CollateralTypeId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    //AddBankQuaranteeCollateral

    [Fact]
    public async Task AddBankQuaranteeCollateral_Fail1()
    {
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.BankQuaranteeCollateral }]);
        ///----------
        mockMediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(new ApiResult<long>(400, false));

        var handler = new AddCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddCollateralRequest { CollateralTypeId = 1, RelationType = EBankQuaranteeCollateral_relationType.Indirect };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddBankQuaranteeCollateral_Fail2()
    {
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.BankQuaranteeCollateral }]);
        ///----------
        mockMediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(new ApiResult<long>() { Data = 1 });
        moq.Context.Setup(x => x.Collaterals.Add(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new AddCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddCollateralRequest { CollateralTypeId = 1, RelationType = EBankQuaranteeCollateral_relationType.Indirect };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    //AddChequeCollateral

    [Fact]
    public async Task AddChequeCollateral_Success()
    {
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.ChequeCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Add(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddCollateralRequest { CollateralTypeId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //AddContractCollateral

    [Fact]
    public async Task AddContractCollateral_Success()
    {
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.ContractCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Add(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddCollateralRequest { CollateralTypeId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //AddDepositCollateral

    [Fact]
    public async Task AddDepositCollateral_Fail()
    {
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.DepositCollateral }]);
        ///----------
        mockMediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(new ApiResult<long>(400, false));

        var handler = new AddCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddCollateralRequest { CollateralTypeId = 1, RelationType = EBankQuaranteeCollateral_relationType.Indirect };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddDepositCollateral_Success()
    {
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.DepositCollateral }]);
        ///----------
        mockMediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(new ApiResult<long>() { Data = 1 });
        moq.Context.Setup(x => x.Collaterals.Add(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddCollateralRequest { CollateralTypeId = 1, RelationType = EBankQuaranteeCollateral_relationType.Indirect };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //AddEstateCollateral

    [Fact]
    public async Task AddEstateCollateral_Success()
    {
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.EstateCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Add(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddCollateralRequest { CollateralTypeId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //AddFixedAssetCollateral

    [Fact]
    public async Task AddFixedAssetCollateral_Success()
    {
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.FixedAssetCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Add(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddCollateralRequest { CollateralTypeId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //AddSafteCollateral

    [Fact]
    public async Task AddSafteCollateral_Success()
    {
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.SafteCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Add(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddCollateralRequest { CollateralTypeId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //AddStocksCollateral

    [Fact]
    public async Task AddStocksCollateral_Success()
    {
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.StocksCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Add(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddCollateralRequest { CollateralTypeId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //AddCurrentChequeCollateral

    [Fact]
    public async Task AddCurrentChequeCollateral_Success()
    {
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.CurrentChequeCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Add(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddCollateralRequest { CollateralTypeId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //AddVehicleCollateral

    [Fact]
    public async Task AddVehicleCollateral_Success()
    {
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.VehicleCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Add(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new AddCollateralRequest { CollateralTypeId = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
