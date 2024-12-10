using Application.Services.StockService;
using Core.Entities;
using Core.Helpers;
using Moq;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.Stock
{
    public class AddOrUpdateTest
    {
        private readonly Mock<IUserHelper> helper = new();
        public static IEnumerable<object[]> requests =>
         new List<object[]>
         {
            new object[] { new AddOrUpdateStockRequest() { CorpId = "124", Bourse = true, Mortgage = false }, 2 },
            new object[] { new AddOrUpdateStockRequest() { CorpId = "98124", Bourse = false, Mortgage = false } , -1 },
            new object[] { new AddOrUpdateStockRequest() { CorpId = "98124", Bourse = true, Mortgage = false, StockId = 1 } , 2 },
         };


        [Theory, MemberData(nameof(requests))]
        public async void AddOrUpdateStockRequest(AddOrUpdateStockRequest request, int resIndex)
        {
            // Arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            var system = new AddOrUpdateStockRequestHandler(collection.UnitOfWork.Object, helper.Object);

            var data = new List<CorporateCustomer>()
            {
                new(){CorpId = "98124"}
            };

            collection.Context.Setup(x => x.BankStaffs).ReturnsDbSet(new List<BankStaff>()
            {
                new() {Id = 23, UserId = "23"}
            });

            helper.Setup(x => x.GetUserFromToken()).Returns(new Core.ViewModel.UserDto()
            {
                Id = "23",
            });

            collection.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet(data);
            collection.Repository.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(resIndex);

            // Act
            var result = await system.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }
    }
}
