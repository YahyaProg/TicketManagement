//using Core.Entities;
//using Gateway.External.IServices;
//using Gateway.External.Models.Requests;
//using Gateway.External.Models.Responses;
//using Moq;
//using RestEase;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Test.TestCases.Gateways.ExternalTest
//{
//    public class ParsianTest
//    {
//        private readonly IExternalServices service = RestClient.For<IExternalServices>("https://external.saminray.com/api/v1/");


//        [Fact]
//        public async Task ShebaEnqTest()
//        {
//            var response = await service.ShebaEnq(It.IsAny<string>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task AverageAccountBalanceTest()
//        {
//            var response = await service.AverageAccountBalance(It.IsAny<AverageAccountBalanceRequest>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task EstelamCheque()
//        {
//            var response = await service.EstelamCheque(It.IsAny<EstelamChequeRequest>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task EstelamChequeDetail()
//        {
//            var response = await service.EstelamChequeDetail(It.IsAny<string>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task GetIdentification()
//        {
//            var response = await service.GetIdentification(It.IsAny<GetIdentificationRequest>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task InquiryMilitary()
//        {
//            var response = await service.InquiryMilitary(It.IsAny<string>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task SamatEstelam()
//        {
//            var response = await service.SamatEstelam(It.IsAny<SamatEstelamRequest>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task EstelamVasighe()
//        {
//            var response = await service.EstelamVasighe(It.IsAny<EstelamVasigheRequest>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task EstelamZamen()
//        {
//            var response = await service.EstelamZamen(It.IsAny<EstelamZamenRequest>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task InquiryShahkar()
//        {
//            var response = await service.InquiryShahkar(It.IsAny<InquiryShahkarRequest>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }

//        [Fact]
//        public async Task InquiryPostalCode()
//        {
//            var response = await service.InquiryPostalCode(It.IsAny<string>());

//            Assert.NotNull(response.ResponseMessage.Content);
//        }
//    }
//}
