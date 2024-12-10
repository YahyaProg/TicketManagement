using Application.Services.WorkplaceInfoService;
using Core.Entities;
using Core.Enums;
using Core.ViewModel;
using Moq;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.WorkPlaceInfo;

public class AddWorkplaceInfoRequestTest
{
    [Fact]
    public async Task AddWorkplaceInfoRequest_Fail()
    { 
        var moq = GetUnitOfWorkMoqCollection();

        moq.UnitOfWork.Setup(x => x.WorkplaceInfoRepo.AddCorporateCustomer(It.IsAny<AddWorkplaceInfo>(), It.IsAny<OccupationPlace>()));
        moq.UnitOfWork.Setup(x => x.Context.Add(It.IsAny<OccupationPlace>()));
        moq.UnitOfWork.Setup(x => x.Context.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new AddWorkplaceInfoRequestHandler(moq.UnitOfWork.Object);

        var request = new AddWorkplaceInfoRequest
        {
            OwnershipType = EOccupationPlace_ownershipType.ThirdPerson,
            CorpId = "1",
            NationalId = "1",
            CustomerType = ECustomerType.CorporateCustomer
        };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddWorkplaceInfoRequest_Success()
    {
        var moq = GetUnitOfWorkMoqCollection();

        moq.UnitOfWork.Setup(x => x.WorkplaceInfoRepo.AddIndividualCustomer(It.IsAny<AddWorkplaceInfo>(), It.IsAny<OccupationPlace>()));
        moq.UnitOfWork.Setup(x => x.Context.Add(It.IsAny<OccupationPlace>()));
        moq.UnitOfWork.Setup(x => x.Context.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new AddWorkplaceInfoRequestHandler(moq.UnitOfWork.Object);

        var request = new AddWorkplaceInfoRequest
        {
            OwnershipType = EOccupationPlace_ownershipType.ThirdPerson,
            CorpId = "1",
            NationalId = "1",
            CustomerType = ECustomerType.IndividualCustomer
        };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
