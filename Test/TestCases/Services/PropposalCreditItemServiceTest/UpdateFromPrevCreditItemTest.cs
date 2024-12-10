using Application.Services.ProposalCreditItemService;
using Core.Enums;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.ProposalCreditItemService
{
    public class UpdateFromPrevCreditItemTest
    {

        public static IEnumerable<object[]> requests =>
            new List<object[]>
            {
                        new object[] { ECollateral_Type.ChequeCollateral },
                        new object[] { ECollateral_Type.ContractCollateral },
                        new object[] { ECollateral_Type.EstateCollateral },
                        new object[] { ECollateral_Type.FixedAssetCollateral },
                        new object[] { ECollateral_Type.SafteCollateral },
                        new object[] { ECollateral_Type.VehicleCollateral },
                        new object[] { ECollateral_Type.StocksCollateral },
                        new object[] { ECollateral_Type.CurrentChequeCollateral },
            };


        [Theory, MemberData(nameof(requests))]
        public async Task UpdateFromPrevCreditItem(ECollateral_Type colType)
        {
            var request = new UpdateFromPrevCreditItemRequest()
            {
                ProposalSchemeId = 1
            };

            var moq = GetUnitOfWorkMoqCollection();
            moq.Context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet(new List<Core.Entities.ProposalScheme>()
            {
                new(){Id = 1, CustomerId = 1, ProposalId = 1, CreateDate = DateTime.Now.AddDays(-19)},
                new(){Id = 2, CustomerId = 1, ProposalId = 1, CreateDate = DateTime.Now}
            });

            moq.Context.Setup(x => x.CustomerSchemes).ReturnsDbSet(new List<Core.Entities.CustomerScheme>()
            {
                new(){Id = 1, CustomerId = 1, ProposalId = 1, ProposalSchemeId = 1},
            });

            moq.Context.Setup(x => x.Customers).ReturnsDbSet(new List<Core.Entities.Customer>()
            {
                new(){Id = 1},
            });

            moq.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet(new List<Core.Entities.CorporateCustomer>()
            {
                new(){Id = 1},
            });

            moq.Context.Setup(x => x.Managers).ReturnsDbSet(new List<Core.Entities.Manager>()
            {
                new(){Id = 1, CustomerSchemeId = 1},
            });

            moq.Context.Setup(x => x.Proposals).ReturnsDbSet(new List<Core.Entities.Proposal>()
            {
                new(){Id = 1, CustomerId = 1, Deleted = false, Status = EProposal_status.approved}
            });

            moq.Context.Setup(x => x.ProposalCreditItems).ReturnsDbSet(new List<Core.Entities.ProposalCreditItem>()
            {
                new(){Id = 1, ProposalTypeId = 1, ProposalSchemeId = 2, ProposalId = 1, PrevItemId = 2}, // changed this schemeID =1 to 2
                new(){Id = 1, ProposalTypeId = 1, ProposalSchemeId = 1, ProposalId = 1, PrevItemId = 2}, // changed this schemeID =1 to 2
                new(){Id = 2, ProposalTypeId = 2, ProposalSchemeId = 1, ProposalId = 2, ParentItemId = 1}
            });

            moq.Context.Setup(x => x.ProposalTypes).ReturnsDbSet(new List<Core.Entities.ProposalType>()
            {
                new(){Id = 1, Code = EProposalType_code.New}
            });

            moq.Context.Setup(x => x.BgCreditItemRates).ReturnsDbSet(new List<Core.Entities.BgCreditItemRate>()
            {
                new(){Id = 2, ProposalCreditItemId = 1},
                new(){Id = 3, ProposalCreditItemId = 0} // changed this 1 to 0
            });

            moq.Context.Setup(x => x.Collaterals).ReturnsDbSet(new List<Core.Entities.Collateral>()
            {
                new(){Id = 1, ProposalSchemeId = 1, ProposalId = 1 },
                new(){Id = 2, ProposalCreditItemId = 1, ProposalSchemeId = 2, ProposalId = 1 , CollateralTypeId = 1},
                new(){Id = 0, ProposalCreditItemId = 1, ProposalSchemeId = 2, ProposalId = 1 , CollateralTypeId = 1}
            });

            moq.Context.Setup(x => x.CollateralTypes).ReturnsDbSet(new List<Core.Entities.CollateralType>()
            {
                new(){Id = 1, Type = colType} // =================================== dynamic
            });

            moq.Context.Setup(x => x.ChequeCollateralSigners).ReturnsDbSet(new List<Core.Entities.ChequeCollateralSigner>()
            {
                new(){Id = 0}
            });

            moq.Context.Setup(x => x.Conditions).ReturnsDbSet(new List<Core.Entities.Condition>()
            {
                new(){Id = 1, ProposalSchemeId = 2}
            });

            moq.Context.Setup(x => x.ControlLimits).ReturnsDbSet(new List<Core.Entities.ControlLimit>()
            {
                new(){Id = 1, ProposalSchemeId = 2}
            });
            moq.Context.Setup(x => x.ControlLimitItems).ReturnsDbSet(new List<Core.Entities.ControlLimitItem>()
            {
                new(){Id = 1, ControlLimitId = 1, ProposalCreditItemId = 1}
            });

            moq.Context.Setup(x => x.SafteCollateralSigners).ReturnsDbSet(new List<Core.Entities.SafteCollateralSigner>()
            {
                new(){Id = 1, SafteCollateralId = 0}
            });

            moq.Context.Setup(x => x.ContractZamens).ReturnsDbSet(new List<Core.Entities.ContractZamen>()
            {
                new(){Id = 1, ContractCollateralId = 0}
            });

            moq.Context.Setup(x => x.StocksCollateralItems).ReturnsDbSet(new List<Core.Entities.StocksCollateralItem>()
            {
                new(){Id = 1, StocksCollateralId = 0}
            });

            moq.Context.Setup(x => x.VehicleCollateralOwners).ReturnsDbSet(new List<Core.Entities.VehicleCollateralOwner>()
            {
                new(){Id = 1, VehicleCollateralId = 0}
            });

            moq.Context.Setup(x => x.VehicleCollateralEvaluations).ReturnsDbSet(new List<Core.Entities.VehicleCollateralEvaluation>()
            {
                new(){Id = 1, VehicleCollateralId = 0}
            });

            moq.Context.Setup(x => x.BlackListChequeCollaterals).ReturnsDbSet(new List<Core.Entities.BlackListChequeCollateral>()
            {
                new(){Id = 1, ChequeCollateralId = 0}
            });

            moq.Context.Setup(x => x.WhiteListChequeCollaterals).ReturnsDbSet(new List<Core.Entities.WhiteListChequeCollateral>()
            {
                new(){Id = 1, ChequeCollateralId = 0}
            });

            moq.Context.Setup(x => x.Otheracctchqcolatrals).ReturnsDbSet(new List<Core.Entities.Otheracctchqcolatral>()
            {
                new(){Id = 1, ChequeCollateralId = 0}
            });

            moq.Context.Setup(x => x.FixedAssetCollateralOwners).ReturnsDbSet(new List<Core.Entities.FixedAssetCollateralOwner>()
            {
                new(){Id = 1, FixedAssetCollateralId = 0}
            });

            moq.Context.Setup(x => x.Fixasstcolatralevals).ReturnsDbSet(new List<Core.Entities.Fixasstcolatraleval>()
            {
                new(){Id = 1, FixedAssetCollateralId = 0}
            });

            moq.Context.Setup(x => x.EstateCollateralOwners).ReturnsDbSet(new List<Core.Entities.EstateCollateralOwner>()
            {
                new(){Id = 1, EstateCollateralId = 0}
            });

            moq.Context.Setup(x => x.EstateCollateralEvaluations).ReturnsDbSet(new List<Core.Entities.EstateCollateralEvaluation>()
            {
                new(){Id = 1, EstateCollateralId = 0}
            });

            moq.Context.Setup(x => x.EstateCollateralPelaks).ReturnsDbSet(new List<Core.Entities.EstateCollateralPelak>()
            {
                new(){Id = 1, EstateCollateralId = 0}
            });

            var handler = new UpdateFromPrevCreditItemRequestHandler(moq.Context.Object);


            var result = await handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }
    }
}
