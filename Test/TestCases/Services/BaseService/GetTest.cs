using Application.Services.BaseService;
using Application.Services.CurrencyService;
using Azure.Core;
using Core.ViewModel;
using Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.BaseService
{
    public class GetTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public async Task Handle_ReturnFalse()
        {

            var request = new GetCurrencyRequest()
            {
                Id = 1,
            };
            var systemUnderTest = new BaseGetHandler<GetCurrencyRequest, Core.Entities.Currency, CurrencyVM,long>(_unitOfWork.Object);

            _unitOfWork.Setup(x => x.Repository.GetByIdAsync<Core.Entities.Currency>(It.IsAny<long>(), It.IsAny<CancellationToken>()));

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);


            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(404, result.Code);
        }

        [Fact]
        public async Task Handle_ReturnTrue()
        {

            var request = new GetCurrencyRequest()
            {
                Id = 1,
            };
            var systemUnderTest = new BaseGetHandler<GetCurrencyRequest, Core.Entities.Currency, CurrencyVM, long>(_unitOfWork.Object);

            _unitOfWork.Setup(x => x.Repository.GetByIdAsync<Core.Entities.Currency>(It.IsAny<long>(), It.IsAny<CancellationToken>())).ReturnsAsync( new Core.Entities.Currency());

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);


            //Assert
            Assert.True(result.IsSuccess);
        }
    }
}
