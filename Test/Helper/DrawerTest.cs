using System;
using System.Text.RegularExpressions;
using Core.Helpers.CaptchaHelper;
using Xunit;

namespace Tests.Helpers.CaptchaHelper
{
    public class DrawerTests
    {
        [Fact]
        public void GenerateCaptchaImage_ReturnsNonNullBase64String()
        {
            // Arrange
            int width = 100;
            int height = 40;
            string captchaCode = "ABCD";

            // Act
            string base64Image = Drawer.GenerateCaptchaImage(width, height, captchaCode);

            // Assert
            Assert.False(string.IsNullOrEmpty(base64Image), "Captcha image Base64 string should not be null or empty.");
        }

        [Fact]
        public void GenerateCaptchaImage_ReturnsValidBase64String()
        {
            // Arrange
            int width = 100;
            int height = 40;
            string captchaCode = "ABCD";

            // Act
            string base64Image = Drawer.GenerateCaptchaImage(width, height, captchaCode);

            // Assert
            Assert.True(IsBase64String(base64Image), "Generated string is not a valid Base64 format.");
        }

        [Fact]
        public void GenerateCaptchaImage_ReturnsNonEmptyImage()
        {
            // Arrange
            int width = 100;
            int height = 40;
            string captchaCode = "ABCD";

            // Act
            string base64Image = Drawer.GenerateCaptchaImage(width, height, captchaCode);

            // Convert Base64 back to byte array and check the image size
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            Assert.True(imageBytes.Length > 0, "Captcha image byte array should not be empty.");
        }

        [Fact]
        public void GenerateCaptchaImage_DifferentCodesGenerateDifferentImages()
        {
            // Arrange
            int width = 100;
            int height = 40;
            string captchaCode1 = "ABCD";
            string captchaCode2 = "WXYZ";

            // Act
            string base64Image1 = Drawer.GenerateCaptchaImage(width, height, captchaCode1);
            string base64Image2 = Drawer.GenerateCaptchaImage(width, height, captchaCode2);

            // Assert
            Assert.NotEqual(base64Image1, base64Image2);
        }

        private bool IsBase64String(string base64)
        {
            return Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,2}$") && (base64.Length % 4 == 0);
        }
    }
}
