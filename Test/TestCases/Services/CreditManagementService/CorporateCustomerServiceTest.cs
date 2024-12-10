using Application.Services.CorporateCustomerService;
using Core.Helpers;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Helper;

namespace Test.TestCases.Services.CreditManagementService
{
    public class CorporateCustomerServiceTest
    {
        private readonly GetCorporateCustomerByIdRequest getById = new() { Id = 1 };
        private readonly GetCorporateCustomerByCorpIdRequest getByCorpId = new() { CorpId = "1" };
        private readonly InitSetting _setting=new() { 
            UploadDirectory = "assets/",
        };

        [Fact]
        public async Task GetCustomerById_NotFound()
        {
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();

            collection.Context.Setup(x => x.Customers).ReturnsDbSet(new List<Core.Entities.Customer>());

            var handler = new GetCorporateCustomerByIdRequestHandler(collection.UnitOfWork.Object);

            var res = await handler.Handle(getById, CancellationToken.None);

            Assert.False(res.IsSuccess);
            Assert.Equal(404, res.Code);
        }

        [Fact]
        public async Task GetCustomerById_Success()
        {
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();

            collection.Context.Setup(x => x.Customers).ReturnsDbSet(new List<Core.Entities.Customer>() { new() { Id = getById.Id, CorporateCustomer = new() } });

            var handler = new GetCorporateCustomerByIdRequestHandler(collection.UnitOfWork.Object);

            var res = await handler.Handle(getById, CancellationToken.None);

            Assert.True(res.IsSuccess);
            Assert.NotNull(res.Data);
        }

        [Fact]
        public async Task GetCustomerByCorpId_Success()
        {
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();

            collection.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet(new List<Core.Entities.CorporateCustomer>() { new() { CorpId = getByCorpId.CorpId } });

            var handler = new GetCorporateCustomerByCorpIdRequestHandler(collection.UnitOfWork.Object, _setting);

            var res = await handler.Handle(getByCorpId, CancellationToken.None);

            Assert.True(res.IsSuccess);
            Assert.NotNull(res.Data);
        }
    }
}
