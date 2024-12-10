using Application.Services.AdjustmentFormulaService;
using Core.Entities;
using Core.Helpers;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.AdjustmentFormulaTests
{
    public class AddAdjustmentFormulaTest
    {
        private readonly AddAdjustmentFormulaRequest Request = new()
        {
            Title = "",
            Items = [new() {
                Title = "a",
                Value = 1
            }]
        };

        private readonly Mock<IUserHelper> UserHelper = new();

        public AddAdjustmentFormulaTest()
        {
            UserHelper.Setup(x => x.GetUserFromToken()).Returns(new Core.ViewModel.UserDto()
            {
                Id = "1"
            });
        }

        [Fact]
        public async Task BankStaff_NotFound()
        {
            var context = new Mock<DBContext>();

            context.Setup(x => x.BankStaffs).ReturnsDbSet([]);

            var handler = new AddAdjustmentFormulaRequestHandler(context.Object, UserHelper.Object);

            var res = await handler.Handle(Request, CancellationToken.None);

            Assert.False(res.IsSuccess);
        }

        [Fact]
        public async Task BankStaff_Success()
        {
            var context = new Mock<DBContext>();

            context.Setup(x => x.BankStaffs).ReturnsDbSet([new() {
                UserId = "1",
            }]);

            context.Setup(x => x.RiskAdjustmentGroups).ReturnsDbSet([]);

            context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

            var handler = new AddAdjustmentFormulaRequestHandler(context.Object, UserHelper.Object);

            var res = await handler.Handle(Request, CancellationToken.None);

            Assert.True(res.IsSuccess);
        }
    }
}
