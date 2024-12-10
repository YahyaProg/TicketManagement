using Application.Services.BankStaffService;
using Core.Entities;
using Core.GenericResultModel;
using Gateway.KeyCloak.IServices;
using Gateway.Model.KeyCloak.Params;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.BankStaffTest
{
    public class UpdateTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<IUserService> _userService = new();

        private static UpdateBankStaffRequest _correctRequestParams = new UpdateBankStaffRequest()
        {
            Id = 1,
            FirstName = "name",
            LastName = "name",
            PersonCode = "1234",
            OrganizationId = 100,
            Position = 0,
            Enabled = true,
            Mobile = "09913108793",
            Rrm = true
        };

        [Fact]
        public async Task UpdateTest_Success()
        {
            // Arrange
            var systemUnderTest = new UpdateBankStaffRequestHandler(_unitOfWork.Object, _userService.Object);

            _unitOfWork.Setup(u => u.BankStaffRepo.ExistsByPersonCodeAsync(It.IsAny<string>())).ReturnsAsync(false);

            _unitOfWork.Setup(u => u.Repository.GetByIdAsync<Organization>(It.IsAny<long>(), CancellationToken.None))
                .ReturnsAsync(new Organization { Id = 190 });

            _unitOfWork.Setup(u => u.Repository.GetByIdAsync<Core.Entities.PositionType>(It.IsAny<long>(), CancellationToken.None))
                .ReturnsAsync(new Core.Entities.PositionType { Id = 190 });

            _unitOfWork.Setup(u => u.Repository.GetByIdAsync<Core.Entities.BankStaff>(It.IsAny<long>(), CancellationToken.None))
                .ReturnsAsync(new Core.Entities.BankStaff { UserId = "0e83da10-0c4a-11ef-a9cd-0800200c9a66", Id = 100 });


            _userService.Setup(u => u.UpdateAsync(It.IsAny<UpdateUserKeyCloakParams>())).ReturnsAsync(new ApiResult<bool>() { IsSuccess = true, Data = true, Code = 200 });


            _unitOfWork.Setup(u => u.Repository.ClearChangeTracker());
            _unitOfWork.Setup(u => u.Repository.Update(It.IsAny<Core.Entities.BankStaff>()));
            _unitOfWork.Setup(u => u.Repository.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

            //Act
            var result = await systemUnderTest.Handle(_correctRequestParams, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task UpdateTest_Failed()
        {
            // Arrange
            var systemUnderTest = new UpdateBankStaffRequestHandler(_unitOfWork.Object, _userService.Object);


            _unitOfWork.Setup(u => u.Repository.GetByIdAsync<Core.Entities.BankStaff>(It.IsAny<long>(), CancellationToken.None))
                .ReturnsAsync(new Core.Entities.BankStaff { UserId = "0e83da10-0c4a-11ef-a9cd-0800200c9a66", Id = 100 });

            _userService.Setup(u => u.UpdateAsync(It.IsAny<UpdateUserKeyCloakParams>())).ReturnsAsync(new ApiResult<bool>() { IsSuccess = false });


            _unitOfWork.Setup(u => u.Repository.ClearChangeTracker());
            _unitOfWork.Setup(u => u.Repository.Update(It.IsAny<Core.Entities.BankStaff>()));
            _unitOfWork.Setup(u => u.Repository.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);


            //Act
            var result = await systemUnderTest.Handle(_correctRequestParams, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
        }
    }
}
