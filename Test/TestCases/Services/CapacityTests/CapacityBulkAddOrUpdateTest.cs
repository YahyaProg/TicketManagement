using Application.Services.CapacityService;
using Core.Entities;
using Core.ViewModel;
using Moq;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.CapacityTests
{
    public class CapacityBulkAddOrUpdateTest
    {

        [Fact]
        public async Task CapacityBulkAddOrUpdateRequestTest()
        {
            // arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            var handler = new CapacityBulkAddOrUpdateRequestHandler(collection.Context.Object);

            collection.Context.Setup(x => x.Capacities).ReturnsDbSet([
                new Capacity(){
                    Id = 1,
                    ProposalId = 1,
                    CustomerId = 1,
                    ProposalSchemeId = 1,
                    CapacityMeasurement = Core.Enums.ECapacityMeasurement.AmountOfEmployedProjects
                }
                ]);

            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

            var request = new CapacityBulkAddOrUpdateRequest()
            {
                Items = new List<CapacityBulkAddItem>()
                {
                    new()
                    {
                        Id = 1,
                        CapacityMeasurement = Core.Enums.ECapacityMeasurement.AmountOfEmployedProjects,
                    }
                },
                ProposalSchemeId = 1,
                CustomerId = 1,
                ProposalId = 1
            };

            // act
            var result = await handler.Handle(request, CancellationToken.None);

            // assert
            Assert.True(result.IsSuccess);
        }
    }
}
