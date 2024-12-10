using Core.Entities;
using Core.ViewModel;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Repositories.WorkPlaceInfoRepo;

public class WorkPlaceInfoRepoTest
{
    private readonly Mock<DBContext> _contextMoq = new();

    [Fact]
    public async Task GetAllWorkPlaceInfo()
    {
        _contextMoq.Setup(x => x.OccupationPlaces).ReturnsDbSet([new() { Id = 0, PropertyId = 0, RelationId = 0 }]);
        _contextMoq.Setup(x => x.Properties).ReturnsDbSet([new() { Id = 0, CityId = 0, ProvinceId = 0, CustomerId = 0 }]);
        _contextMoq.Setup(x => x.PersonRelations).ReturnsDbSet([new() { Id = 0 }]);
        _contextMoq.Setup(x => x.Cities).ReturnsDbSet([new() { Id = 0 }]);
        _contextMoq.Setup(x => x.Provinces).ReturnsDbSet([new() { Id = 0 }]);
        _contextMoq.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 0 }]);
        _contextMoq.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() { Id = 0 }]);
        _contextMoq.Setup(x => x.IndividualCustomers).ReturnsDbSet([new() { Id = 0 }]);
        _contextMoq.Setup(x => x.WorkPlaceTypes).ReturnsDbSet([]);

        var WorkPlaceInfoRepo = new Infrastructure.Repositories.WorkplaceInfoRepository.WorkplaceInfoRepo(_contextMoq.Object);

        var result = await WorkPlaceInfoRepo.Search(new WorkplaceInfoIM { Id = 0, Page = 1, Size = 1 });

        Assert.NotEmpty(result.Items);
    }
    [Fact]
    public async Task GetOneWorkPlaceInfo()
    {
        _contextMoq.Setup(x => x.OccupationPlaces).ReturnsDbSet([new() { Id = 1, PropertyId = 0, RelationId = 0,
                    Property = new(){Id = 0,
                                    CustomerId=0,
                                    Customer = new(){ 
                                                    Id = 0 ,
                                                    CorporateCustomer = new(){
                                                                    Id=0
                                                                            },
                                    } 
                    } 
        }]);

        var WorkPlaceInfoRepo = new Infrastructure.Repositories.WorkplaceInfoRepository.WorkplaceInfoRepo(_contextMoq.Object);

        var result = await WorkPlaceInfoRepo.GetOne(1);

        Assert.IsType<OccupationPlace>(result);

    }
    [Fact]
    public  void AddCorporateCustomerWorkPlaceInfo()
    {
        _contextMoq.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() {Id=15, CorpId ="10", }]);
        _contextMoq.Setup(x => x.Customers).ReturnsDbSet([new() { Id =15 }]);
        var request = new AddWorkplaceInfo() { 
            CorpId = "10",
        };
        var model = new OccupationPlace()
        {
            Id = 25 ,
            Property =new()
            {
                Id=10
            }
        };
        var WorkPlaceInfoRepo = new Infrastructure.Repositories.WorkplaceInfoRepository.WorkplaceInfoRepo(_contextMoq.Object);

        var result =  WorkPlaceInfoRepo.AddCorporateCustomer(request,model);

        Assert.NotNull(result);

    }
    [Fact]
    public void AddCorporateCustomerWorkPlaceInfo_Else()
    {
        _contextMoq.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() { Id = 15, CorpId = "11", }]);
        _contextMoq.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 15 }]);
        var request = new AddWorkplaceInfo()
        {
            CorpId = "10",
            CompanyTypeId = 25,
            Name = "Name",
            Bazargani = "Bazargani" ,
            PaidPercent= 100000000000,
            CurrentFund=522222222222
        };
        var model = new OccupationPlace()
        {
            Id = 25,
            Property = new()
            {
                Id = 10
            }
        };
        var WorkPlaceInfoRepo = new Infrastructure.Repositories.WorkplaceInfoRepository.WorkplaceInfoRepo(_contextMoq.Object);

        var result = WorkPlaceInfoRepo.AddCorporateCustomer(request, model);

        Assert.NotNull(result);

    }
    [Fact]
    public void AddIndividualCustomerWorkPlaceInfo()
    {
        _contextMoq.Setup(x => x.IndividualCustomers).ReturnsDbSet([new() { Id=10, NationalId = "15" }]);
        _contextMoq.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 10 }]);
        var request = new AddWorkplaceInfo()
        {
           
            CorpId = "10",
            NationalId = "15"
        };
        var model = new OccupationPlace()
        {
            Id = 25 ,
            Property = new()
            {
                Id = 10
            }
        };
        var WorkPlaceInfoRepo = new Infrastructure.Repositories.WorkplaceInfoRepository.WorkplaceInfoRepo(_contextMoq.Object);

        var result = WorkPlaceInfoRepo.AddIndividualCustomer(request, model);

        Assert.NotNull(result);

    }
    [Fact]
    public void AddIndividualCustomerWorkPlaceInfo_Else()
    {
        _contextMoq.Setup(x => x.IndividualCustomers).ReturnsDbSet([new() { Id = 10, NationalId = "15" }]);
        _contextMoq.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 10 }]);
        var request = new AddWorkplaceInfo()
        {

            CorpId = "10",
            NationalId = "11"  ,
            BirthDate = DateTime.Now,   
        };
        var model = new OccupationPlace()
        {
            Id = 25,
            Property = new()
            {
                Id = 10
            }
        };
        var WorkPlaceInfoRepo = new Infrastructure.Repositories.WorkplaceInfoRepository.WorkplaceInfoRepo(_contextMoq.Object);

        var result = WorkPlaceInfoRepo.AddIndividualCustomer(request, model);

        Assert.NotNull(result);

    }

}
