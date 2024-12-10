using Application.BaseService;
using Application.Services.CurrencyService;
using Core.GenericResultModel;
using Core.ViewModel;
using Gateway;
using Infrastructure;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;
using Test.Helper;

namespace Test.TestCases.Services.BaseService
{
    public class SearchTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public async Task BaseSearchTest()
        {
            //Arrange
            var request = new SearchCurrencyRequest()
            {
                Code = "23",
                Title= "salam",
                Page = 1,
                Size= 10
            };
            var systemUnderTest = new BaseSearchHandler<SearchCurrencyRequest, Core.Entities.Currency, CurrencyVM>(_unitOfWork.Object);
            _unitOfWork.Setup(x => x.Repository.GetListAsync<Core.Entities.Currency, PaginatedList<Core.Entities.Currency>>(It.IsAny<Expression<System.Func<Core.Entities.Currency, bool>>>(), It.IsAny<System.Linq.Expressions.Expression<System.Func<Core.Entities.Currency, TanvirArjel.EFCore.GenericRepository.PaginatedList<Core.Entities.Currency>>>>(), It.IsAny<CancellationToken>()));

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
        }
    }
}
