using Core.Enums;
using Core.Utils;
using Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Utils
{
    public class UtilTest
    {
        [Fact]
        public void TryJsonIndentedFormat_test()
        {
            //Arrange
            string input = "string";

            //Act
            var result = input.TryJsonIndentedFormat();

            //Assert
            if (result == null)
            {
                Assert.Equal("", result);
            }
            else
            {
                Assert.Equal(input, result);
            }
            //Assert.Equal(input, result);
        }

        [Fact]
        public void TryJsonIndentedFormatError_test()
        {
            //Arrange
            string input = "{'name' : 'amir'}";

            //Act
            var result = input.TryJsonIndentedFormat();

            //Assert
            Assert.NotEqual("string", result);
        }

        [Fact]
        public void EncryptString_test()
        {
            //Arrange
            string key = "2s5v8y/B?E(H+MbQeShVmYq3t6w9z$C&";
            string plainText = "this is the plain text";

            //Act
            var result = StaticUtils.EncryptString(key, plainText);

            //Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public void DecryptString_test()
        {
            //Arrange
            string key = "2s5v8y/B?E(H+MbQeShVmYq3t6w9z$C&";
            string cipherText = "/7MYcKhmZtm3UsBNCL5sYGPvuBglgF2XpwcIWWFziBc=";
            string plainText = "this is the plain text";

            //Act
            var result = StaticUtils.DecryptString(key, cipherText);

            //Assert
            Assert.Equal(plainText, result);
        }


        [Fact]
        public void GetEnumDescription_WithDescription()
        {
            //Arrange
            EOrganizationTypeLimited sampleEnum = EOrganizationTypeLimited.branch;

            //Act
            var result = sampleEnum.GetEnumDescription();

            //Assert
            Assert.Equal("شعبه", result);
        }

        [Fact]
        public void GetEnumDescription_WithoutDescription()
        {
            //Arrange
            just_for_test sampleEnum = just_for_test.credit_department;

            //Act
            var result = sampleEnum.GetEnumDescription();

            //Assert
            Assert.Equal("credit_department", result);
        }

        public enum just_for_test
        {
            credit_department = 0,
        }
    }
}
