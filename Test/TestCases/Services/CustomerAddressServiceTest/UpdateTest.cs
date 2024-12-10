using Application.Services.BaseService;
using Application.Services.CustomerAddressService;
using Core.Entities;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.CustomerAddressServiceTest
{
    public class CustomerAddressServiceUpdateTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task UpdateCustomerAddress_Test(int type)
        {
            // arrange
            var mediator = new Mock<IMediator>();
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();

            var property = new Property()
            {
                Id = 1,
                CustomerId = 1,
            };

            var address = new Address()
            {
                Id = 1,
                CustomerSchemeId = 1,
                PropertyId = 1,
                RelationId = 1,
                Property = property
            };

            collection.Context.Setup(x => x.CustomerAddresses).ReturnsDbSet([
                new CustomerAddress(){
                    ID = 1,
                    AddressId = 1,
                    Address = type != 1 ? address : null // 1= null
                }
                ]);


            collection.Context.Setup(x => x.CustomerSchemes).ReturnsDbSet([
                new CustomerScheme(){
                    Id= type != 2 ? 1 : 0, // 2 = 0
                    CustomerId = 1
                }
                ]);


            mediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(
                new Core.GenericResultModel.ApiResult<long>()
                {
                    IsSuccess = type != 3 ? true : false, // 3= false
                    Data = 20
                }
                );

            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

            var handler = new UpdateAddressRequestHandler(collection.Context.Object, mediator.Object);

            // act
            var result = await handler.Handle(new() { Id = 1, OwnershipType = Core.Enums.EAddress_ownerShip.ThirdPerson, RelationId = 1, CustomerSchemeId = 1, CorpId = "21313", CityId = 1 }, CancellationToken.None);

            // assert
            Assert.NotNull(result);
        }
    }
}
