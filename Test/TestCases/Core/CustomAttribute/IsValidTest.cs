using Core.CustomAttribute;

namespace Test.TestCases.CoreTest.CustomAttributeTest;

public class IsValidAttributeTests
{
 
    [Fact]
    public void CheckNationalId_ReturnsCorrectValidation()
    {
        // Valid case
        Assert.False(IsValidAttribute.CheckNationalId(ECustomeValidation.NationalId, "4882419521"));

        // Invalid case
        Assert.True(IsValidAttribute.CheckNationalId(ECustomeValidation.NationalId, "12345678"));
    }

    [Fact]
    public void CheckCorpId_ReturnsCorrectValidation()
    {
        // Valid case
        Assert.True(IsValidAttribute.CheckCorpId(ECustomeValidation.CorpId, "00850798911"));

    }

    [Fact]
    public void CheckPostalCode_ReturnsCorrectValidation()
    {

        // Valid case
        Assert.True(IsValidAttribute.CheckPostalCode(ECustomeValidation.PostalCode, "4713245566"));
    }

    [Fact]
    public void CheckFax_ReturnsCorrectValidation()
    {
        // Valid case
        Assert.False(IsValidAttribute.CheckFax(ECustomeValidation.Fax, "01234567890"));

        // Invalid case
        Assert.True(IsValidAttribute.CheckFax(ECustomeValidation.Fax, "1234"));
    }

    [Fact]
    public void CheckMobile_ReturnsCorrectValidation()
    {
        // Valid case
        Assert.False(IsValidAttribute.CheckMobile(ECustomeValidation.Mobile, "09123456789"));

        // Invalid case
        Assert.True(IsValidAttribute.CheckMobile(ECustomeValidation.Mobile, "12345"));
    }

    [Fact]
    public void CheckMobile2_ReturnsCorrectValidation()
    {
        // Valid case (0-11 digits)
        Assert.False(IsValidAttribute.CheckMobile2(ECustomeValidation.Mobile2, "09123456789"));
        Assert.False(IsValidAttribute.CheckMobile2(ECustomeValidation.Mobile2, "123"));

        // Invalid case (more than 11 digits)
        Assert.True(IsValidAttribute.CheckMobile2(ECustomeValidation.Mobile2, "123456789012"));
    }

    [Fact]
    public void IsNationalCodeValid_ReturnsExpectedResult()
    {
        // Valid code
        Assert.True(IsValidAttribute.IsNationalCodeValid("1457760835"));

        // Invalid code
        Assert.False(IsValidAttribute.IsNationalCodeValid("1234567890"));
    }

    [Fact]
    public void IsCorpIdValid_ReturnsExpectedResult()
    {
        // Valid corp ID
        Assert.True(IsValidAttribute.IsCorpIdValid("10260067460"));

        // Invalid corp ID
        Assert.False(IsValidAttribute.IsCorpIdValid("123456"));
    }
}