using Application.Services.BaseService;
using Application.Services.OccupationPlaceService;
using Core.Entities;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.OccupationPlaceServiceTest
{
    public class UpdateTest
    {

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(0, 1, true)]
        [InlineData(1, 0, true)]
        [InlineData(1, 1, false)]
        public async Task UpdateOccupationPlace_test(long ocId, long cusid, bool inqres)
        {
            // arrange
            var mediator = new Mock<IMediator>();
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();

            collection.Context.Setup(x => x.OccupationPlaces).ReturnsDbSet([
              new OccupationPlace(){
                Id = ocId,
                Property = new(){
                    Id = 1,
                    CustomerId = 1,
                    CityId = 1
                }
              }
            ]);

            collection.Context.Setup(x => x.CustomerSchemes).ReturnsDbSet([
                new CustomerScheme(){Id = cusid, CustomerId = 1}
            ]);

            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

            mediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(new Core.GenericResultModel.ApiResult<long>()
            {
                IsSuccess = inqres,
                Data = 1
            });

            var handler = new UpdateOccupationPlaceRequestHandler(collection.Context.Object, mediator.Object);

            // act
            var result = await handler.Handle(new UpdateOccupationPlaceRequest() { Id = 1, OwnershipType = Core.Enums.EOccupationPlace_ownershipType.ThirdPerson, CustomerSchemeId = 1 }, CancellationToken.None);


            // assert
            Assert.NotNull(result);
        }
    }
}
