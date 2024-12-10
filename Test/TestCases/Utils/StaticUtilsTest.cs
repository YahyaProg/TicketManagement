using Application.Services.ProvinceService;
using Core.Entities;
using Core.Enums;
using Core.GenericResultModel;
using Core.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;

namespace Test.TestCases.Utils
{
    public class StaticUtilsTest
    {
        [Fact]
        public void FromUnixTime_ShouldConvertUnixTimeToDateTime()
        {
            // Arrange
            long unixTime = 1625097600000; // 2021-07-01 00:00:00 UTC

            // Act
            DateTime result = StaticUtils.FromUnixTime(unixTime);

            // Assert
            Assert.Equal(result, new DateTime(2021, 7, 1, 0, 0, 0, DateTimeKind.Utc));
        }

        [Fact]
        public void StripHTML_ShouldRemoveHtmlTagsFromString()
        {
            // Arrange
            string input = "<div>Hello <b>world</b>!</div>";
            string expected = "Hello world!";

            // Act
            string result = input.StripHTML();

            // Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void GenerateResult_ShouldCreateApiResultFromModelStateErrors()
        {
            // Arrange
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Name", "Name is required.");
            modelState.AddModelError("Age", "Age must be a positive integer.");

            // Act
            ApiResult result = modelState.GenerateResult();

            // Assert
            Assert.Equal(406, result.Code);
            Assert.Contains("Name is required.", result.ValidationErrors.First(x => x.PropertyName == "Name").Errors);
            Assert.Contains("Age must be a positive integer.", result.ValidationErrors.First(x => x.PropertyName == "Age").Errors);
        }


        [Fact]
        public void ToShamsiDate_ShouldConvertDateToPersianCalendarString()
        {
            // Arrange
            DateTime date = new(2021, 7, 1, 15, 30, 0);

            // Act
            string result = StaticUtils.ToShamsiDate(date);

            // Assert
            Assert.Equal("1400/04/10", result); // Assuming the date converts to this Shamsi date
        }


        [Fact]
        public void ToShamsiDateTime_ShouldConvertDateTimeToPersianCalendarString()
        {
            // Arrange
            DateTime date = new(2021, 7, 1, 15, 30, 0);

            // Act
            string result = StaticUtils.ToShamsiDateTime(date);

            // Assert
            Assert.Equal("1400/04/10 15:30", result); // Assuming the date converts to this Shamsi date
        }

        [Fact]
        public void CalculateFormula_ShouldEvaluateMathExpression()
        {
            // Arrange
            string formula = "A + B * C";
            var values = new[]
            {
                new KeyValuePair<string, double>("A", 2),
                new KeyValuePair<string, double>("B", 3),
                new KeyValuePair<string, double>("C", 4),
            };

            // Act
            double result = formula.CalculateFormula(values);

            // Assert
            Assert.Equal(14, result);// Expected: 2 + 3 * 4 = 14
        }

        [Fact]
        public void CalculateWithCondition_ShouldReturnTrueBranchResult_WhenConditionIsMet()
        {
            // Arrange
            string formula = "ifelse(A > 5,A + B,B + C)";
            var values = new List<KeyValuePair<string, double>>
            {
                new KeyValuePair<string, double>("A", 6),
                new KeyValuePair<string, double>("B", 2),
                new KeyValuePair<string, double>("C", 3)
            };

            // Act
            double result = formula.CalculateWithCondition(values, false);

            // Assert
            Assert.Equal(8, result); // 6 + 2
        }

        [Fact]
        public void MobileNormalization_ShouldFormatMobileNumbersCorrectly()
        {
            // Arrange
            string mobile = "9123456789";

            // Act
            string result = mobile.MobileNormalization();

            // Assert
            Assert.Equal("09123456789", result);
        }

        [Fact]
        public void MobileNormalization_ShouldReturnNullWhenEmpty()
        {
            // Arrange
            string mobile = "";

            // Act
            string result = mobile.MobileNormalization();

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public void SeparateLanValue_ShouldReturnFirstValue_WhenAcceptLanIsFa()
        {
            // Arrange
            string input = "Persian,English";
            string expected = "Persian";

            // Act
            string result = input.SeparateLanValue("fa");

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetDiffMonths_ShouldReturnDifferenceInMonths()
        {
            // Arrange
            DateTime date1 = new(2023, 10, 1);
            DateTime date2 = new(2022, 1, 1);

            // Act
            int result = StaticUtils.GetDiffMonths(date1, date2);

            // Assert
            Assert.Equal(21, result);
        }

        [Fact]
        public void CheckCondition_ShouldReturnTrueForValidCondition()
        {
            bool expectedResult = true;
            // Arrange
            string condition = "X > Y";
            var values = new List<KeyValuePair<string, double>>
            {
                new("X", 10),
                new("Y", 5)
            };

            // Act
            bool result = condition.CheckCondition(values);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void ConvertToDropDownItem_ShouldReturnCorrectDropDownResponseVM()
        {
            // Arrange
            var entity = new Province
            {
                Id = 1,
                Title = "Mazandaran",
                Deleted = false
            };
            var expectedKey = 1;
            var expectedValue = "Mazandaran";

            // Act
            var result = entity.ConvertToDropDownItem<Province, DropDownProvinceRequest, long?>();

            // Assert
            Assert.Equal(expectedKey, result.Key);
            Assert.Equal(expectedValue, result.Value);
        }

        [Fact]
        public void CheckCondition_ShouldReturnFalseForInvalidCondition()
        {
            bool expectedResult = false;
            // Arrange
            string condition = "A <= B";
            var values = new List<KeyValuePair<string, double>>
        {
            new("A", 2),
            new("B", 1)
        };

            // Act
            bool result = condition.CheckCondition(values);

            // Assert
            Assert.Equal(result, expectedResult);
        }


        [Fact]
        public void CheckCondition_ShouldReturnFalseForNonMatchingCondition()
        {
            bool expectedResult = false;
            // Arrange
            string condition = "X * Y < 15";
            var values = new List<KeyValuePair<string, double>>
        {
            new("X", 3),
            new("Y", 5)
        };

            // Act
            bool result = condition.CheckCondition(values);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void GetFirstCharsOfGuid_ShouldReturnEightCharacterGuidSubstring()
        {
            int expectedResult = 8;
            // Act
            string result = StaticUtils.GetFirstCharsOfGuid();

            // Assert
            Assert.Equal(result.Length, expectedResult);

        }

        [Fact]
        public void GetRandomNumber_ShouldReturnValueWithinRange()
        {
            // Arrange
            int min = 10;
            int max = 20;

            // Act
            long result = StaticUtils.GetRandomNumber(min, max);

            // Assert
            Assert.InRange(result, min, max - 1);
        }


        [Fact]
        public void SplitCalFinancialFormula_ShouldReturnFormulaTermsWithoutOperators()
        {
            // Arrange
            string formula = "A + B * C / D - E";

            // Act
            string[] result = formula.SplitCalcFinancialFormula();

            // Assert
            Assert.Equal(result, ["A ", " B ", " C ", " D ", " E"]);
        }

        [Fact]
        public void SplitCalFinancialFormula_ShouldReturnEmptyArrayForNullInput()
        {
            // Arrange
            string formula = null;

            // Act
            string[] result = formula.SplitCalcFinancialFormula();

            // Assert
            Assert.Empty(result);
        }


        [Fact]
        public void SetBranches_ShouldCacheBranchesByUserId()
        {
            // Arrange
            var cacheMock = new MemoryCache(new MemoryCacheOptions());
            string userId = "user123";
            var branches = new List<string> { "Branch1", "Branch2" };

            // Act
            cacheMock.SetBranches(userId, branches);
            cacheMock.SetBranches(userId, branches);

            // Assert
            Assert.Equal(cacheMock.Get<List<string>>($"{userId}-branches"), branches);
        }

        [Fact]
        public void GetBranches_ShouldReturnCachedBranchesByUserId()
        {
            // Arrange
            var cacheMock = new MemoryCache(new MemoryCacheOptions());
            string userId = "user123";
            var branches = new List<string> { "Branch1", "Branch2" };
            cacheMock.Set($"{userId}-branches", branches);

            // Act
            var result = cacheMock.GetBranches(userId);

            // Assert
            Assert.Equal(result, branches);
        }

        [Fact]
        public void GetBranches_ShouldReturnNullIfUserIdNotCached()
        {
            // Arrange
            var cacheMock = new MemoryCache(new MemoryCacheOptions());
            string userId = "userNotCached";

            // Act
            var result = cacheMock.GetBranches(userId);

            // Assert
            Assert.Null(result);
        }

        #region  AutoCalculateRiskInfo 
        [Fact]
        public void AutoCalculateRiskInfo_ShouldReturnCorrectResult_ForHistoryOfCreditTransactions()
        {
            // Arrange
            double value = 6;
            double requestedAmount = 1000;

            // Act
            double result = StaticUtils.AutoCalculateRiskInfo(ERiskInfoItem_autoCalculate.HISTORY_OF_CREDIT_TRANSACTIONS, value, requestedAmount);

            // Assert
            Assert.Equal(3.9, result); // Value >= 5
        }
        [Theory]
        [InlineData(ERiskInfoItem_autoCalculate.HISTORY_OF_CREDIT_TRANSACTIONS, 5, 0, 3.9)]
        [InlineData(ERiskInfoItem_autoCalculate.HISTORY_OF_CREDIT_TRANSACTIONS, 0, 0, 0)]
        [InlineData(ERiskInfoItem_autoCalculate.LAST_BALANCE_SCORE_IN_PARSIAN, 10, 100, 4.2)]
        [InlineData(ERiskInfoItem_autoCalculate.CREDITOR_LAST_TRANSACTIONS, 100, 1, 3.6)]
        [InlineData(ERiskInfoItem_autoCalculate.RETURNING_CHEQUE_AMOUNT_IN_PARSIAN, 0, 0, 3.9)]
        [InlineData(ERiskInfoItem_autoCalculate.RETURNING_CHEQUE_AMOUNT_OTHER_BANKS, 100, 10, -39)]
        [InlineData(ERiskInfoItem_autoCalculate.NON_CURRENT_CUSTOMER_DEBTS, 0, 0, 4.8)]
        [InlineData(ERiskInfoItem_autoCalculate.NON_CURRENT_CUSTOMER_DEBTS, 100, 10, -48)]
        public void AutoCalculateRiskInfo_ShouldReturnScaledResult_ForHistoryOfCreditTransactions(ERiskInfoItem_autoCalculate en, double value, double requestedAmount, double expected)
        {
            // Arrange

            // Act
            double result = StaticUtils.AutoCalculateRiskInfo(en, value, requestedAmount);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AutoCalculateRiskInfo_ShouldCapResult_ForLastBalanceScoreInParsian()
        {
            // Arrange
            double value = 50;
            double requestedAmount = 1000;

            // Act
            double result = StaticUtils.AutoCalculateRiskInfo(ERiskInfoItem_autoCalculate.LAST_BALANCE_SCORE_IN_PARSIAN, value, requestedAmount);

            // Assert
            Assert.Equal(2.1, result); // Capped at 2.1
        }

        [Fact]
        public void AutoCalculateRiskInfo_ShouldHandleZeroValue_ForReturningChequeAmount()
        {
            // Arrange
            double value = 0;
            double requestedAmount = 1000;

            // Act
            double result = StaticUtils.AutoCalculateRiskInfo(ERiskInfoItem_autoCalculate.RETURNING_CHEQUE_AMOUNT_IN_PARSIAN, value, requestedAmount);

            // Assert
            Assert.Equal(3.9, result); // Value == 0
        }

        [Fact]
        public void AutoCalculateRiskInfo_ShouldCalculateNegativeResult_ForReturningChequeAmount()
        {
            // Arrange
            double value = 200;
            double requestedAmount = 1000;

            // Act
            double result = StaticUtils.AutoCalculateRiskInfo(ERiskInfoItem_autoCalculate.RETURNING_CHEQUE_AMOUNT_IN_PARSIAN, value, requestedAmount);

            // Assert
            Assert.Equal(-0.78, result, 2); // -(200 / 1000) * 3.9
        }

        [Fact]
        public void AutoCalculateRiskInfo_ShouldReturnZero_ForUnknownCalculationType()
        {
            // Arrange
            double value = 50;
            double requestedAmount = 1000;

            // Act
            double result = StaticUtils.AutoCalculateRiskInfo((ERiskInfoItem_autoCalculate)100, value, requestedAmount);

            // Assert
            Assert.Equal(0, result); // Default case
        }

        #endregion

        [Fact]
        public void ToShamsi_Success()
        {
            var res = StaticUtils.ToShamsiDate(DateTime.Now);
            Assert.NotNull(res);
        }

        [Theory]
        [InlineData("fa")]
        [InlineData("en")]
        public void SeparateLanValue_Success(string accept_lan)
        {
            var res = StaticUtils.SeparateLanValue("1,2", accept_lan);
            Assert.NotNull(res);
        }

        [Fact]
        public void GetFullName_ShouldReturnFullName_WhenModelIsValid()
        {
            // Arrange
            var staff = new BankStaff
            {
                FirstName = "John",
                LastName = "Doe"
            };

            // Act
            var result = staff.GetFullName();

            // Assert
            Assert.Equal("John Doe", result);
        }


        [Fact]
        public void CalculateWithCondition_ConditionTrue_ReturnsFirstResult()
        {
            // Arrange
            string formule = "ifelse(A > 5 , B + 2 , C - 1)";
            var values = new List<KeyValuePair<string, double>>
        {
            new KeyValuePair<string, double>("A", 6),
            new KeyValuePair<string, double>("B", 10),
            new KeyValuePair<string, double>("C", 4)
        };

            // Act
            double result = formule.CalculateWithCondition(values, false);

            // Assert
            Assert.Equal(12, result); // Expecting B + 2 = 10 + 2
        }

        [Fact]
        public void GetRandomNumber_Success()
        {
            var res = StaticUtils.GetRandomNumber(1, 5);
            Assert.True(res >= 1 && res <= 5);
        }

        [Fact]
        public void GetFullName()
        {
            var staff = new BankStaff { FirstName = "majid", LastName = "mohammadi" };
            var result = StaticUtils.GetFullName(staff);
            Assert.Equal("majid mohammadi", result);
        }

        [Fact]
        public void ConvertElseIf_ShouldConvertProperly()
        {
            // Arrange
            string input = "ifelse(x > 10, 'Yes', 'No')";
            string expectedOutput = "(x > 10) ? 'Yes' : 'No'";

            // Act
            string result = input.ConvertElseIf();

            // Assert
            Assert.Equal(expectedOutput, result);
        }


        [Fact]
        public void ConvertElseIf_ShouldReturnSameForNonMatchingInput()
        {
            // Arrange
            string input = "x + y";
            string expectedOutput = "x + y";

            // Act
            string result = input.ConvertElseIf();

            // Assert
            Assert.Equal(expectedOutput, result);
        }

        [Fact]
        public void GetCode_ShouldReturnParamCodeIfNotNull()
        {
            // Arrange
            var relations = new IndividualRelations { ParamCode = "Code123", UsedFormulaCode = "Formula456" };

            // Act
            string result = relations.GetCode();

            // Assert
            Assert.Equal("Code123", result);
        }

        [Fact]
        public void GetCode_ShouldReturnUsedFormulaCodeIfParamCodeIsNull()
        {
            // Arrange
            var relations = new IndividualRelations { ParamCode = null, UsedFormulaCode = "Formula456" };

            // Act
            string result = relations.GetCode();

            // Assert
            Assert.Equal("Formula456", result);
        }

        [Fact]
        public void Test_HistoryOfCreditTransactions()
        {
            // Arrange
            var autoCalculate = ERiskInfoItem_autoCalculate.HISTORY_OF_CREDIT_TRANSACTIONS;
            double value = 20; // Example value
            double requestedAmount = 100; // Example requested amount

            // Act
            var result = StaticUtils.AutoCalculateRiskInfo_Individual(autoCalculate, value, requestedAmount);

            // Assert
            Assert.Equal(15, result); // Min(20 / 5, 1) * 15 = 15
        }

        [Fact]
        public void Test_CreditorLastTransactions_LowValue()
        {
            // Arrange
            var autoCalculate = ERiskInfoItem_autoCalculate.CREDITOR_LAST_TRANSACTIONS;
            double value = 100; // Example value
            double requestedAmount = 1000; // Example requested amount

            // Act
            var result = StaticUtils.AutoCalculateRiskInfo_Individual(autoCalculate, value, requestedAmount);

            // Assert
            Assert.Equal(0.08, result); // Min(100 / (1000 * 10), 2) * 8 = 8
        }

        [Fact]
        public void Test_CreditorLastTransactions_HighValue()
        {
            // Arrange
            var autoCalculate = ERiskInfoItem_autoCalculate.CREDITOR_LAST_TRANSACTIONS;
            double value = 30000; // Example value
            double requestedAmount = 1000; // Example requested amount

            // Act
            var result = StaticUtils.AutoCalculateRiskInfo_Individual(autoCalculate, value, requestedAmount);

            // Assert
            Assert.Equal(16, result); // Min(30000 / (1000 * 10), 2) * 8 = 16
        }

        [Fact]
        public void Test_ReturningChequeAmountInParsian_NoValue()
        {
            // Arrange
            var autoCalculate = ERiskInfoItem_autoCalculate.RETURNING_CHEQUE_AMOUNT_IN_PARSIAN;
            double value = 0;
            double requestedAmount = 1000;

            // Act
            var result = StaticUtils.AutoCalculateRiskInfo_Individual(autoCalculate, value, requestedAmount);

            // Assert
            Assert.Equal(8, result); // Value == 0 => result = 8
        }

        [Fact]
        public void Test_ReturningChequeAmountInParsian_PositiveValue()
        {
            // Arrange
            var autoCalculate = ERiskInfoItem_autoCalculate.RETURNING_CHEQUE_AMOUNT_IN_PARSIAN;
            double value = 100;
            double requestedAmount = 1000;

            // Act
            var result = StaticUtils.AutoCalculateRiskInfo_Individual(autoCalculate, value, requestedAmount);

            // Assert
            Assert.Equal(-0.8, result, 0.0001); // -1 * ((100 / 1000) * 8) = -0.8
        }

        [Fact]
        public void Test_NonCurrentCustomerDebts_PositiveValue()
        {
            // Arrange
            var autoCalculate = ERiskInfoItem_autoCalculate.NON_CURRENT_CUSTOMER_DEBTS;
            double value = 200;
            double requestedAmount = 1000;

            // Act
            var result = StaticUtils.AutoCalculateRiskInfo_Individual(autoCalculate, value, requestedAmount);

            // Assert
            Assert.Equal(-1.6, result, 0.0001); // -1 * ((200 / 1000) * 8) = -1.6
        }


    }
}
