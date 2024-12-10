using Application.Services.ProvinceService;
using Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.ProvinceTest
{
    public class ProvinceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        [Fact]
        public void AddProvinceRequest_Success() =>
            Assert.NotNull(new AddProvinceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteProvinceRequest_Success() =>
            Assert.NotNull(new DeleteProvinceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetByIdProvinceRequest_Success() =>
            Assert.NotNull(new GetProvinceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateProvinceRequest_Success() =>
            Assert.NotNull(new UpdateProvinceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchProvinceRequest_Success() =>
            Assert.NotNull(new SearchProvinceRequestHandler(_unitOfWork.Object));
    }
}
