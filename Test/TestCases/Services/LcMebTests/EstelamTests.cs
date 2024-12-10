using Amazon.Runtime.Internal.Util;
using Application.Services.BgmebService;
using Core.Entities;
using Core.GenericResultModel;
using Core.Helpers;
using Gateway.External.IServices;
using Gateway.Model.External.Requests;
using Gateway.Model.External.Responses;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Helper;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.LcMebTests;

public class EstelamBgMebTests
{
    private readonly EstelamBgMebRequest request = new()
    {
        CustomerId = 1,
        ProposalId = 1,
    };

    private readonly InitSetting initSetting = new InitSetting()
    {
        ExternalSettings = new()
        {
            Rate = 1
        }
    };

    private readonly MoqCollection moq = GetUnitOfWorkMoqCollection();

    private readonly Mock<IExternalServices> externalServices = new();

    private readonly EstelamBgMebRequestHandler handler;
    public EstelamBgMebTests()
    {

        // arrange
        var proposal = new Proposal()
        {
            Id = request.ProposalId,
            Customer = new()
            {
                Id = request.CustomerId,
                CorporateCustomer = new()
                {
                    Id = request.CustomerId
                },
                IndividualCustomer = new()
                {
                    Id = request.CustomerId,
                }
            }
        };

        var proposalScheme = new Core.Entities.ProposalScheme()
        {
            Proposal = proposal,
            Deleted = false,
            CreateDate = DateTime.Now,
        };

        var customerScheme = new Core.Entities.CustomerScheme()
        {
            ProposalScheme = proposalScheme,
        };

        var manager = new Core.Entities.Manager()
        {
            CustomerScheme = customerScheme,
            PersonId = 123
        };

        moq.Context.Setup(x => x.Proposals).ReturnsDbSet([proposal]);

        var CbCustomerResponse = new CbCustomerResponse()
        {
            CustomerId = request.CustomerId.ToString(),
        };
        externalServices.Setup(x => x.CbCustomer(It.IsAny<CbCustomerRequest>())).ReturnsAsync(CbCustomerResponse.GetExternalResponseMoq());

        var CbBgResponse = new List<CbBgResponse>()
        {
            new()
            {
                CustomerId = request.CustomerId.ToString(),
                AccountNo = "1",
                BgNewamt = 1,
                BgType = "ajab",
                CurrencyCode = "irr",
                OutSatnding = 1,
                Settled = true,
                NpaclassCode = "APL"
            }
        };
        externalServices.Setup(x => x.CbBg(It.IsAny<CbBgRequest>())).ReturnsAsync(CbBgResponse.GetExternalResponseMoq());

        moq.Context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet([proposalScheme]);

        moq.Context.Setup(x => x.CustomerSchemes).ReturnsDbSet([customerScheme]);

        moq.Context.Setup(x => x.Managers).ReturnsDbSet([manager]);

        moq.Context.Setup(x => x.LoanMebs).ReturnsDbSet([new() {
            AccountNo = "1",
            ProposalId = request.ProposalId,
        }]);

        moq.Context.Setup(x => x.Customers).ReturnsDbSet([new() {
            Id = manager.PersonId,
            IndividualCustomer = new(){
                NationalId = "1",
            }
        }]);

        moq.Context.Setup(x => x.ProposalManagerBgs).ReturnsDbSet([new() {
            ProposalId = request.ProposalId,
            Bg = null
        }]);
           
        handler = new EstelamBgMebRequestHandler(moq.UnitOfWork.Object, externalServices.Object, initSetting);
    }

    [Theory]
    [InlineData((long)12,(long)1,"ajab","irr")]
    [InlineData((long)12,(long)1,"","")]
    [InlineData((long)1,null, "ajab", "irr")]
    [InlineData((long)1,(long)1, "ajab", "irr")]
    public async Task Success(long BgMeb_CustomerId, long? BgMeb_ProposalId, string bankGuaranteeType_Title, string currency_Code)
    {
        moq.Context.Setup(x => x.Bgmebs).ReturnsDbSet([new() {
            CustomerId = BgMeb_CustomerId,
            AccountNo = "1",
            ProposalId = BgMeb_ProposalId,
        }]);

        moq.Context.Setup(x => x.BankGuaranteeTypes).ReturnsDbSet([new() {
            Title = bankGuaranteeType_Title,
        }]);

        moq.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet([new() {
            Id = 1,
            ProposalId = 1
        }]);

        moq.Context.Setup(x => x.Currencies).ReturnsDbSet([new() {
            Code = currency_Code,
        }]);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.True(res.IsSuccess);
    }

}
