using Application.Services.CollateralService;
using Core.Enums;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.Collateral;

public class DeleteCollateralRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    //Handle

    [Fact]
    public async Task Handle_Fail1()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new DeleteCollateralRequestHandler(moq.Context.Object);

        var request = new DeleteCollateralRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_Fail2()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = 0 }]);

        var handler = new DeleteCollateralRequestHandler(moq.Context.Object);

        var request = new DeleteCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    //DeleteChequeCollateral

    [Fact]
    public async Task DeleteChequeCollateral_Fail()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.ChequeCollateral }]);
        ///----------
        moq.Context.Setup(x => x.ChequeCollateralSigners).ReturnsDbSet([new() { ChequeCollateralId = 1 }]);
        moq.Context.Setup(x => x.ChequeCollaterals).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.ChequeCollaterals.Remove(It.IsAny<Core.Entities.ChequeCollateral>()));

        moq.Context.Setup(x => x.Collaterals.Remove(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new DeleteCollateralRequestHandler(moq.Context.Object);

        var request = new DeleteCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    //DeleteContractCollateral

    [Fact]
    public async Task DeleteContractCollateral_Success()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.ContractCollateral }]);
        ///----------
        moq.Context.Setup(x => x.ContractZamens).ReturnsDbSet([new() { ContractCollateralId = 1 }]);
        moq.Context.Setup(x => x.ContractCollaterals).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.ContractCollaterals.Remove(It.IsAny<Core.Entities.ContractCollateral>()));

        moq.Context.Setup(x => x.Collaterals.Remove(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new DeleteCollateralRequestHandler(moq.Context.Object);

        var request = new DeleteCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //DeleteEstateCollateral

    [Fact]
    public async Task DeleteEstateCollateral_Success()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.EstateCollateral }]);
        ///----------
        moq.Context.Setup(x => x.EstateCollateralEvaluations).ReturnsDbSet([new() { EstateCollateralId = 1 }]);
        moq.Context.Setup(x => x.EstateCollateralOwners).ReturnsDbSet([new() { EstateCollateralId = 1 }]);
        moq.Context.Setup(x => x.EstateCollateralPelaks).ReturnsDbSet([new() { EstateCollateralId = 1 }]);

        moq.Context.Setup(x => x.EstateCollaterals).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.EstateCollaterals.Remove(It.IsAny<Core.Entities.EstateCollateral>()));

        moq.Context.Setup(x => x.Collaterals.Remove(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new DeleteCollateralRequestHandler(moq.Context.Object);

        var request = new DeleteCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //DeleteDepositCollateral

    [Fact]
    public async Task DeleteDepositCollateral_Success()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.DepositCollateral }]);
        ///----------
        moq.Context.Setup(x => x.DepositCollaterals).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.DepositCollaterals.Remove(It.IsAny<Core.Entities.DepositCollateral>()));

        moq.Context.Setup(x => x.Collaterals.Remove(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new DeleteCollateralRequestHandler(moq.Context.Object);

        var request = new DeleteCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //DeleteFixedAssetCollateral

    [Fact]
    public async Task DeleteFixedAssetCollateral_Success()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.FixedAssetCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Fixasstcolatralevals).ReturnsDbSet([new() { FixedAssetCollateralId = 1 }]);
        moq.Context.Setup(x => x.FixedAssetCollateralOwners).ReturnsDbSet([new() { FixedAssetCollateralId = 1 }]);
        moq.Context.Setup(x => x.FixedAssetCollaterals).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.FixedAssetCollaterals.Remove(It.IsAny<Core.Entities.FixedAssetCollateral>()));

        moq.Context.Setup(x => x.Collaterals.Remove(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new DeleteCollateralRequestHandler(moq.Context.Object);

        var request = new DeleteCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //DeleteSafteCollateral

    [Fact]
    public async Task DeleteSafteCollateral_Success()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.SafteCollateral }]);
        ///----------
        moq.Context.Setup(x => x.SafteCollateralSigners).ReturnsDbSet([new() { SafteCollateralId = 1 }]);
        moq.Context.Setup(x => x.SafteCollaterals).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.SafteCollaterals.Remove(It.IsAny<Core.Entities.SafteCollateral>()));

        moq.Context.Setup(x => x.Collaterals.Remove(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new DeleteCollateralRequestHandler(moq.Context.Object);

        var request = new DeleteCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //DeleteStocksCollateral

    [Fact]
    public async Task DeleteStocksCollateral_Success()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.StocksCollateral }]);
        ///----------
        moq.Context.Setup(x => x.StocksCollateralItems).ReturnsDbSet([new() { StocksCollateralId = 1 }]);
        moq.Context.Setup(x => x.StocksCollaterals).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.StocksCollaterals.Remove(It.IsAny<Core.Entities.StocksCollateral>()));

        moq.Context.Setup(x => x.Collaterals.Remove(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new DeleteCollateralRequestHandler(moq.Context.Object);

        var request = new DeleteCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //DeleteCurrentChequeCollateral

    [Fact]
    public async Task DeleteCurrentChequeCollateral_Success()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.CurrentChequeCollateral }]);
        ///----------
        moq.Context.Setup(x => x.BlackListChequeCollaterals).ReturnsDbSet([new() { ChequeCollateralId = 1 }]);
        moq.Context.Setup(x => x.WhiteListChequeCollaterals).ReturnsDbSet([new() { ChequeCollateralId = 1 }]);
        moq.Context.Setup(x => x.Otheracctchqcolatrals).ReturnsDbSet([new() { ChequeCollateralId = 1 }]);
        moq.Context.Setup(x => x.CurrentChequeCollaterals).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.CurrentChequeCollaterals.Remove(It.IsAny<Core.Entities.CurrentChequeCollateral>()));

        moq.Context.Setup(x => x.Collaterals.Remove(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new DeleteCollateralRequestHandler(moq.Context.Object);

        var request = new DeleteCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //DeleteVehicleCollateral

    [Fact]
    public async Task DeleteVehicleCollateral_Success()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.VehicleCollateral }]);
        ///----------
        moq.Context.Setup(x => x.VehicleCollateralEvaluations).ReturnsDbSet([new() { VehicleCollateralId = 1 }]);
        moq.Context.Setup(x => x.VehicleCollateralOwners).ReturnsDbSet([new() { VehicleCollateralId = 1 }]);
        moq.Context.Setup(x => x.VehicleCollaterals).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.VehicleCollaterals.Remove(It.IsAny<Core.Entities.VehicleCollateral>()));

        moq.Context.Setup(x => x.Collaterals.Remove(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new DeleteCollateralRequestHandler(moq.Context.Object);

        var request = new DeleteCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    //DeleteDefault

    [Fact]
    public async Task DeleteDefault_Success()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.BankQuaranteeCollateral }]);
        ///----------
        moq.Context.Setup(x => x.Collaterals.Remove(It.IsAny<Core.Entities.Collateral>()));
        moq.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new DeleteCollateralRequestHandler(moq.Context.Object);

        var request = new DeleteCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
