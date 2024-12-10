using Application.Services.Report.VM;
using Core.Enums;
using Core.Utils;

namespace Test.TestCases.Services.ReportTest
{
    public class GetProposalInfoReportVMTests
    {
        [Fact]
        public void CorporateCustomerReportVM_ShouldReturnCorrectEnumDescription()
        {
            // Arrange
            var corporateCustomer = new CorporateCustomerReportVM
            {
                CorporateType = ECorporateCustomer_corporateType.Service // Example enum value
            };

            // Act
            var corporateTypeStr = corporateCustomer.CorporateTypeStr;

            // Assert
            Assert.Equal("خدماتی", corporateTypeStr);
        }

        [Fact]
        public void WorkPlaceInfoReportVM_ShouldInitializeCorrectly()
        {
            // Arrange
            var workplaceInfo = new WorkPlaceInfoReportVM
            {
                City = "Sample City",
                Address = "123 Main St",
                Province = "Sample Province",
                OwnershipType = EOccupationPlace_ownershipType.Leased
            };

            // Assert
            Assert.Equal("Sample City", workplaceInfo.City);
            Assert.Equal("123 Main St", workplaceInfo.Address);
            Assert.Equal(EOccupationPlace_ownershipType.Leased, workplaceInfo.OwnershipType);
        }

        [Fact]
        public void MebAccountTurnoverReportVM_ShouldReturnRoundedValues()
        {
            // Arrange
            var turnoverReport = new MebAccountTurnoverReportVM
            {
                AvgBalance = "1234.56",
                FinalBalance = "7890.12",
                CreditTurnover = "345.78",
                DebitTurnover = "987.65"
            };

            // Act & Assert
            Assert.Equal("1235", turnoverReport.AvgBalance); // Rounded to nearest integer
            Assert.Equal("7890", turnoverReport.FinalBalance);
            Assert.Equal("346", turnoverReport.CreditTurnover);
            Assert.Equal("988", turnoverReport.DebitTurnover);
        }

        [Fact]
        public void ReportComment_TypeStr_ShouldReturnEnumDescription()
        {
            // Arrange
            var reportComment = new ReportComment
            {
                Type = (int)ECompanyFinancialInfo_type.cashflow, // Example enum value,
                TypeStr = ECompanyFinancialInfo_type.cashflow.GetEnumDescription()
            };

            // Act
            var typeStr = reportComment.TypeStr;

            // Assert
            Assert.Equal("صورت جریان وجوه نقد", typeStr);
        }

        [Fact]
        public void LcmebDetails_ShouldStoreCollateralAmountsCorrectly()
        {
            // Arrange
            var lcmebDetails = new LcmebDetails
            {
                Cheque = 1000.0,
                Deposite = 500.0,
                Other = 300.0
            };

            // Assert
            Assert.Equal(1000.0, lcmebDetails.Cheque);
            Assert.Equal(500.0, lcmebDetails.Deposite);
            Assert.Equal(300.0, lcmebDetails.Other);
        }
    }
}
