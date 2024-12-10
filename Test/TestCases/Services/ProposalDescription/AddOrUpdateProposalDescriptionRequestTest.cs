using Application.Services.ProposalDescriptionService;
using Core.Entities;
using Core.ViewModel;
using Infrastructure;
using MockQueryable.Moq;
using Moq;

namespace Test.TestCases.Services.ProposalDescriptionTest;


public class AddOrUpdateProposalDescriptionRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task AddOrUpdateProposalDescriptionRequest_Add_Success()
    {
        //Arrange

        AddOrUpdateProposalDescriptionRequest request = new()
        {
            Category = "cat1",
            Descriptions = "new description",
            ProposalId = 1
        };


        List<Core.Entities.ProposalDescription> proposalDescriptions = [
            new()
            {
                Id = 1,
                Category = "cat1",
                Descriptions = "new description",
                ProposalId = 1
            }
        ];

        var mock = proposalDescriptions.BuildMock();
        _ = _unitOfWork.Setup(x => x.Repository.GetQueryable<Core.Entities.ProposalDescription>()).Returns(mock);
        _ = _unitOfWork.Setup(x => x.Repository.AddAsync<ProposalDescriptionAdd>(request, It.IsAny<CancellationToken>()));
        _ = _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(It.IsAny<CancellationToken>()))
        .ReturnsAsync(1);

        var _systemUnderTest = new AddOrUpdateProposalDescriptionRequestHandler(_unitOfWork.Object);

        //Act
        var result = await _systemUnderTest.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);

    }


    [Fact]
    public async Task AddOrUpdateProposalDescriptionRequest_Update_Success()
    {
        //Arrange
        AddOrUpdateProposalDescriptionRequest request = new()
        {
            Category = "cat1",
            Descriptions = "new description",
            ProposalId = 1
        };


        List<Core.Entities.ProposalDescription> proposalDescriptions = [
             new()
             {
                 Id = 1,
                 Category = "cat1",
                 Descriptions = "old description ",
                 ProposalId = 2
             }
        ];

        var mock = proposalDescriptions.BuildMock();
        _ = _unitOfWork.Setup(x => x.Repository.GetQueryable<Core.Entities.ProposalDescription>()).Returns(mock);

        _ = _unitOfWork.Setup(x => x.Repository.Update(request));
        _ = _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(It.IsAny<CancellationToken>()))
        .ReturnsAsync(1);
        var _systemUnderTest = new AddOrUpdateProposalDescriptionRequestHandler(_unitOfWork.Object);

        //Act
        var result = await _systemUnderTest.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }


    [Fact]
    public async Task AddOrUpdateProposalDescriptionRequest_Add_Fail()
    {
        //Arrange
        AddOrUpdateProposalDescriptionRequest request = new()
        {
            Category = "cat1",
            Descriptions = "description",
            ProposalId = 1
        };


        List<Core.Entities.ProposalDescription> proposalDescriptions = [
            new()
            {
                Id = 1,
                Category = "cat1",
                Descriptions = "new description",
                ProposalId = 1
            }
        ];

        var mock = proposalDescriptions.BuildMock();
        _ = _unitOfWork.Setup(x => x.Repository.GetQueryable<Core.Entities.ProposalDescription>()).Returns(mock);

        _ = _unitOfWork.Setup(x => x.Repository.AddAsync<ProposalDescriptionAdd>(It.IsAny<List<ProposalDescriptionAdd>>(), It.IsAny<CancellationToken>()));
        _ = _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);
        var _systemUnderTest = new AddOrUpdateProposalDescriptionRequestHandler(_unitOfWork.Object);

        //Act
        var result = await _systemUnderTest.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
    }


    [Fact]
    public async Task AddOrUpdateProposalDescriptionRequest_Update_Fail()
    {
        //Arrange
        AddOrUpdateProposalDescriptionRequest request = new()
        {
            Category = "cat1",
            Descriptions = "description",
            ProposalId = 1
        };

        List<Core.Entities.ProposalDescription> proposalDescriptions = [
            new()
            {
                Id = 1,
                Category = "cat1",
                Descriptions = "new description",
                ProposalId = 2
            }
        ];

        var mock = proposalDescriptions.BuildMock();
        _ = _unitOfWork.Setup(x => x.Repository.GetQueryable<Core.Entities.ProposalDescription>()).Returns(mock);

        _ = _unitOfWork.Setup(x => x.Repository.AddAsync<ProposalDescriptionAdd>(It.IsAny<List<ProposalDescriptionAdd>>(), It.IsAny<CancellationToken>()));
        _ = _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);
        var _systemUnderTest = new AddOrUpdateProposalDescriptionRequestHandler(_unitOfWork.Object);

        //Act
        var result = await _systemUnderTest.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
    }
}