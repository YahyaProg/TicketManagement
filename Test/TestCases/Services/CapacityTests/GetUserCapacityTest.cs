using Application.Services.CapacityService;
using Core.Entities;
using Moq.EntityFrameworkCore;
using Test.Helper;



namespace Test.TestCases.Services.CapacityTests
{
    public class GetUserCapacityTest
    {

        [Fact]
        public async Task CapacityBulkAddOrUpdateRequestTest()
        {
            // arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            var handler = new GetUserCapacityListRequestHandler(collection.Context.Object);

            collection.Context.Setup(x => x.Capacities).ReturnsDbSet([
                new Capacity(){
                    Id = 1,
                    ProposalId = 1,
                    CustomerId = 1,
                    ProposalSchemeId = 1,
                    CapacityMeasurement = Core.Enums.ECapacityMeasurement.AmountOfEmployedProjects,
                    Value=1
                },
                new Capacity(){
                    Id = 2,
                    ProposalId = 1,
                    CustomerId = 1,
                    ProposalSchemeId = 1,
                    CapacityMeasurement = Core.Enums.ECapacityMeasurement.NumberOfStaff,
                    Value=2
                },
                ]);


            var request = new GetUserCapacityListRequest()
            {
                CustomerId = 1,

            };

            // act
            var result = await handler.Handle(request, CancellationToken.None);

            // assert
            Assert.True(result.IsSuccess);
        }
    }
}
