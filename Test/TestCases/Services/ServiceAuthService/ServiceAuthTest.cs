using Application.Services.Auth.ServiceAuthService;
using Application.Services.RiskInfoItemService;
using Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.ServiceAuthService
{
    public class ServiceAuthTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void GetServiceAuthRequest_Success() =>
            Assert.NotNull(new GetServiceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchServiceAuthRequest_Success() =>
            Assert.NotNull(new SearchServiceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownServiceAuthRequest_Success() =>
            Assert.NotNull(new DropDownServiceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void AddServiceAuthRequest_Success() =>
            Assert.NotNull(new AddServiceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteServiceAuthRequest_Success() => 
            Assert.NotNull(new DeleteServiceRequestHandler(_unitOfWork.Object));
        [Fact]
        public void UpdateServiceAuthRequest_Success() =>
            Assert.NotNull(new UpdateServiceRequestHandler(_unitOfWork.Object));
    }
}
