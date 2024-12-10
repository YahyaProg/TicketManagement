using Application.Services.BaseService;
using Application.Services.CurrencyService;
using Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.BaseService
{
    public class DeleteTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public async Task Handle_ReturnFalse()
        {
            // Arrange
            var request = new DeleteCurrencyRequest()
            {
                Id = 1,
            };

            var systemUnderTest = new BaseDeleteHandler<DeleteCurrencyRequest, Core.Entities.Currency, long>(_unitOfWork.Object);

            _unitOfWork.Setup(x => x.Repository.GetByIdAsync<Core.Entities.Currency>(It.IsAny<long>(), It.IsAny<CancellationToken>()));

            _unitOfWork.Setup(x => x.Repository.Remove(It.IsAny<Core.Entities.Currency>()));
            _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(It.IsAny<CancellationToken>()));

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
            var request = new DeleteCurrencyRequest()
            {
                Id = 1,
            };

            var systemUnderTest = new BaseDeleteHandler<DeleteCurrencyRequest, Core.Entities.Currency, long>(_unitOfWork.Object);

            _unitOfWork.Setup(x => x.Repository.GetByIdAsync<Core.Entities.Currency>(It.IsAny<long>(), It.IsAny<CancellationToken>()));

            _unitOfWork.Setup(x => x.Repository.Remove(It.IsAny<Core.Entities.Currency>()));
            _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);
            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);


            //Assert
            Assert.True(result.IsSuccess);
        }
    }
}
