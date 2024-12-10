using Application.Services.BaseService;
using Application.Services.WhiteListChequeConfigService;
using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories.WhiteListChequeConfigRepository;
using MediatR;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Services.WhiteListChequeConfigReq
{
    public class AddOrUpdateWhiteListChequeConfigRequestTest
    {
        private readonly Mock<IRepository> _repository= new();
        private readonly Mock<IWhiteListChequeConfigRepo> _whiteListRepo= new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<IMediator> _mediator = new();
        [Fact]
        public async Task AddWhiteistChequeConfigRequest_Success()
        {
            var request = new AddWhiteListChequeConfigRequest()
            {
                Id = 10,
                NationalId = "NationalId",
                CorpId = "CorpId",
                CustomerType = Core.Enums.ECustomerType.CorporateCustomer,
                CompanyTypeId = 15,
                CorporateCustomerName = "CorporateCustomerName",
                CustomerBazargani = "CustomerBazargani",
                PaidPercent = 15,
                CurrentFund = 12,
                MaxAmount = 6521,
            };
    
            _whiteListRepo.Setup(x => x.GetById(It.IsAny<long>())).ReturnsAsync(new WhiteListChequeConfig()
            {
                Id = 10
            });
            _unitOfWork.Setup(x => x.WhiteListChequeConfigRepo).Returns(_whiteListRepo.Object);

            _repository.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(15);
            _mediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), It.IsAny<CancellationToken>())).
                ReturnsAsync(new Core.GenericResultModel.ApiResult<long>()
                {
                    Data = 15
                });
            _unitOfWork.Setup(X => X.Repository).Returns(_repository.Object);
            var handler = new AddWhiteListChequeConfigRequestHandler(_unitOfWork.Object, _mediator.Object);
            var result =await handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task AddWhiteistChequeConfigRequest_Successelse()
        {
            var request = new AddWhiteListChequeConfigRequest()
            {
                Id = 10,
                NationalId = "NationalId",
                CorpId = "CorpId",
                CustomerType = Core.Enums.ECustomerType.IndividualCustomer,
                CompanyTypeId = 15,
                CorporateCustomerName = "CorporateCustomerName",
                CustomerBazargani = "CustomerBazargani",
                PaidPercent = 15,
                CurrentFund = 12,
                MaxAmount = 6521,
                BirthDate = DateTime.Today,
                
            };

            _whiteListRepo.Setup(x => x.GetById(It.IsAny<long>())).ReturnsAsync(new WhiteListChequeConfig()
            {
                Id = 10
            });
            _unitOfWork.Setup(x => x.WhiteListChequeConfigRepo).Returns(_whiteListRepo.Object);

            _repository.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(15);
            _mediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), It.IsAny<CancellationToken>())).
                ReturnsAsync(new Core.GenericResultModel.ApiResult<long>()
                {
                    Data = 15
                });
            _unitOfWork.Setup(X => X.Repository).Returns(_repository.Object);
            var handler = new AddWhiteListChequeConfigRequestHandler(_unitOfWork.Object, _mediator.Object);
            var result = await handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);
        }
    }
}
