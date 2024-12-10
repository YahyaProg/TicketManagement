using Application.Services.WorkplaceInfoService;
using Core.Entities;
using Core.Enums;
using Moq;
using Test.Helper;

namespace Test.TestCases.Services.WorkPlaceInfo
{
    public class UpdateWorkPlaceInfo
    {
        public static IEnumerable<object[]> requests =>
        new List<object[]>
        {
            new object[] { new Customer() { Id = 5, IndividualCustomer = new() { NationalId = "234", BirthDate = DateTime.Now }, CorporateCustomer = new () {Id = 6, CorpId= "124", CompanyTypeId = 2, Name = "amir", Bazargani = "test", PaidPercent = 12, CurrentFund= 12} },  new UpdateWorkplaceInfoRequest() { Id= 1, CorpId = "2324", CustomerType = ECustomerType.IndividualCustomer , NationalId = "12423"} },
            new object[] { new Customer() { Id = 5, CorporateCustomer = new () {Id = 6} }, new UpdateWorkplaceInfoRequest() { Id= 1, CorpId = "2324", CustomerType = ECustomerType.CorporateCustomer } },
            new object[] { new Customer() { Id = 5, IndividualCustomer = new() { NationalId = "234", BirthDate = DateTime.Now } },  new UpdateWorkplaceInfoRequest() { Id= 1, CorpId = "2324", CustomerType = ECustomerType.CorporateCustomer } },
            new object[] { new Customer() { Id = 7}, new UpdateWorkplaceInfoRequest() { Id= 1, CorpId = "2324", CustomerType = ECustomerType.CorporateCustomer } },
        };

        [Theory, MemberData(nameof(requests))]
        public async void UpdateWorkPalceInfo(Customer? owner = null, UpdateWorkplaceInfoRequest? request = null)
        {
            // Arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            var system = new UpdateWorkplaceInfoRequestHandler(collection.UnitOfWork.Object);

            if (owner?.Id == 7)
                owner = null;

            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(2);

            collection.UnitOfWork.Setup(x => x.WorkplaceInfoRepo.GetOne(It.IsAny<long>())).ReturnsAsync(new OccupationPlace()
            {
                Id = 5,
                Version = 1,
                Fax = "09158965323",
                OccupationInformationId = 5,
                CustomerSchemeId = 5,
                OwnershipType = EOccupationPlace_ownershipType.Owner,
                Phone = "09158965323",
                PropertyId = 5,
                RelationId = 5,
                WorkPlaceTypeId = 5,
                Property = new Property()
                {
                    Id = 5,
                    CustomerId = 5,
                    Customer = owner
                }
            });

            // Act
            var result = await system.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
