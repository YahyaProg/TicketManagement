using Application.Services.BankStaffService;
using Application.Services.CmntService;
using Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.BankStaffTest
{
    public class BankStaffTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void GetBankStaffRequest_Success() =>
            Assert.NotNull(new GetBankStaffRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchBankStaffRequest_Success() =>
            Assert.NotNull(new SearchBankStaffRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DropDownBankStaffRequest_Success() =>
            Assert.NotNull(new DropDownBankStaffRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownBankStaffRequest()
        {
            var request = new DropDownBankStaffRequest()
            {
                KeyWord = "a",
                Deleted = true,
                DefaultValue = 0
            };

            Assert.NotNull(request);

        }
    }
}
