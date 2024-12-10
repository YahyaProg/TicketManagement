using Application.Services.BaseService;
using Application.Services.CollateralService;
using Core.Enums;
using Core.GenericResultModel;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.Collateral;

public class UpdateCollateralRequestTest
{
    private readonly Mock<IMediator> mockMediator = new();
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    //Handle

    [Fact]
    public async Task Handle_Fail1()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_Fail2()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = 0 }]);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    //UpdateBankQuaranteeCollateral

    [Fact]
    public async Task UpdateBankQuaranteeCollateral_Fail1()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1, BankQuaranteeCollateral = new() }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.BankQuaranteeCollateral }]);
        ///----------
        mockMediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(new ApiResult<long>(400, false));

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1, RelationType = EBankQuaranteeCollateral_relationType.Indirect };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateBankQuaranteeCollateral_Fail2()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1, BankQuaranteeCollateral = new() }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.BankQuaranteeCollateral }]);
        ///----------
        mockMediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(new ApiResult<long>() { Data = 1 });
        moq.Context.Setup(x => x.Collaterals.Update(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1, RelationType = EBankQuaranteeCollateral_relationType.Indirect };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateBankQuaranteeCollateral_Success()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1, BankQuaranteeCollateral = new() }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.BankQuaranteeCollateral }]);
        ///----------
        mockMediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(new ApiResult<long>() { Data = 1 });
        moq.Context.Setup(x => x.Collaterals.Update(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1, RelationType = EBankQuaranteeCollateral_relationType.Indirect };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //UpdateChequeCollateral

    [Fact]
    public async Task UpdateChequeCollateral_Fail()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1, ChequeCollateral = new() }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.ChequeCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Update(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1, AllManagers = true };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateChequeCollateral_Success()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1, ChequeCollateral = new() }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.ChequeCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Update(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1, AllManagers = true };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //UpdateContractCollateral

    [Fact]
    public async Task UpdateContractCollateral_Fail()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1, ContractCollateral = new() }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.ContractCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Update(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateContractCollateral_Success()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1, ContractCollateral = new() }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.ContractCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Update(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //UpdateDepositCollateral

    [Fact]
    public async Task UpdateDepositCollateral_Fail1()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1, DepositCollateral = new() }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.DepositCollateral }]);
        ///----------
        mockMediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(new ApiResult<long>(400, false));

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1, RelationType = EBankQuaranteeCollateral_relationType.Indirect };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateDepositCollateral_Fail2()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1, DepositCollateral = new() }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.DepositCollateral }]);
        ///----------
        mockMediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(new ApiResult<long>() { Data = 1 });
        moq.Context.Setup(x => x.Collaterals.Update(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1, RelationType = EBankQuaranteeCollateral_relationType.Indirect };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateDepositCollateral_Success()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1, DepositCollateral = new() }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.DepositCollateral }]);
        ///----------
        mockMediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(new ApiResult<long>() { Data = 1 });
        moq.Context.Setup(x => x.Collaterals.Update(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1, RelationType = EBankQuaranteeCollateral_relationType.Indirect };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //UpdateEstateCollateral

    [Fact]
    public async Task UpdateEstateCollateral_Fail()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1, EstateCollateral = new() }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.EstateCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Update(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateEstateCollateral_Success()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1, EstateCollateral = new() }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.EstateCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Update(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //UpdateFixedAssetCollateral

    [Fact]
    public async Task UpdateFixedAssetCollateral_Fail()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1, FixedAssetCollateral = new() }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.FixedAssetCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Update(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateFixedAssetCollateral_Success()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1, FixedAssetCollateral = new() }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.FixedAssetCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Update(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //UpdateSafteCollateral

    [Fact]
    public async Task UpdateSafteCollateral_Fail()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1, SafteCollateral = new() }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.SafteCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Update(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1, AllManagers = true };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateSafteCollateral_Success()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1, SafteCollateral = new() }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.SafteCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Update(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1, AllManagers = true };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //UpdateDefualt

    [Fact]
    public async Task UpdateDefualt_Fail()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.StocksCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Update(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateDefualt_Success1()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.CurrentChequeCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Update(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateDefualt_Success2()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.VehicleCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Update(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new UpdateCollateralRequestHandler(moq.Context.Object, mockMediator.Object);

        var request = new UpdateCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
