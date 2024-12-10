using Application.Services.CustomerRequestService;
using Core.Entities;
using Core.Enums;
using Core.GenericResultModel;
using Core.Helpers;
using Core.ViewModel.CustomerRequests;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.ProposalTest;

public class AddProposalAndCustomerReqTests
{
    private readonly AddProposalAndCustomerReqRequest request = new()
    {
        CustomerId = 1,
        Description = " "
    };
    private readonly Mock<IUserHelper> helper;
    public AddProposalAndCustomerReqTests()
    {
        helper = new();
        helper.Setup(x => x.GetUserFromToken()).Returns(new Core.ViewModel.UserDto { Id = "1" });
    }

    [Fact]
    public async Task CustomerNotFound()
    {
        var moq = GetUnitOfWorkMoqCollection();

        moq.Context.Setup(x => x.Customers).ReturnsDbSet([]);

        var res = await GetRes(moq.UnitOfWork.Object);

        Assert.False(res.IsSuccess);
    }

    [Fact]
    public async Task HaveToDoProposal()
    {
        var moq = GetUnitOfWorkMoqCollection();

        moq.Context.Setup(x => x.Customers).ReturnsDbSet([
            new(){
                Id = request.CustomerId,
                CorporateCustomer = new(){
                    CorpId = "123456"
                }
            }
        ]);

        moq.Context.Setup(x => x.BankStaffs).ReturnsDbSet([
            new(){
                UserId = "1"
            }
        ]);

        moq.Context.Setup(x => x.Proposals).ReturnsDbSet([
            new(){
                CustomerId = request.CustomerId,
                Status = EProposal_status.todo
            }
        ]);

        var res = await GetRes(moq.UnitOfWork.Object);

        Assert.False(res.IsSuccess);
    }

    [Fact]
    public async Task ProposalSchemeNotFound()
    {
        var moq = GetUnitOfWorkMoqCollection();

        moq.Context.Setup(x => x.Customers).ReturnsDbSet([
            new(){
                Id = request.CustomerId,
                CorporateCustomer = new(){
                    CorpId = "123456"
                }
            }
        ]);

        moq.Context.Setup(x => x.BankStaffs).ReturnsDbSet([
            new(){
                UserId = "1"
            }
        ]);
        moq.Context.Setup(x => x.ProposalActionLogs).ReturnsDbSet([
           new(){
                UserId = "1"
            }
       ]);

        moq.Context.Setup(x => x.Proposals).ReturnsDbSet([]);

        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet([]);
        moq.Context.Setup(x => x.CustomerSchemes).ReturnsDbSet([]);

        moq.Context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

        var res = await GetRes(moq.UnitOfWork.Object);

        Assert.True(res.IsSuccess);
    }

    [Fact]
    public async Task Success()
    {
        var moq = GetUnitOfWorkMoqCollection();

        moq.Context.Setup(x => x.Customers).ReturnsDbSet([
            new(){
                Id = request.CustomerId,
                CorporateCustomer = new(){
                    CorpId = "123456"
                }
            }
        ]);

        moq.Context.Setup(x => x.BankStaffs).ReturnsDbSet([
            new(){
                UserId = "1"
            }
        ]);
        moq.Context.Setup(x => x.ProposalActionLogs).ReturnsDbSet([
           new(){
                UserId = "1"
            }
       ]);

        moq.Context.Setup(x => x.Proposals).ReturnsDbSet([
            new(){
                Id = 1,
                MosavabeNo = "1403",
                ProposalDescriptions = [
                    new()
                ],
                RiskInfos = [
                    new()
                ],
                Cmnts = [
                    new()
                ]
            }
        ]);


        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet([
            new Core.Entities.ProposalScheme()
            {
                CustomerId = request.CustomerId,
                ProposalId = 1,
                FinancialValues = [
                    new(){
                        Id = 1
                    },
                    new(){
                        Id = 2
                    }]
            }
        ]);
        moq.Context.Setup(x => x.CustomerSchemes).ReturnsDbSet([
            new(){
                CustomerId = request.CustomerId,
                Managers = [ new() ],
                Licences = [ new() ],
                IvbbFixedAsset = [ new() ],
                CompanyRelations = [ new() ],
                CompActvQuestions = [ new() ],
                CompQualQuestions = [ new() ],
                MajorStocksHolders = [ new() ],
                TradingCompanyActivities = [ new() ],
                OccupationPlaces = [ new() ],
            }
        ]);

        moq.Context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

        var res = await GetRes(moq.UnitOfWork.Object);

        Assert.True(res.IsSuccess);
    }

    private async Task<ApiResult<AddProposalAndCustomerReqRequestVM>> GetRes(IUnitOfWork unitOfWork)
    {
        var handler = new AddProposalAndCustomerReqRequestHandler(unitOfWork, helper.Object);

        return await handler.Handle(request, CancellationToken.None);
    }
}
