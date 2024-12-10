using Application.Services.RiskCalculation;
using Core.Entities;
using Core.GenericResultModel;
using Core.ViewModel.RiskCalculation;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.RiskCalculationService
{
    public class GetAllRiskCalculationRequestHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsApiResult_WhenlCorporateCustomersIdIsValid()
        {
            // Arrange

            var moq = Helper.MoqHelper.GetUnitOfWorkMoqCollection();
            
            moq.Context.Setup(x => x.Proposals).ReturnsDbSet([new Core.Entities.Proposal() { Id = 321, CustomerId = 128834 }]);
            moq.Context.Setup(x => x.Customers).ReturnsDbSet([new Core.Entities.Customer() {Id = 128834 }]);
            moq.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet([]);
            moq.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() { Id = 128834, IsicGroupId = 40000, IsicSubGroupId = 67059 }]);
            moq.Context.Setup(x => x.RiskCustomerGroupItems).ReturnsDbSet([new() { Id = 123, IsicGroupId = 40000, IsicSubGroupId = 67059, RiskCustomerGroupId = 132452 }]);
            moq.Context.Setup(x => x.RiskCustomerGroups).ReturnsDbSet([new() { Id = 132452,  Title = "مشتری صنعتی" }]);
            moq.Context.Setup(x => x.PrimaryRisks).ReturnsDbSet([new() { Id = 133895, Category = Core.Enums.EPrimaryRisk_category.C, Deleted = false , Last = true, Parent = null ,  RiskCustomerGroupId = 132452, Title = "بررسی مواد دارویی" , Ver = 2 ,Weight = 55},
            new() { Id = 133896, Category = Core.Enums.EPrimaryRisk_category.C, Deleted = false , Last = true, ParentId = null ,  RiskCustomerGroupId = 132452, Title = "تست" , Ver = 2 ,Weight = 100},
            new() { Id = 133897, Category = Core.Enums.EPrimaryRisk_category.C, Deleted = false , Last = true, ParentId = null ,  RiskCustomerGroupId = 132452, Title = "تستhjت" , Ver = 2 ,Weight = 0},
            new() { Id = 133898, Category = Core.Enums.EPrimaryRisk_category.C, Deleted = false , Last = true, ParentId = null ,  RiskCustomerGroupId = 132452, Title = "سوالات کمی" , Ver = 2 ,Weight = 0},
            new() { Id = 133899, Category = Core.Enums.EPrimaryRisk_category.C, Deleted = false , Last = true, ParentId = null ,  RiskCustomerGroupId = 132452, Title = "سوالات کیفی" , Ver = 2 ,Weight = 0},
            new() { Id = 133900, Category = Core.Enums.EPrimaryRisk_category.C, Deleted = false , Last = true, ParentId = 133895 , RiskCustomerGroupId = 132452, RiskInfoGroupId=51838, Ver = 2 ,Weight = 100,Customizable= true  },
            new() { Id = 133903, Category = Core.Enums.EPrimaryRisk_category.C, Deleted = false , Last = true, ParentId = 133898 , RiskCustomerGroupId = 132452, RiskInfoGroupId=null , FormulaId = 40841 , Ver = 2 ,Weight = 20,Customizable= false},]);
            
            moq.Context.Setup(x => x.FinancialValues).ReturnsDbSet([]);

            moq.Context.Setup(x=>x.RiskInfoGroups).ReturnsDbSet([new() { Id = 51838 , Cutoffjson = "[{\"value\":1}]" , Title = "چک برگشتی" }]);
            moq.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet([new() { Id = 40841, Cutoff2json = "[{\"value\":1}]", Cutoffjson = "[{\"value\":1}]", Title = "اهرم مال", Formula = "[{\"id\":10736,\"title\":\"جمع كل بدهيها\",\"year\":{\"id\":0,\"title\":\"آخرين سال\"},\"operand\":true},{\"title\":\"/\",\"operator\":true},{\"id\":10746,\"title\":\"جمع حقوق صاحبان سهام\",\"year\":{\"id\":0,\"title\":\"آخرين سال\"},\"operand\":true}]" }]);



            var handler = new GetAllRiskCalculationRequestHandler(moq.UnitOfWork.Object);

            var request = new GetAllRiskCalculationRequest { ProposalId = 321, ver = null };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ApiResult<jsonListVM>>(result);
        }

        [Fact]
        public async Task Handle_ReturnsApiResult_WhenlIndividualCustomersIdIsValid()
        {
            // Arrange

            var moq = Helper.MoqHelper.GetUnitOfWorkMoqCollection();

            moq.Context.Setup(x => x.Proposals).ReturnsDbSet([new Core.Entities.Proposal() { Id = 321, CustomerId = 53397, Deleted = false }]);
            moq.Context.Setup(x => x.Customers).ReturnsDbSet([new Core.Entities.Customer() { Id = 53397 }]);
            moq.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet([]);
            moq.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet([new() { Id = 53397 }]);

            moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet([new() { Id  = 696969, ProposalId = 321 }]);
            moq.Context.Setup(x => x.CustomerSchemes).ReturnsDbSet([new() { Id =858585, ProposalSchemeId = 696969}]);
            moq.Context.Setup(x => x.OccupationInformations).ReturnsDbSet([new() { CustomerSchemeId = 858585, IsicGroupId = 40000 }]);

            moq.Context.Setup(x => x.RiskCustomerGroupItems).ReturnsDbSet([new() { Id = 123, IsicGroupId = 40000, IsicSubGroupId = 67059, RiskCustomerGroupId = 132452 }]);
            moq.Context.Setup(x => x.RiskCustomerGroups).ReturnsDbSet([new() { Id = 132452, Title = "مشتری صنعتی" }]);
            moq.Context.Setup(x => x.PrimaryRisks).ReturnsDbSet([
            new() { Id = 133895, Category = Core.Enums.EPrimaryRisk_category.C, Deleted = false , Last = true, Parent = null ,  RiskCustomerGroupId = 132452, Title = "بررسی مواد دارویی" , Ver = 2 ,Weight = 55},
            new() { Id = 133896, Category = Core.Enums.EPrimaryRisk_category.I, Deleted = false , Last = true, ParentId = null ,  RiskCustomerGroupId = 132452, Title = "تست" , Ver = 2 ,Weight = 100},
            new() { Id = 133897, Category = Core.Enums.EPrimaryRisk_category.C, Deleted = false , Last = true, ParentId = null ,  RiskCustomerGroupId = 132452, Title = "تستhjت" , Ver = 2 ,Weight = 0},
            new() { Id = 133898, Category = Core.Enums.EPrimaryRisk_category.C, Deleted = false , Last = true, ParentId = null ,  RiskCustomerGroupId = 132452, Title = "سوالات کمی" , Ver = 2 ,Weight = 0},
            new() { Id = 133899, Category = Core.Enums.EPrimaryRisk_category.C, Deleted = false , Last = true, ParentId = null ,  RiskCustomerGroupId = 132452, Title = "سوالات کیفی" , Ver = 2 ,Weight = 0},
            new() { Id = 133900, Category = Core.Enums.EPrimaryRisk_category.C, Deleted = false , Last = true, ParentId = 133895 , RiskCustomerGroupId = 132452, RiskInfoGroupId=51838, Ver = 2 ,Weight = 100,Customizable= true  },
            new() { Id = 133903, Category = Core.Enums.EPrimaryRisk_category.C, Deleted = false , Last = true, ParentId = 133898 , RiskCustomerGroupId = 132452, RiskInfoGroupId=null , FormulaId = 40841 , Ver = 2 ,Weight = 20,Customizable= false},
            new() { Id = 133903, Category = Core.Enums.EPrimaryRisk_category.C, Deleted = false , Last = true, ParentId = 133898 , RiskCustomerGroupId = null, RiskInfoGroupId=null , FormulaId = 40841 , Ver = 2 ,Weight = 20,Customizable= false},
            new() { Id = 133903, Category = Core.Enums.EPrimaryRisk_category.I, Deleted = false , Last = true, ParentId = 133898 , RiskCustomerGroupId = null, RiskInfoGroupId=null , FormulaId = 40841 , Ver = 2 ,Weight = 20,Customizable= false},
            ]);

            moq.Context.Setup(x => x.FinancialValues).ReturnsDbSet([]);

            moq.Context.Setup(x => x.RiskInfoGroups).ReturnsDbSet([new() { Id = 51838, Cutoffjson = "[{\"value\":1}]", Title = "چک برگشتی" }]);
            moq.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet([new() { Id = 40841, Cutoff2json = "[{\"value\":1}]", Cutoffjson = "[{\"value\":1}]", Title = "اهرم مال", Formula = "[{\"id\":10736,\"title\":\"جمع كل بدهيها\",\"year\":{\"id\":0,\"title\":\"آخرين سال\"},\"operand\":true},{\"title\":\"/\",\"operator\":true},{\"id\":10746,\"title\":\"جمع حقوق صاحبان سهام\",\"year\":{\"id\":0,\"title\":\"آخرين سال\"},\"operand\":true}]" }]);



            var handler = new GetAllRiskCalculationRequestHandler(moq.UnitOfWork.Object);

            var request = new GetAllRiskCalculationRequest { ProposalId = 321, ver = null };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ApiResult<jsonListVM>>(result);
        }


    }
}
