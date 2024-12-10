using Application.Services.BaseService;
using Application.Services.CurrencyService;
using DNTPersianUtils.Core;
using EntityFrameworkCoreMock;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Test.TestCases.Services.BaseService
{
    public class UpdateTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public async Task Handle_ReturnFalse()
        {
            // Arrange
            var request = new UpdateCurrencyRequest()
            {
                Code = "code",
                ExchangeRate = 2,
                Id = 1,
                Title = "title"
            };

            var systemUnderTest = new BaseUpdateHandler<UpdateCurrencyRequest, Core.Entities.Currency>(_unitOfWork.Object);

            _unitOfWork.Setup(x => x.Repository.Update<Core.Entities.Currency>(It.IsAny<Core.Entities.Currency>()));
            _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(It.IsAny<CancellationToken>()));
            var contextMoq = new DbContextMock<DBContext>();

            _unitOfWork.SetupGet(x => x.Context).Returns(contextMoq.Object);
            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);


            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task Handle_ReturnTrue()
        {
            // Arrange
            var request = new UpdateCurrencyRequest()
            {
                Code = "code",
                ExchangeRate = 2,
                Id = 1,
                Title = "title"
            };

            var systemUnderTest = new BaseUpdateHandler<UpdateCurrencyRequest, Core.Entities.Currency>(_unitOfWork.Object);

            _unitOfWork.Setup(x => x.Repository.Update<Core.Entities.Currency>(It.IsAny<Core.Entities.Currency>()));
            _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);


            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);


            //Assert
            Assert.True(result.IsSuccess);
        }

    }
}
