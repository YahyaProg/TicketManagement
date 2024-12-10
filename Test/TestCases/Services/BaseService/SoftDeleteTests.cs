using Application.Services.BaseService;
using Application.Services.CurrencyService;
using Core.Entities;
using Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.BaseService
{
    public class SoftDeleteTests
    {

        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public async Task Handle_404()
        {
            // Arrange
            var request = new DeleteCurrencyRequest()
            {
                Id = 1,
            };

            var systemUnderTest = new BaseSoftDeleteHandler<DeleteCurrencyRequest, Core.Entities.Currency, long>(_unitOfWork.Object);

            _unitOfWork.Setup(x => x.Repository.GetByIdAsync<Core.Entities.Currency?>(It.IsAny<long>(), It.IsAny<CancellationToken>())).ReturnsAsync(value: null);

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);


            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(404, result.Code);
        }

        [Fact]
        public async Task Handle_DeletedBefore()
        {
            // Arrange
            var request = new DeleteCurrencyRequest()
            {
                Id = 1,
            };

            var systemUnderTest = new BaseSoftDeleteHandler<DeleteCurrencyRequest, Core.Entities.Currency, long>(_unitOfWork.Object);

            _unitOfWork.Setup(x => x.Repository.GetByIdAsync<Core.Entities.Currency?>
                (It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Core.Entities.Currency { Deleted = true });

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);


            //Assert
            Assert.True(result.IsSuccess);
        }


        [Fact]
        public async Task Handle_Fail()
        {
            // Arrange
            var request = new DeleteCurrencyRequest()
            {
                Id = 1,
            };

            var systemUnderTest = new BaseSoftDeleteHandler<DeleteCurrencyRequest, Core.Entities.Currency, long>(_unitOfWork.Object);

            _unitOfWork.Setup(x => x.Repository.GetByIdAsync<Core.Entities.Currency?>
                (It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Core.Entities.Currency { Deleted = false });

            _unitOfWork.Setup(x => x.Repository.Update<Core.Entities.Currency>(It.IsAny<Core.Entities.Currency>()));
            _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);


            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task Handle_Success()
        {
            // Arrange
            var request = new DeleteCurrencyRequest()
            {
                Id = 1,
            };

            var systemUnderTest = new BaseSoftDeleteHandler<DeleteCurrencyRequest, Core.Entities.Currency, long>(_unitOfWork.Object);

            _unitOfWork.Setup(x => x.Repository.GetByIdAsync<Core.Entities.Currency?>
                (It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Core.Entities.Currency { Deleted = false });

            _unitOfWork.Setup(x => x.Repository.Update<Core.Entities.Currency>(It.IsAny<Core.Entities.Currency>()));
            _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);


            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);


            //Assert
            Assert.True(result.IsSuccess);
        }
    }
}
