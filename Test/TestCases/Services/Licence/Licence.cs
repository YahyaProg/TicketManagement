using Application.Services.LicenceService;
using Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.LicenceTest
{
    public class LicenceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        [Fact]
        public void AddLicenceRequest_Success() =>
            Assert.NotNull(new AddLicenceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteLicenceRequest_Success() =>
            Assert.NotNull(new DeleteLicenceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetByIdLicenceRequest_Success() =>
            Assert.NotNull(new GetLicenceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateLicenceRequest_Success() =>
            Assert.NotNull(new UpdateLicenceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchLicenceRequest_Success() =>
            Assert.NotNull(new SearchLicenceRequestHandler(_unitOfWork.Object));
    }
}
