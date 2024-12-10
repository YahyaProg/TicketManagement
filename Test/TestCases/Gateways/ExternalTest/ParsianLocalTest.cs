//using Gateway.External.IServices;
//using Infrastructure.Repositories.Setting;
//using Microsoft.Extensions.Configuration;
//using Moq;
//using RestEase;
//using Gateway.External.Models.Requests;


//namespace Test.TestCases.Gateways.ExternalTest
//{
//    public class ParsianLocalTest()
//    {
//        private readonly IExternalServices service =  RestClient.For<IExternalServices>("https://external.saminray.com/api/v1/");
       
//        [Fact]
//        public async Task CbComsignTest()
//        {
//            var response = await service.CbComsign(It.IsAny<CbComsignRequest>());
            
//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task CbBgTest()
//        {
//            var response = await service.CbBg(It.IsAny<CbBgRequest>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task CbBgloanlcTest()
//        {
//            var response = await service.CbBgloanlc(It.IsAny<CbBgloanlcRequest>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task CbLoanTest()
//        {
//            var response = await service.CbLoan(It.IsAny<CbLoanRequest>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task CbLcTest()
//        {
//            var response = await service.CbLc(It.IsAny<CbLcRequest>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task CbCustomerTest()
//        {
//            var response = await service.CbCustomer(It.IsAny<CbCustomerRequest>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task CbCustrelTest()
//        {
//            var response = await service.CbCustrel(It.IsAny<CbCustrelRequest>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task CbDepositedChequesTest()
//        {
//            var response = await service.CbDepositedCheques(It.IsAny<CbDepositedChequesRequest>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task CbLatePaymentTest()
//        {
//            var response = await service.CbLatePayment(It.IsAny<int>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task CbLatePaymentDtlResponseTest()
//        {
//            var response = await service.CbLatePaymentDtlResponse(It.IsAny<int>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task CbSecuritiesTest()
//        {
//            var response = await service.CbSecurities(It.IsAny<CbSecuritiesRequest>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task CbSepordenaghdiTest()
//        {
//            var response = await service.CbSepordenaghdi(It.IsAny<CbSepordenaghdiRequest>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }
//    }
//}
