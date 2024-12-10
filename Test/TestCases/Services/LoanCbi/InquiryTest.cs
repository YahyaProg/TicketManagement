using Application.Services.LoanCbiService;
using Application.Services.LoanMebService;
using Azure;
using Core.Entities;
using Core.GenericResultModel;
using Core.Helpers;
using Core.Logger;
using EntityFrameworkCoreMock;
using Gateway.External.IServices;
using Gateway.Model.External.Requests;
using Gateway.Model.External.Responses;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;
using Newtonsoft.Json;
using System.DirectoryServices.Protocols;
using System.Threading;
using Test.Helper;

namespace Test.TestCases.Services.LoanCbi
{
    public class InquiryTest
    {




        public static IEnumerable<object[]> requests =>
       new List<object[]>
       {
            new object[] { true, true, false, 1},
            new object[] { false, true, false, 2},
            new object[] { false, false, true, 3}
       };


        private readonly Mock<ILoggerManager> logger = new();
        private readonly Mock<IExternalServices> exService = new();
        private readonly Mock<DBContext> _contextMock = new();


        [Theory, MemberData(nameof(requests))]
        public async void inquiryReturningChequeCbi(bool isManager, bool isCorp, bool isIndv, int reqType)
        {
            // Arrange
            InitSetting settings = new()
            {
                ExternalSettings = new()
                {
                    BaseUrl = "https://external.saminray.com/api/v1/"
                }
            };
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            var system = new InquiryCbRequestHandler(collection.UnitOfWork.Object, logger.Object, exService.Object, settings);

            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

            #region external services
            var estelamZameRes = MoqHelper.GetExternalResponseMoq(new EstelamZamenResponse()
            {
                EstelamZamenRows = new List<EstelamZamenRow>
                {
                    new EstelamZamenRow(){RequestNum= "2335", AmOriginal = "123", AmBedehiKol = "213", AmBenefit = "2131", AmDirkard = "14123", AmMoavagh = "23", AmTahod = " 21312", AmEltezam = "21312", AmMashkuk = "123", AmSarResid= "213", AmSukht = "231", DateSarResid = "2022-01-01", ShobeName = "sad", Date = "2022-01-01", ZmntIdNo = "13", ZmntLgId = "213"  }
                }
            });
            exService.Setup(x => x.EstelamZamen(It.IsAny<EstelamZamenRequest>())).ReturnsAsync(estelamZameRes);


            var estelamVasiqeRes = MoqHelper.GetExternalResponseMoq(new EstelamVasigheResponse()
            {
                EstelamVasigheRows = new List<EstelamVasigheRows>
                {
                    new EstelamVasigheRows(){Amount = "2321", Priority = "2", Type ="10"}
                }
            });
            exService.Setup(x => x.EstelamVasighe(It.IsAny<EstelamVasigheRequest>())).ReturnsAsync(estelamVasiqeRes);


            var estelamSamatRes = MoqHelper.GetExternalResponseMoq(new SamatEstelamResponse()
            {
                TashilatRows = new List<TashilatRow>
                {
                    new TashilatRow()
                    {
                        BankCode = "23",
                        DasteBandi = "اصلی",
                        BranchCode = "23",
                        RequstType = reqType,
                        AmountBenefit = 23,
                        AmountDirkard= 2143,
                        AmountSukht = 232,
                        DateSarResid = "2023-01-24",
                        DateEstemhal = "2023-05-13",
                        AmountEltezam = 2323,
                        AmountMashkuk = 0,
                        AmountSarResid = 0,
                        AmountMoavagh = 0,
                        AmountOriginal = 23232,
                        AmountTahod = 23232,
                        AmountTotalDebt = 232,
                        BranchName ="amirhosein",
                        ContractDate ="2023-04-10",
                        LegalPersonNaionalId = "232234",
                        LegalPersonName = "amirhosein",
                        RealPersonName = "amirhosein2",
                        RealPersonNationalCode = "2050213192",
                        RequestNumber = "232341",
                        Type = "10"
                    }
                }
            });
            exService.Setup(x => x.SamatEstelam(It.IsAny<SamatEstelamRequest>())).ReturnsAsync(estelamSamatRes);
            #endregion

            #region Moq DBSETS

            //            CreditStatuses
            //LoanStatuses
            //LoanTypes

            collection.Context.Setup(x => x.CreditStatuses).ReturnsDbSet(new List<Core.Entities.CreditStatus>());
            collection.Context.Setup(x => x.LoanStatuses).ReturnsDbSet(new List<Core.Entities.LoanStatus>());
            collection.Context.Setup(x => x.LoanTypes).ReturnsDbSet(new List<Core.Entities.LoanType>());

            collection.Context.Setup(x => x.ManagerDebts).ReturnsDbSet(new List<Core.Entities.ManagerDebt>()
            {
                new Core.Entities.ManagerDebt(){ Id = 1, CustomerId = 1, LastEtelam = true }
            });

            collection.Context.Setup(x => x.Bgcbis).ReturnsDbSet(new List<Bgcbi>()
            {
                new Bgcbi(){ Id = 1, LastEtelam = true, CustomerId = 1 }
            });

            collection.Context.Setup(x => x.Lccbis).ReturnsDbSet(new List<Lccbi>()
            {
                new Lccbi(){ Id = 1, LastEtelam = true, CustomerId = 1 }
            });
            collection.Context.Setup(x => x.LoanCbis).ReturnsDbSet(new List<Core.Entities.LoanCbi>()
            {
                new Core.Entities.LoanCbi(){ Id = 1, LastEtelam = true, CustomerId = 1 }
            });

            collection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet(new List<Core.Entities.ProposalScheme>()
            {
                new Core.Entities.ProposalScheme() {Id = 1, ProposalId = 1, CustomerId = 1}
            });
            collection.Context.Setup(x => x.Proposals).ReturnsDbSet(new List<Core.Entities.Proposal>()
            {
                new Core.Entities.Proposal() {Id = 1, CustomerId = 1}
            });
            collection.Context.Setup(x => x.VasigheCbis).ReturnsDbSet(new List<VasigheCbi>());
            collection.Context.Setup(x => x.Cbis).ReturnsDbSet(new List<Cbi>());
            collection.Context.Setup(x => x.ZamenCbis).ReturnsDbSet(new List<ZamenCbi>());
            collection.Context.Setup(x => x.CustomerSchemes).ReturnsDbSet(new List<Core.Entities.CustomerScheme>()
            {
                new Core.Entities.CustomerScheme() {Id = 1, ProposalSchemeId = 1}
            });
            collection.Context.Setup(x => x.OtherBankBranches).ReturnsDbSet(new List<OtherBankBranch>()
            {
                new OtherBankBranch(){ Code = "1123"}
            });
            collection.Context.Setup(x => x.Managers).ReturnsDbSet(new List<Manager>()
            {
                new Manager() {Id = 1, CustomerSchemeId = 1, CorpBoardOfDirectorId = 1, Person = new() { NationalId = "20510" } }
            });

            collection.Context.Setup(x => x.Banks).ReturnsDbSet(new List<Bank>());

            collection.Context.Setup(x => x.Currencies).ReturnsDbSet(new List<Core.Entities.Currency>());

            // if test for corp customer or manager
            if (isCorp)
                collection.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet(new List<CorporateCustomer>()
                {
                    new CorporateCustomer() {Id = 1, CorpId = "2906"}
                });
            else
                collection.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet(new List<CorporateCustomer>());

            // if test for indv customer
            if (isIndv)
                collection.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet(new List<IndividualCustomer>()
                {
                    new IndividualCustomer() {Id = 1, NationalId = "205"}
                });
            else
                collection.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet(new List<IndividualCustomer>());

            #endregion


            var request = new InquiryCbRequest()
            {
                manager = isManager,
                proposalSchemeId = 1
            };

            // Act
            var result = await system.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }



        public static IEnumerable<object[]> values =>
             new List<object[]>
             {
                    new object[] { 0, 0, 0, 0},//1
                    new object[] { 1, 0, 0, 0}, //2
                    new object[] { 0, 0, 2, 0}, //3
                    new object[] { 0, 2, 0, 0}, //4
                    new object[] { 0,0, 0, 3}, //1
             };

        [Theory, MemberData(nameof(values))]
        public void InqueryGetTashilatTitleTest(double amSarResid, double amMoavagh, double amMashkuk, double amSukht)
        {

            // Act
            var res = InquiryCbRequestHandler.GetTashilatStatusTitle(amSarResid, amMoavagh, amMashkuk, amSukht);

            // Assert
            Assert.NotNull(res);
        }


        [Theory, MemberData(nameof(values))]
        public void InqueryGetLoanStatusTitleTest(int amSarResid, int amMoavagh, int amMashkuk, int amSukht)
        {

            // Act
            var res = InquiryCbRequestHandler.GetLoanStatusTitle(amSarResid, amMoavagh, amMashkuk, amSukht);

            // Assert
            Assert.NotNull(res);
        }



        [Fact]
        public async void EstelamTashilatManagerTest()
        {
            var initSetting = new InitSetting
            {
                ExternalSettings = new ExternalSettings
                {
                    Rate = 1
                }
            };
            // Arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            var system = new InquiryCbRequestHandler(collection.UnitOfWork.Object, logger.Object, exService.Object, initSetting);

            collection.Context.Setup(x => x.Currencies).ReturnsDbSet(new List<Core.Entities.Currency>());
            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);
            var estelamZameRes = MoqHelper.GetExternalResponseMoq(new SamatEstelamResponse()
            {
                TashilatRows = new()
             {
                 new()
                 {
                     RequstType = 2,
                     RequestNumber = "23",
                     DasteBandi = "اصلی",
                     AmountTotalDebt = 2323,
                     AmountTahod = 232,
                     AmountMoavagh = 23,
                     AmountBenefit = 232,
                     AmountMashkuk = 232,
                     AmountDirkard = 232,
                     AmountSukht = 232,
                     AmountEltezam = 232,
                     AmountSarResid = 232,
                     AmountOriginal = 2132
                 },
                  new()
                 {
                     RequstType = 1,
                     RequestNumber = "23",
                     DasteBandi = "sadasd",
                     AmountTotalDebt = 2323,
                     AmountTahod = 232,
                     AmountMoavagh = 23,
                     AmountBenefit = 232,
                     AmountMashkuk = 232,
                     AmountDirkard = 232,
                     AmountSukht = 232,
                     AmountEltezam = 232,
                     AmountSarResid = 232,
                     AmountOriginal = 2132
                 },
                   new()
                 {
                     RequstType = 3,
                     RequestNumber = "23",
                     DasteBandi = "sadasd",
                     AmountTotalDebt = 2323,
                     AmountTahod = 232,
                     AmountMoavagh = 23,
                     AmountBenefit = 232,
                     AmountMashkuk = 232,
                     AmountDirkard = 232,
                     AmountSukht = 232,
                     AmountEltezam = 232,
                     AmountSarResid = 232,
                     AmountOriginal = 2132
                 },
                   new()
                 {
                     RequstType = 3,
                     RequestNumber = "23",
                     DasteBandi = "اصلی",
                     AmountTotalDebt = 2323,
                     AmountTahod = 232,
                     AmountMoavagh = 23,
                     AmountBenefit = 232,
                     AmountMashkuk = 232,
                     AmountDirkard = 232,
                     AmountSukht = 232,
                     AmountEltezam = 232,
                     AmountSarResid = 232,
                     AmountOriginal = 2132
                 },
             }
            });
            exService.Setup(x => x.SamatEstelam(It.IsAny<SamatEstelamRequest>())).ReturnsAsync(estelamZameRes);
            collection.Context.Setup(x => x.ManagerDebts).ReturnsDbSet(new List<Core.Entities.ManagerDebt>());
            Manager man = new()
            {
                Id = 1,
                Person = new()
                {
                    NationalId = "232"
                }
            };

            Core.Entities.Proposal proposal = new()
            {
                CustomerId = 1,
                Id = 1,

            };

            // Act
            var res = await system.EstelamTashilatManager(man, proposal, CancellationToken.None);


            // Assert
            Assert.Null(res);
        }

        [Fact]
        public async Task EstelamZamen_ReturnsMessage_WhenNotSuccessful()
        {

            var initSetting = new InitSetting
            {
                ExternalSettings = new ExternalSettings
                {
                    Rate = 1
                }
            };
            string expectedResult = "zamenCbi operation failed";
            // Arrange
            var proposal = new Proposal { Id = 1, CustomerId = 123 };
            var row = new EstelamZamenRow { RequestNum = "1234",
                Date = "2022-01-01",
                DateSarResid = "2023-01-01",
                ShobeCode = "1",
                ShobeName = "شعبه-مرکزی"
            };
            var zamenType = "10";
            var nationalCode = "987654321";
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            var _service = new InquiryCbRequestHandler(collection.UnitOfWork.Object, logger.Object, exService.Object, initSetting);
           

            var estelamZameRes = MoqHelper.GetExternalResponseMoq(new SamatEstelamResponse()
            {
                TashilatRows = new List<TashilatRow>
                {
                    new TashilatRow { 
                        BankCode = "BK01", 
                        CurrencyCode = "USD", 
                        RequstType = 3, 
                        Type = "10", 
                        DasteBandi = "اصلی",
                        BranchName = "شعبه-مرکزی",
                        BranchCode = "1"
                    }
                },
            });
            

            exService.Setup(x => x.SamatEstelam(It.IsAny<SamatEstelamRequest>())).ReturnsAsync(estelamZameRes);
            collection.Context.Setup(x => x.Banks).ReturnsDbSet(new List<Bank> { new Bank { Code = "BK01", Id = 1 } });
            collection.Context.Setup(x => x.Currencies).ReturnsDbSet( new List<Currency> { new Currency { Code = "USD", Id = 1 } });
            collection.Context.Setup(x => x.ZamenCbis).ReturnsDbSet(new List<ZamenCbi>());

            // Act
            var result = await _service.EstelamZamen(proposal, zamenType, nationalCode, row, CancellationToken.None);

            // Assert
            Assert.Equal(result, expectedResult); 

        }


        [Theory]
        [InlineData(10, "قرض الحسنه")]
        [InlineData(11, "مشاركت مدني")]
        [InlineData(14, "مضاربه")]
        [InlineData(15, "سلف")]
        [InlineData(16, "فروش اقساطي")]
        [InlineData(19, "اجاره به شرط تمليك")]
        [InlineData(20, "جعاله")]
        [InlineData(23, "خريد دين")]
        [InlineData(26, "مرابحه")]
        [InlineData(51, "مناقصه غير دولتي")]
        [InlineData(52, "فرآیند ارجاع کار")]
        [InlineData(53, "مزايده غیر دولتي")]
        [InlineData(54, "حسن اجرای  غیر دولتی")]
        [InlineData(55, "حسن اجرای دولتی")]
        [InlineData(56, "پیش پرداخت غیر دولتی")]
        [InlineData(57, "پیش پرداخت  دولتی")]
        [InlineData(58, "استراداد كسور غیر دولتی")]
        [InlineData(59, "استراداد كسور دولتی")]
        [InlineData(60, "تعهد پرداخت")]
        [InlineData(61, "گمرکی")]
        [InlineData(70, "مشاركت مدني ارز")]
        [InlineData(75, "سلف ارز")]
        [InlineData(80, "فروش اقساطي ارز")]
        [InlineData(85, "خريد دين ارز")]
        [InlineData(90, "پرداخت شده")]
        [InlineData(100, "")] // Test default case
        public void GetZamenReqTypeTitle_ShouldReturnExpectedResult(int code, string expected)
        {
            // Act
            string result = InquiryCbRequestHandler.GetZamenReqTypeTitle(code);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
