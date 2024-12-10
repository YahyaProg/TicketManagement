using Application.Services.BaseService;
using Application.Services.BlackListChequeConfigService;
using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories.BlackListChequeConfigRepositor;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Services.BlackListChequeConfigRequest
{
    public class AddBlackListChequeConfigRequestTest
    {
        private readonly Mock<IRepository> _repository = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<IBlackListChequeConfigRepo> _blackListRepo = new();
        private readonly Mock<IMediator> _mediator = new();
        private readonly Mock<DBContext> _contextMoq = new();
        [Fact]
        public void AddBlackListChequeConfigRequest_Success()
        {

            var request = new AddBlackListChequeConfigRequest()
            {
                CustomerType = Core.Enums.ECustomerType.CorporateCustomer,
                NationalId = "NationalId",
                CorpId = "CorpId",
                Foreign = false,
                CompanyTypeId = 110562,
                CorporateCustomerName = "CorporateCustomerName",
                CustomerBazargani = "CustomerBazargani",
                PaidPercent = 25,
                ChequeCollateralId = 15,
                CurrentFund = 15,

            };
            _blackListRepo.Setup(x => x.GetById(It.IsAny<long>())).ReturnsAsync(new BlackListChequeConfig()
            {
                Id = 25,
                Version = 95,
                ChequeCollateralId = 15,
                CustomerId = 36,
                ChequeCollateral = new CurrentChequeConfig()
                {
                    Id = 15,
                    Version = 98,
                    MaxAmountPerCheque = 1000000000000000,
                    MaxDurationDays = 9525,
                }
            });
            _unitOfWork.Setup(x => x.BlackListChequeConfigRepo).Returns(_blackListRepo.Object);

            _repository.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).
                ReturnsAsync(958);
            _contextMoq.Setup(x => x.CorporateCustomers).ReturnsDbSet([new()
            {
              Id = 15
            }]);
            _contextMoq.Setup(x => x.Customers).ReturnsDbSet([new()
            {
                Id =36,
                IndividualCustomer = new IndividualCustomer() {Id = 15 } ,
                Foreign = false,
                CorporateCustomer = new CorporateCustomer(){ Id = 15 }
            }]);
            _contextMoq.Setup(x => x.BlackListChequeConfigs).ReturnsDbSet([new BlackListChequeConfig()
            {
                Id = 25,
                Version = 95,
                ChequeCollateralId = 15,
                CustomerId = 36,
                ChequeCollateral = new CurrentChequeConfig()
                {
                    Id = 15,
                    Version = 98,
                    MaxAmountPerCheque = 1000000000000000,
                    MaxDurationDays = 9525,
                }
            }]);
            _unitOfWork.Setup(x => x.Context).Returns(_contextMoq.Object);
            _mediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), It.IsAny<CancellationToken>())).
                ReturnsAsync(new Core.GenericResultModel.ApiResult<long>()
                {
                    Data = 36
                });


            var handler = new AddBlackListChequeConfigRequestHandler(_unitOfWork.Object, _mediator.Object);
            var result = handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);

        }
        [Fact]
        public void AddBlackListChequeConfigRequest_SuccessElse()
        {

            var request = new AddBlackListChequeConfigRequest()
            {
                CustomerType = Core.Enums.ECustomerType.IndividualCustomer,
                NationalId = "NationalId",
                CorpId = "CorpId",
                Foreign = false,
                CompanyTypeId = 110562,
                CorporateCustomerName = "CorporateCustomerName",
                CustomerBazargani = "CustomerBazargani",
                PaidPercent = 25,
                ChequeCollateralId = 15,
                CurrentFund = 15,

            };
            _blackListRepo.Setup(x => x.GetById(It.IsAny<long>())).ReturnsAsync(new BlackListChequeConfig()
            {
                Id = 25,
                Version = 95,
                ChequeCollateralId = 15,
                CustomerId = 36,
                ChequeCollateral = new CurrentChequeConfig()
                {
                    Id = 15,
                    Version = 98,
                    MaxAmountPerCheque = 1000000000000000,
                    MaxDurationDays = 9525,
                }
            });
            _unitOfWork.Setup(x => x.BlackListChequeConfigRepo).Returns(_blackListRepo.Object);

            _repository.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).
                ReturnsAsync(958);
            _contextMoq.Setup(x => x.CorporateCustomers).ReturnsDbSet([new()
            {
              Id = 15
            }]);
            _contextMoq.Setup(x => x.IndividualCustomers).ReturnsDbSet([new()
            {
              Id = 15
            }]);
            _contextMoq.Setup(x => x.Customers).ReturnsDbSet([new()
            {
                Id =36,
                IndividualCustomer = new IndividualCustomer() {Id = 15 } ,
                Foreign = false,
                CorporateCustomer = new CorporateCustomer(){ Id = 15 }
            }]);
            _contextMoq.Setup(x => x.BlackListChequeConfigs).ReturnsDbSet([new BlackListChequeConfig()
            {
                Id = 44,
                Version = 95,
                ChequeCollateralId = 15,
                CustomerId = 36,
                ChequeCollateral = new CurrentChequeConfig()
                {
                    Id = 15,
                    Version = 98,
                    MaxAmountPerCheque = 1000000000000000,
                    MaxDurationDays = 9525,
                }
            }]);
            _unitOfWork.Setup(x => x.Context).Returns(_contextMoq.Object);
            _mediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), It.IsAny<CancellationToken>())).
                ReturnsAsync(value: new Core.GenericResultModel.ApiResult<long>()
                {
                    Data = 36
                });


            var handler = new AddBlackListChequeConfigRequestHandler(_unitOfWork.Object, _mediator.Object);
            var result = handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);

        }
    }
}
