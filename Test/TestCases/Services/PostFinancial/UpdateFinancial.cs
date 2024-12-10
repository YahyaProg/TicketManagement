using Application.Services.FinancialService;
using Core.Entities;
using Core.Enums;
using Core.ViewModel;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.PostFinancial
{
    public class UpdateFinancialTest
    {
        private readonly Mock<DBContext> _db = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly UpdateFinancialRequestHandler handler;
        public UpdateFinancialTest()
        {
            _unitOfWork.Setup(x => x.Context).Returns(_db.Object);
            handler = new UpdateFinancialRequestHandler(_unitOfWork.Object);
        }
        [Fact]
        public async Task UpdateFinancialRequest_Success()
        {
            var request = new UpdateFinancialRequest()
            {
                IdValues = new IdValuesType[]
               {
                   new IdValuesType()
                   {
                       Id=3
                   },
               },
                CorporateCustomerId = 25,
                CurrencyId = 20,
                Years = []
            };
            _db.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet([]);
            _unitOfWork.Setup(x => x.FinancialRepo.GetPreparedList(It.IsAny<long[]>())).
              ReturnsAsync(new List<FinancialPreparedListVM>()
              {
                  new()
                  {

                      FinancialInfoItem_Id = 1,
                      FinancialInfoItem_Value = 2 ,
                      FinancialInfoItem_FinancialInfoDefId=1,
                     CompanyFinancialInfo_Id = 4
                  }

              });
            _db.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(5);

            _db.Setup(x => x.FinancialInfoItems.FindAsync(It.IsAny<long>(), It.IsAny<CancellationToken>())).ReturnsAsync(
                new FinancialInfoItem()
                {
                    Id = 3,
                    CorporateCustomerId = 25,
                });
            _db.Setup(x => x.FinancialInfos).ReturnsDbSet([new()
            {
                CorporateCustomerId = 25,
                CurrencyId = 20,
            }]);
            Assert.NotNull(await handler.Handle(request, CancellationToken.None));
        }


        [Fact]
        public async Task UpdateFinancialRequest_Success2()
        {
            var request = new UpdateFinancialRequest()
            {
                IdValues = new IdValuesType[]
               {
                   new IdValuesType()
                   {
                       Id=1,
                       Value = 3
                   },
               },
                CorporateCustomerId = 25,
                CurrencyId = 20,
                Years = []
            };
            _db.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet([]);

            _unitOfWork.Setup(x => x.FinancialRepo.GetPreparedList(It.IsAny<long[]>())).
              ReturnsAsync(new List<FinancialPreparedListVM>()
              {
                  new()
                  {

                      FinancialInfoItem_Id = 1,
                      FinancialInfoItem_Value = 2 ,
                      FinancialInfoItem_FinancialInfoDefId=1,
                      FinancialInfo_FinancialYearInfoId=1,
                     CompanyFinancialInfo_Id = 3,
                     CompanyFinancialInfo_SubType=ECompanyFinancialInfo_subType.calculable,
                     CompanyFinancialInfo_ParentId = 12

                  },
                  new()
                  {

                      FinancialInfoItem_Id = 2,
                      FinancialInfoItem_Value = 2 ,
                      FinancialInfoItem_FinancialInfoDefId=1,
                      FinancialInfo_FinancialYearInfoId=1,
                     CompanyFinancialInfo_Id = 12,
                     CompanyFinancialInfo_SubType=ECompanyFinancialInfo_subType.leaf,

                  },

              });
            _db.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(5);

            _db.Setup(x => x.FinancialInfoItems.FindAsync(It.IsAny<long>(), It.IsAny<CancellationToken>())).ReturnsAsync(
                new FinancialInfoItem()
                {
                    Id = 3,
                    CorporateCustomerId = 25,
                });
            _db.Setup(x => x.FinancialInfos).ReturnsDbSet([new()
            {
                CorporateCustomerId = 25,
                CurrencyId = 20,
            }]);
            Assert.NotNull(await handler.Handle(request, CancellationToken.None));
        }



    }
}
