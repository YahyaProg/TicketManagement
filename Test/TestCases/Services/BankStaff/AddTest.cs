using Application.Services.BankStaffService;
using Core.Entities;
using Core.GenericResultModel;
using FluentValidation.TestHelper;
using Gateway.KeyCloak.IServices;
using Gateway.Model.KeyCloak.Params;
using Gateway.Model.KeyCloak.ViewModel;
using Infrastructure;
using Moq;
using static Application.Services.BankStaffService.AddBankStaffRequest;

namespace Test.TestCases.Services.BankStaffTest
{
    public class AddTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<IUserService> _service = new();

        private static AddBankStaffRequest _correctRequestParams = new AddBankStaffRequest()
        {
            username = "admin",
            firstName = "name",
            lastName = "name",
            PersonCode = "1234",
            OrganizationId = 100,
            Password = "123456789",
            ConfirmPassword = "123456789",
            Mobile = "09912983589",
            Position = 0
        };

        private static AddBankStaffRequest _worngRequestParams = new AddBankStaffRequest()
        {
            username = string.Empty,
            firstName = string.Empty,
            lastName = string.Empty,
            PersonCode = string.Empty,
            OrganizationId = 100,
            Password = string.Empty,
            ConfirmPassword = string.Empty,
            Position = 0
            
        };

        [Fact]
        public async Task AddTest_Validation_Success()
        {
            // Arrange

            //Act
            var validates = new AddBankStaffRequestValidator();
            var resultValidate = await validates.TestValidateAsync(_correctRequestParams);

            //Assert
            resultValidate.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task AddTest_Validation_Failed()
        {
            // Arrange

            //Act
            var validates = new AddBankStaffRequestValidator();
            var resultValidate = await validates.TestValidateAsync(_worngRequestParams);

            //Assert
            resultValidate.ShouldHaveAnyValidationError();
        }

        [Fact]
        public async Task AddTest_Success()
        {
            // Arrange
            var systemUnderTest = new AddBankStaffRequestHandler(_unitOfWork.Object, _service.Object);

            _service.Setup(s => s.AddAsync(It.IsAny<AddUserKeyCloakParams>()))
                .ReturnsAsync(new ApiResult<UserKeyCloakVM>() { IsSuccess = true, Code = 0 , Data = new UserKeyCloakVM { id = Guid.NewGuid() } });
            _unitOfWork.Setup(u => u.BankStaffRepo.ExistsByPersonCodeAsync(It.IsAny<string>())).ReturnsAsync(false);
            _unitOfWork.Setup(u => u.Repository.GetByIdAsync<Organization>(It.IsAny<long>(),CancellationToken.None))
                .ReturnsAsync(new Organization { Id = _correctRequestParams.OrganizationId.Value });



            _unitOfWork.Setup(u => u.Repository.AddAsync(It.IsAny<Core.Entities.BankStaff>(), CancellationToken.None));
            _unitOfWork.Setup(u => u.Repository.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);


            //Act
            var result = await systemUnderTest.Handle(_correctRequestParams, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task AddTest_Failed()
        {
            // Arrange
            var systemUnderTest = new AddBankStaffRequestHandler(_unitOfWork.Object, _service.Object);

            _service.Setup(s => s.AddAsync(It.IsAny<AddUserKeyCloakParams>()))
                .ReturnsAsync(new ApiResult<UserKeyCloakVM>() { IsSuccess = true, Code = 200, Data = new UserKeyCloakVM { id = Guid.NewGuid() } });
            _unitOfWork.Setup(u => u.BankStaffRepo.ExistsByPersonCodeAsync(It.IsAny<string>())).ReturnsAsync(false);
            _unitOfWork.Setup(u => u.Repository.AddAsync(It.IsAny<Core.Entities.BankStaff>(), CancellationToken.None));
            _unitOfWork.Setup(u => u.Repository.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

            //Act
            var result = await systemUnderTest.Handle(_correctRequestParams, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
        }
    }
}
