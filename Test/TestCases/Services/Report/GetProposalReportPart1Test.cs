using Application.Services.Report;
using Core.Entities;
using Core.Enums;
using Core.ViewModel.CompanyMembersInfo;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;
using TanvirArjel.EFCore.GenericRepository;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.ReportTest;
public class GetProposalInfoReportPart1RequestHandlerTests
{
    private readonly Mock<DBContext> _mockDbContext;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly GetProposalInfoReportPart1RequestHandler _handler;
    private readonly MoqCollection _moq = GetUnitOfWorkMoqCollection();

    public GetProposalInfoReportPart1RequestHandlerTests()
    {
        _mockDbContext = new Mock<DBContext>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        // Initialize the handler with mocked dependencies
        _handler = new GetProposalInfoReportPart1RequestHandler(_mockDbContext.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnsExpectedData()
    {

        // Arrange
        var request = new GetProposalInfoReportPart1Request
        {
            ProposalSchemeId = 1
        };

        // Mock data setup for DBContext and UnitOfWork to return expected test data
        // 1. Mock the DBContext responses for each query involved in the Handle method.


        // Example of how to use CreateDbSetMock for each DbSet in your DbContext
        var proposalSchemes = new List<ProposalScheme>
            {
                new ProposalScheme { Id = 1, CustomerId = 1 , ProposalId = 1}
            }.AsQueryable();
        ////_ = proposalSchemes.BuildMock();
        _ = _mockDbContext.Setup(x => x.ProposalSchemes).ReturnsDbSet(proposalSchemes);

        var corporateCustomers = new List<CorporateCustomer>
            {
                new CorporateCustomer { Id = 1, RegisterPlaceId = 1 ,Name="SamiaPala",CorpId="123"}
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.CorporateCustomers).ReturnsDbSet(corporateCustomers);

        var customers = new List<Customer>
            {
               new Customer { Id = 1, ClientNo = "123" }
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.Customers).ReturnsDbSet(customers);

        var cities = new List<Core.Entities.City>
            {
              new Core.Entities.City { Id = 1, Title = "CityTitle",ProvinceId=1 }
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.Cities).ReturnsDbSet(cities);


        var proposals = new List<Proposal>
            {
              new Proposal { Id = 1, MosavabeNo = "12345", CustomerId = 1 }
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.Proposals).ReturnsDbSet(proposals);


        //-----------------
        var ProposalActionLogs = new List<ProposalActionLog>
            {
              new ProposalActionLog { Id = 1, UserId = "1", ProposalId=1,ProposalSchemeId = 1 }
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.ProposalActionLogs).ReturnsDbSet(ProposalActionLogs);

        var bankStaffs = new List<BankStaff>
            {
              new BankStaff { Id = 1, UserId = "1", FirstName = "m",LastName = "m" }
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.BankStaffs).ReturnsDbSet(bankStaffs);

        //-----------------
        var customerSchemes = new List<CustomerScheme>
            {
              new CustomerScheme { Id = 1, CustomerId = 1,}
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.CustomerSchemes).ReturnsDbSet(customerSchemes);

        //----------------
        _mockDbContext.Setup(x => x.CompQualQuestions).ReturnsDbSet([
            new CompQualQuestion(){Id = 1, CustomerSchemeId = 1, ActivityCompanySub = 0, LoanPermission = 0, AssetsPermission = 0 , ThirdPersonPermission = 0, PeacePermission = 0 },
            ]);
        var returnValueCompanyMembersInfo = new PaginatedList<GetAllBoardMembersAndManagersVM>(
            [
             new GetAllBoardMembersAndManagersVM(){  NationalId="1111111",Name="x",FullName="xx",ManagerId=1},
              new GetAllBoardMembersAndManagersVM(){ NationalId="2222222",Name="y",FullName="yy",ManagerId=2},
            ], 2, 1, 2);
        _mockUnitOfWork.Setup(x => x.CompanyMembersInfoRepo.GetAllBoardMembersAndManagers(It.IsAny<GetAllBoardMembersAndManagersIM>()))
                       .ReturnsAsync(returnValueCompanyMembersInfo);


        var returnValueMajorShareholders = new PaginatedList<MajorShareholdersGetAllVM>(
            [
            new MajorShareholdersGetAllVM(){ Id =1,Name="x",NationalId="1111111" },
            new MajorShareholdersGetAllVM(){ Id =2,Name="y",NationalId="2222222"},
            ], 2, 1, 2);
        _mockUnitOfWork.Setup(x => x.CompanyMembersInfoRepo.GetAllMajorShareholders(It.IsAny<CompanyMembersInfoGetIM>()))
                       .ReturnsAsync(returnValueMajorShareholders);

        //------------------
        var manufacturingLicences = new List<ManufacturingLicence>
            {
              new ManufacturingLicence { Id = 1, CustomerSchemeId = 1,LicenceNo="123",ExpiryDate=DateTime.Now.AddDays(90)}
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.ManufacturingLicences).ReturnsDbSet(manufacturingLicences);

        var manufacturingLicenceItems = new List<ManufacturingLicenceItem>
            {
              new () { Id = 1, CustomerSchemeId = 1,UnitTypeId=1,ActualCapacity=1000,ManufacturingLicenceId=1}
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.ManufacturingLicenceItems).ReturnsDbSet(manufacturingLicenceItems);

        var licences = new List<Licence>
            {
              new () { Id = 1, CustomerSchemeId = 1,LicenceNo="1234",Description=""}
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.Licences).ReturnsDbSet(licences);

        var serviceCompanyRanks = new List<ServiceCompanyRank>
            {
              new () { Id = 1, CustomerSchemeId = 1,LicenceNo="1234",Rank=1,Title="A"}
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.ServiceCompanyRanks).ReturnsDbSet(serviceCompanyRanks);

        //-------------------------------

        var occupationPlaces = new List<OccupationPlace>
            {
              new () { Id = 1, CustomerSchemeId = 1,PropertyId=1,RelationId=1,OwnershipType=EOccupationPlace_ownershipType.Owner,WorkPlaceTypeId=1}
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.OccupationPlaces).ReturnsDbSet(occupationPlaces);

        var workPlaceTypes = new List<WorkPlaceType>
            {
              new () { Id = 1, Code = EWorkPlaceType_code.daftarmarkazi,Title="Type"}
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.WorkPlaceTypes).ReturnsDbSet(workPlaceTypes);

        var personRelations = new List<PersonRelation>
            {
              new () { Id = 1,Title="Related" }
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.PersonRelations).ReturnsDbSet(personRelations);

        var properties = new List<Property>
            {
              new () { Id = 1,CityId=1,ProvinceId=1,Address="Address",CustomerId=1,PropertyTypeId=1,CurrencyId=1 }
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.Properties).ReturnsDbSet(properties);

        var provinces = new List<Core.Entities.Province>
            {
              new () { Id = 1,Title="Province Title" }
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.Provinces).ReturnsDbSet(provinces);


        var individualCustomers = new List<IndividualCustomer>
            {
              new () { Id = 5,FirstName="Name",LastName="Family",BirthPlaceId=1 }
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.IndividualCustomers).ReturnsDbSet(individualCustomers);
        //--------------------
        var proposalDescriptions = new List<ProposalDescription>
            {
              new () { Id = 1,Category="branch-proposal",Descriptions="Desc1",ProposalId=1 },
              new () { Id = 2,Category= "customer-request",Descriptions="Desc3",ProposalId=1 },
            }.AsQueryable();
        _ = _mockDbContext.Setup(x => x.ProposalDescriptions).ReturnsDbSet(proposalDescriptions);
        _ = _mockDbContext.Setup(x => x.CompActvQuestions).ReturnsDbSet([new() { Id=1}]);


        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(200, result.Code);
        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.CorpCustomer);
        Assert.Equal("CityTitle", result.Data.CorpCustomer.RegisterPlace);
        Assert.Equal("12345", result.Data.MosavabeNumber);

    }


}

