using Infrastructure;
using Infrastructure.Repositories.AccountModuleDescRepository;
using Infrastructure.Repositories.BgcollateralRepository;
using Infrastructure.Repositories.BgotherBankRepository;
using Infrastructure.Repositories.BlackListChequeConfigRepositor;
using Infrastructure.Repositories.CbiRegInfoRepository;
using Infrastructure.Repositories.CmntRepository;
using Infrastructure.Repositories.CompanyMembersInfoRepository;
using Infrastructure.Repositories.CompanyRelationRepository;
using Infrastructure.Repositories.CorporateCustomerRepository;
using Infrastructure.Repositories.CreditManagementRepository;
using Infrastructure.Repositories.CurrencyRepository;
using Infrastructure.Repositories.CurrentChequeConfigRepo;
using Infrastructure.Repositories.CustomerRequestDetailRepository;
using Infrastructure.Repositories.CustomerSchemeRepo;
using Infrastructure.Repositories.FinancialRepository;
using Infrastructure.Repositories.DocumentDefRepository;
using Infrastructure.Repositories.LoanMebRepository;
using Infrastructure.Repositories.LoanOtherBanksRepository;
using Infrastructure.Repositories.ManagerDebtRepository;
using Infrastructure.Repositories.ManufacturingCompanyMaterialRepoditory;
using Infrastructure.Repositories.ManufacturingCompanyProductRepository;
using Infrastructure.Repositories.ManufacturingLicenceRepository;
using Infrastructure.Repositories.MenuRoleRepository;
using Infrastructure.Repositories.OrganizationRepository;
using Infrastructure.Repositories.OrganizationTypeLimitRepository;
using Infrastructure.Repositories.ProposalManagerBGRepository;
using Infrastructure.Repositories.ProposalManagerLcRepository;
using Infrastructure.Repositories.ProposalManagerLoanRepositpry;
using Infrastructure.Repositories.ProposalSchemeRepository;
using Infrastructure.Repositories.ServiceCompanyActivityRepository;
using Infrastructure.Repositories.StockRepository;
using Infrastructure.Repositories.TabAccessByOrgRepository;
using Infrastructure.Repositories.TradingCompanyActivityRepository;
using Infrastructure.Repositories.WhiteListChequeConfigRepository;
using Infrastructure.Repositories.WorkplaceInfoRepository;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TanvirArjel.EFCore.GenericRepository;
using Infrastructure.Repositories.IsicGroupRepository;
using Infrastructure.Repositories.BankStaffRepository;
using Infrastructure.Repositories.RiskCustomerGroupItemRepository;
using Infrastructure.Repositories.RiskInfoGroupRepository;

namespace Test.TestCases.UnitOfWorkTests
{
    public class UnitOfWorkTest
    {
        [Fact]
        public void Dispose_CallsDisposeOnAllRepositories()
        {
            // Arrange

            var dBContext = new Mock<DBContext>();
            var repository = new Mock<IRepository>();
            var accountModuleDescRepo = new Mock<IAccountModuleDescRepo>();
            var bankStaffRepo = new Mock<IBankStaffRepo>();
            var cmntRepo = new Mock<ICmntRepo>();
            var cbiRegInfoRepo = new Mock<ICbiRegInfoRepo>();
            var companyRelationRepo = new Mock<ICompanyRelationRepo>();
            var stockRepo = new Mock<IStockRepo>();
            var menuRoleRepo = new Mock<IMenuRoleRepo>();
            var creditManagementRepo = new Mock<ICreditManagementRepo>();
            var organizationRepo = new Mock<IOrganizationRepo>();
            var customerRequestDetailRepo = new Mock<ICustomerRequestDetailRepo>();
            var corporateCustomerRepo = new Mock<ICorporateCustomerRepo>();
            var workplaceInfoRepo = new Mock<IWorkplaceInfoRepo>();
            var customerSchemeRepo = new Mock<ICustomerSchemeRepo>();
            var manufacturingLicenceItemRepo = new Mock<IManufacturingLicenceItemRepo>();
            var companyMembersInfoRepo = new Mock<ICompanyMembersInfoRepo>();
            var organizationTypeLimitRepo = new Mock<IOrganizationTypeLimitRepo>();
            var tabAccessByOrgRepo = new Mock<ITabAccessByOrgRepo>();
            var proposalSchemeRepo = new Mock<IProposalSchemeRepo>();
            var serviceCompanyActivityRepo = new Mock<IServiceCompanyActivityRepo>();
            var tradingCompanyActivityRepo = new Mock<ITradingCompanyActivityRepo>();
            var bgotherBankRepo = new Mock<IBgotherBankRepo>();
            var manufacturingCompanyMaterialRepo = new Mock<IManufacturingCompanyMaterialRepo>();
            var loanMebRepo = new Mock<ILoanMebRepo>();
            var bgcollateralRepo = new Mock<IBgcollateralRepo>();
            var manufacturingCompanyProductRepo = new Mock<IManufacturingCompanyProductRepo>();
            var proposalManagerBGRepo = new Mock<IProposalManagerBGRepo>();
            var proposalManagerLoanRepo = new Mock<IProposalManagerLoanRepo>();
            var proposalManagerLcRepo = new Mock<IProposalManagerLcRepo>();
            var loanOtherBanksRepo = new Mock<ILoanOtherBanksRepo>();
            var blackListChequeConfigRepo = new Mock<IBlackListChequeConfigRepo>();
            var currentChequeConfigRepo = new Mock<ICurrentChequeConfigRepo>();
            var whiteListChequeConfigRepo = new Mock<IWhiteListChequeConfigRepo>();
            var currencyRepo = new Mock<ICurrencyRepo>();
            var financialRepo = new Mock<IFinancialRepo>();
            var managerDebtRepo = new Mock<IManagerDebtRepo>();
            var documentDefRepo = new Mock<IDocumentDefRepo>();
            var isicGroupRepo = new Mock<IIsicGroupRepo>();
            var riskCustomerGroupItemsRepo = new Mock<IRiskCustomerGroupItemRepo>();
            var riskInfoGroup = new Mock<IRiskInfoGroupRepo>();


            var unitOfWork = new Infrastructure.UnitOfWork(
                dBContext.Object,
                repository.Object,
                accountModuleDescRepo.Object,
                bankStaffRepo.Object,
                cmntRepo.Object,
                companyRelationRepo.Object,
                stockRepo.Object,
                menuRoleRepo.Object,
                creditManagementRepo.Object,
                cbiRegInfoRepo.Object,
                organizationRepo.Object,
                customerRequestDetailRepo.Object,
                corporateCustomerRepo.Object,
                workplaceInfoRepo.Object,
                customerSchemeRepo.Object,
                manufacturingLicenceItemRepo.Object,
                companyMembersInfoRepo.Object,
                organizationTypeLimitRepo.Object,
                tabAccessByOrgRepo.Object,
                serviceCompanyActivityRepo.Object,
                proposalSchemeRepo.Object,
                tradingCompanyActivityRepo.Object,
                bgotherBankRepo.Object,
                manufacturingCompanyMaterialRepo.Object,
                loanMebRepo.Object,
                bgcollateralRepo.Object,
                manufacturingCompanyProductRepo.Object,
                proposalManagerLcRepo.Object,
                loanOtherBanksRepo.Object,
                proposalManagerBGRepo.Object,
                currencyRepo.Object,
                proposalManagerLoanRepo.Object,
                blackListChequeConfigRepo.Object,
                currentChequeConfigRepo.Object,
                whiteListChequeConfigRepo.Object,
                financialRepo.Object,
                managerDebtRepo.Object,
                documentDefRepo.Object,
                isicGroupRepo.Object,
                riskInfoGroup.Object,
                riskCustomerGroupItemsRepo.Object
            );

            // Act
            unitOfWork.Dispose();
            var a = unitOfWork.CbiRegInfoRepo;
            var c = unitOfWork.AccountModuleDescRepo;
            var d = unitOfWork.CbiRegInfoRepo;
            var e = unitOfWork.CompanyRelationRepo;
            var g = unitOfWork.StockRepo;
            var h = unitOfWork.MenuRoleRepo;
            var i = unitOfWork.CmntRepo;
            var j = unitOfWork.CreditManagementRepo;
            var k = unitOfWork.OrganizationRepo;


            var a1 = unitOfWork.CustomerRequestDetailRepo;
            var b1 = unitOfWork.CorporateCustomerRepo;
            var c1 = unitOfWork.WorkplaceInfoRepo;
            var d1 = unitOfWork.CustomerSchemeRepo;
            var e1 = unitOfWork.ManufacturingLicenceItemRepo;
            var f1 = unitOfWork.CompanyMembersInfoRepo;
            var g1 = unitOfWork.OrganizationTypeLimitRepo;
            var h1 = unitOfWork.TabAccessByOrgRepo;
            var i1 = unitOfWork.ProposalSchemeRepo;
            var j1 = unitOfWork.LoanMebRepo;
            var k1 = unitOfWork.ServiceCompanyActivityRepo;

            var a2 = unitOfWork.TradingCompanyActivityRepo;
            var b2 = unitOfWork.BgotherBankRepo;
            var c2 = unitOfWork.ManufacturingCompanyMaterialRepo;
            var d2 = unitOfWork.BgcollateralRepo;
            var e2 = unitOfWork.ManufacturingCompanyProductRepo;
            var f2 = unitOfWork.ProposalManagerBGRepo;
            var g2 = unitOfWork.ProposalManagerLoanRepo;
            var h2 = unitOfWork.ProposalManagerLcRepo;
            var i2 = unitOfWork.LoanOtherBanksRepo;
            var j2 = unitOfWork.BlackListChequeConfigRepo;
            var k2 = unitOfWork.CurrentChequeConfigRepo;

            var a3 = unitOfWork.WhiteListChequeConfigRepo;
            var b3 = unitOfWork.FinancialRepo;
            var c3 = unitOfWork.ManagerDebtRepo;
            var d3 = unitOfWork.CurrencyRepo;
            var e3 = unitOfWork.DocumentDefRepo;
            var f3 = unitOfWork.IsicGroupRepo;
            var g3 = unitOfWork.RiskCustomerGroupItemRepo;
            var h3 = unitOfWork.RiskInfoGroupRepo;

            // Assert
            dBContext.Verify(x => x.Dispose(), Times.Never);
        }
    }
}
