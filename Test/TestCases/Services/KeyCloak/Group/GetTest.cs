using Application.Services.Auth.Keycloak.GroupService;
using Core.GenericResultModel;
using Gateway.KeyCloak.IServices;
using Moq;
using static Application.Services.Auth.Keycloak.GroupService.GetGroupKeycloakRequest;
using FluentValidation.TestHelper;
using Gateway.Model.KeyCloak.Dto;

namespace Test.TestCases.Services.KeyCloak.Group
{
    public class GetTest
    {
        private readonly Mock<IGroupService> _service = new();

        [Fact]
        public async Task GetTest_Validation_Success()
        {
            //Arrange
            GetGroupKeycloakRequest request = new GetGroupKeycloakRequest
            {
                Id = Guid.NewGuid()
            };

            //Act
            var validates = new GetGroupKeycloakRequestValidator();
            var resultValidate = await validates.TestValidateAsync(request);

            //Assert
            resultValidate.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task GetTest_Validation_Failed()
        {
            //Arrange
            GetGroupKeycloakRequest request = new GetGroupKeycloakRequest
            {
                Id = Guid.Empty
            };

            //Act
            var validates = new GetGroupKeycloakRequestValidator();
            var resultValidate = await validates.TestValidateAsync(request);

            //Assert
            resultValidate.ShouldHaveAnyValidationError();
        }

        [Fact]
        public async Task GetTest_Success()
        {
            // Arrange
            var request = new GetGroupKeycloakRequest() { Id = Guid.NewGuid() };

            var systemUnderTest = new GetGroupKeycloakRequestHandler(_service.Object);

            _service.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(new ApiResult<RoleKeycloakDto>() { IsSuccess = true, Code = 200});

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task GetTest_Failed()
        {
            // Arrange
            var request = new GetGroupKeycloakRequest() { Id = Guid.NewGuid() };

            var systemUnderTest = new GetGroupKeycloakRequestHandler(_service.Object);

            _service.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(new ApiResult<RoleKeycloakDto>() { IsSuccess = false, Code = 500 });

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.NotEqual(200, result.Code);
        }

    }
}
