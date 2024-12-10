using Application.Services.CollateralService;
using Core.Enums;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.Collateral;

public class GetCollateralRequestTest
{
    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    [Fact]
    public async Task GetCollateralRequest_Fail()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 2 }]);

        var handler = new GetCollateralRequestHandler(moq.Context.Object);

        var request = new GetCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    //BankQuaranteeCollateral

    [Fact]
    public async Task GetCollateralRequest_BankQuaranteeCollateral1()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.BankQuaranteeCollateral }]);
        //--------------------
        moq.Context.Setup(x => x.BankQuaranteeCollaterals).ReturnsDbSet([new() { Id = 1 }]);
        //----------
        moq.Context.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new GetCollateralRequestHandler(moq.Context.Object);

        var request = new GetCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task GetCollateralRequest_BankQuaranteeCollateral2()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.BankQuaranteeCollateral }]);
        //--------------------
        moq.Context.Setup(x => x.BankQuaranteeCollaterals).ReturnsDbSet([new() { Id = 1, CustomerId = 2 }]);
        //----------
        moq.Context.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new GetCollateralRequestHandler(moq.Context.Object);

        var request = new GetCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result.Data);
    }

    //ChequeCollateral

    [Fact]
    public async Task GetCollateralRequest_ChequeCollateral()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.ChequeCollateral }]);
        //--------------------
        moq.Context.Setup(x => x.ChequeCollaterals).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new GetCollateralRequestHandler(moq.Context.Object);

        var request = new GetCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result.Data);
    }

    //ContractCollateral

    [Fact]
    public async Task GetCollateralRequest_ContractCollateral()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.ContractCollateral }]);
        //--------------------
        moq.Context.Setup(x => x.ContractCollaterals).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new GetCollateralRequestHandler(moq.Context.Object);

        var request = new GetCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result.Data);
    }

    //DepositCollateral

    [Fact]
    public async Task GetCollateralRequest_DepositCollateral()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.DepositCollateral }]);
        //--------------------
        moq.Context.Setup(x => x.DepositCollaterals).ReturnsDbSet([new() { Id = 1, CustomerId = 1 }]);
        //----------
        moq.Context.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 1, IndividualCustomer = new(), CorporateCustomer = new() }]);

        var handler = new GetCollateralRequestHandler(moq.Context.Object);

        var request = new GetCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result.Data);
    }

    //EstateCollateral

    [Fact]
    public async Task GetCollateralRequest_EstateCollateral()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.EstateCollateral }]);
        //--------------------
        moq.Context.Setup(x => x.EstateCollaterals).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new GetCollateralRequestHandler(moq.Context.Object);

        var request = new GetCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result.Data);
    }

    //FixedAssetCollateral

    [Fact]
    public async Task GetCollateralRequest_FixedAssetCollateral()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.FixedAssetCollateral }]);
        //--------------------
        moq.Context.Setup(x => x.FixedAssetCollaterals).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new GetCollateralRequestHandler(moq.Context.Object);

        var request = new GetCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result.Data);
    }

    //SafteCollateral

    [Fact]
    public async Task GetCollateralRequest_SafteCollateral()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = ECollateral_Type.SafteCollateral }]);
        //--------------------
        moq.Context.Setup(x => x.SafteCollaterals).ReturnsDbSet([new() { Id = 1 }]);

        var handler = new GetCollateralRequestHandler(moq.Context.Object);

        var request = new GetCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result.Data);
    }

    //Default

    [Fact]
    public async Task GetCollateralRequest_Default()
    {
        moq.Context.Setup(x => x.Collaterals).ReturnsDbSet([new() { Id = 1, CollateralTypeId = 1 }]);
        moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet([new() { Id = 1, Type = (ECollateral_Type)11 }]);

        var handler = new GetCollateralRequestHandler(moq.Context.Object);

        var request = new GetCollateralRequest { Id = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result.Data);
    }
}
