using Application.Services.BaseService;
using Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using TanvirArjel.EFCore.GenericRepository;
using Application.Services.CurrencyService;

namespace Test.TestCases.Services.BaseService
{
    public class AddTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public async Task Handle_ReturnFalse()
        {
            // Arrange
            var request = new AddCurrencyRequest()
            {
                Code = "23",
                Title = "Test",
                ExchangeRate = 1,
            };
            var systemUnderTest = new BaseAddHandler<AddCurrencyRequest, Core.Entities.Currency>(_unitOfWork.Object);

            _unitOfWork.Setup(x => x.Repository.AddAsync<Core.Entities.Currency>(It.IsAny<Core.Entities.Currency>(), It.IsAny<CancellationToken>()));

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
            var request = new AddCurrencyRequest()
            {
                Code = "23",
                Title = "Test",
                ExchangeRate = 1,
            };
            var systemUnderTest = new BaseAddHandler<AddCurrencyRequest, Core.Entities.Currency>(_unitOfWork.Object);

            _unitOfWork.Setup(x => x.Repository.AddAsync<Core.Entities.Currency>(It.IsAny<Core.Entities.Currency>(), It.IsAny<CancellationToken>()));

            _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);


            //Assert
            Assert.True(result.IsSuccess);

        }
    }
}
