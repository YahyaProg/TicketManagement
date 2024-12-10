using Application.Services.ManufacturingLicenceService;
using Application.Services.ServiceCompanyRankService;
using Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.ManufacturingLicenceTest
{
    public class ManufacturingLicence
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        [Fact]
        public void AddManufacturingLicenceRequest_Success() =>
            Assert.NotNull(new AddManufacturingLicenceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteManufacturingLicenceRequest_Success() =>
            Assert.NotNull(new DeleteManufacturingLicenceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetByIdManufacturingLicenceRequest_Success() =>
            Assert.NotNull(new GetManufacturingLicenceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateManufacturingLicenceRequest_Success() =>
            Assert.NotNull(new UpdateManufacturingLicenceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchManufacturingLicenceRequest_Success() =>
            Assert.NotNull(new SearchManufacturingLicenceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownManufacturingLicenceRequest_Success() =>
           Assert.NotNull(new DropDownManufacturingLicenceRequestHandler(_unitOfWork.Object));


        [Fact]
        public void DropDownManufacturingLicenceRequest()
        {
            var request = new DropDownManufacturingLicenceRequest()
            {
                KeyWord = "a"
            };

            Assert.NotNull(request);

        }
    }
}
