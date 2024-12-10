using Application.Services.FinancialYearInfoService;
using FluentValidation.TestHelper;

namespace Test.TestCases.Services.FinancialYearInfoTest
{
    public class ValidationsTest
    {
        [Fact]
        public void AddFinancialYearInfoValidatorTest()
        {
           var  _validator = new AddFinancialYearInfoValidator();
            var request = new AddFinancialYearInfoRequest
            {
                FromDate = new DateTime(2022, 1, 1),
                ToDate = new DateTime(2021, 12, 31)
            };

            _validator.TestValidate( request);

        }
        [Fact]
        public void UpdateFinancialYearInfoValidatorTest()
        {
            var _validator = new UpdateFinancialYearInfoValidator();
            var request = new UpdateFinancialYearInfoRequest
            {
                Audited = true,
                FromDate = new DateTime(2022, 1, 1),
                ToDate = new DateTime(2021, 12, 31)
            };

            _validator.TestValidate(request);

        }
        [Fact]
        public void checkDateTest()
        {
            var result = ValidationChecks.checkDate(
                new Dates()
                {
                    FromDate = DateTime.Now.AddDays(-5),
                    ToDate = DateTime.Now
                });
            Assert.True(result);
        }
        [Fact]
        public void checkDateTestlse()
        {
            var result = ValidationChecks.checkDate(
                new Dates()
                {
                    FromDate = DateTime.Now.AddDays(5),
                    ToDate = DateTime.Now
                });
            Assert.False(result);
        }
        [Fact]
        public void checkTickedTest()
        {
            var result = ValidationChecks.checkTicked(
                new Ticked()
                {
                    Audited = true,
                    Auditor = "Auditor"
                });
            Assert.True(result);
        }
        [Fact]
        public void checkTickedTestElse()
        {
            var result = ValidationChecks.checkTicked(
                new Ticked()
                {
                    Audited = false,
                    Auditor = "Auditor"
                });
            Assert.True(result);
        }
        [Fact]
        public void checkTickedTestelse()
        {
            var result = ValidationChecks.checkTicked(
                new Ticked()
                {
                    Audited = true,
                    Auditor = null
                });
            Assert.False(result);
        }

    }
}
