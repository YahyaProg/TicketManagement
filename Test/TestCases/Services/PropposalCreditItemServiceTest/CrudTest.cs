using Application.Services.ProposalCreditItemService;
using Core.Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Test.Helper;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.PropposalCreditItemServiceTest
{
    public class CrudTest
    {
        private readonly MoqCollection collection; 
        public CrudTest()
        {
            collection = MoqHelper.GetUnitOfWorkMoqCollection();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task DeleteProposalCreditItem_Test(int type)
        {
            // Arrange
            var handler = new DeleteProposalCreditItemRequestHandler(collection.Context.Object);

            collection.Context.Setup(x => x.ProposalCreditItems).ReturnsDbSet([
                new ProposalCreditItem(){Id = 1}
                ]);
            collection.Context.Setup(x => x.Conditions).ReturnsDbSet([
                new Core.Entities.Condition(){Id = 1, ProposalCreditItemId = type != 2 ? 0 : 1}
                ]);

            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(type != 1 ? 0 : 1);


            // Act
            var result = await handler.Handle(new() { Id = 1 }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task UpdateProposalCreditItem_Test(int type)
        {
            // Arrange
            var handler = new UpdateProposalCreditItemRequestHandler(collection.Context.Object);

            collection.Context.Setup(x => x.ProposalCreditItems).ReturnsDbSet([
                new ProposalCreditItem(){Id = 1}
                ]);
            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(type != 1 ? 0 : 1);


            // Act
            var result = await handler.Handle(new() { Id = 1, ParentItemId = type != 2 ? 0 : 1 }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }
    }
}
