using Application.Services.BaseService;
using Application.Services.CompanyMembersInfoService;
using Core.Enums;
using Core.GenericResultModel;
using Core.Logger;
using FluentValidation.TestHelper;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.Managers
{
    public class AddManagerServiceTests
    {
        private readonly Mock<IMediator> mediator = new();
        private readonly Mock<ILoggerManager> logger = new();
        private readonly MoqCollection collection = GetUnitOfWorkMoqCollection();
        private readonly AddManagerRequest request = new()
        {
            ProposalSchemeId = 1,
            NationalId = "1",
            CorporateAgent = true,
            PositionTypeId = 1,
            CorpId = "1",
            CompanyTypeId = 1,
            Name = "1"
        };


        [Fact]
        public async Task Validation_Failed()
        {

            var request = new AddManagerRequest
            {
                CorporateAgent = true
            };

            var validator = new AddManagerValidation();

            var res = await validator.TestValidateAsync(request);

            res.ShouldHaveAnyValidationError();
        }

        [Fact]
        public async Task CustomerScheme_NotFound_Error()
        {
            collection.Context.Setup(x => x.CustomerSchemes).ReturnsDbSet(new List<Core.Entities.CustomerScheme>());

            var res = await Act();

            Assert.False(res.IsSuccess);
        }

        [Fact]
        public async Task InquiryIndividual_Failed()
        {
            var dbSet = new List<Core.Entities.CustomerScheme>
            {
                new()
                {
                    ProposalSchemeId = 1
                }
            };

            collection.Context.Reset();
            collection.Context.Setup(x => x.CustomerSchemes).ReturnsDbSet(dbSet);

            mediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ApiResult<long>(404, false)
                {
                    Message = "afarin",
                    MessageEn = "afarin"
                });

            var res = await Act();

            Assert.False(res.IsSuccess);
        }

        [Fact]
        public async Task InquiryCorporate_Failed()
        {
            var dbSet = new List<Core.Entities.CustomerScheme>
            {
                new()
                {
                    ProposalSchemeId = 1
                }
            };

            collection.Context.Reset();
            collection.Context.Setup(x => x.CustomerSchemes).ReturnsDbSet(dbSet);

            mediator.Setup(x => x.Send(It.Is<InquiryCustomerV2Request>(x => x.CustomerType == ECustomerType.IndividualCustomer), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ApiResult<long>(200, true)
                {
                    Data = 2,
                    Message = "afarin",
                    MessageEn = "afarin"
                });

            mediator.Setup(x => x.Send(It.Is<InquiryCustomerV2Request>(x => x.CustomerType == ECustomerType.CorporateCustomer), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ApiResult<long>(404, false)
                {
                    Message = "afarin",
                    MessageEn = "afarin"
                });

            var res = await Act();

            Assert.False(res.IsSuccess);
        }

        [Fact]
        public async Task Success()
        {
            var dbSet = new List<Core.Entities.CustomerScheme>
            {
                new()
                {
                    ProposalSchemeId = 1
                }
            };

            collection.Context.Reset();
            collection.Context.Setup(x => x.CustomerSchemes).ReturnsDbSet(dbSet);


            mediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ApiResult<long>(200, true)
                {
                    Data = 2,
                    Message = "afarin",
                    MessageEn = "afarin"
                });

            collection.Context
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(2);

            var res = await Act();

            Assert.True(res.IsSuccess);
        }

        private async Task<ApiResult> Act()
        {
            var handler = new AddManagerRequestHandler(collection.UnitOfWork.Object, mediator.Object, logger.Object);

            var res = await handler.Handle(request, CancellationToken.None);

            return res;
        }
    }
}
