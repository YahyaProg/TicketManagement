using Application.Services.FinancialFormulaService;
using Core.Entities;
using Core.GenericResultModel;
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
using Test.Helper;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.FinancialFormulaService
{
    public class ApproveRequestHandlerTests
    {
        private readonly Mock<IUserHelper> _userHelperMock;

        public ApproveRequestHandlerTests()
        {

            _userHelperMock = new Mock<IUserHelper>();

        }

        [Fact]
        public async Task Handle_ApproveRequest_ReturnsFalseApiResult()
        {

            // Arrange
            var request = new ApproveRequest { Id = 1 };


            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            ApproveRequestHandler _handler = new ApproveRequestHandler(collection.Context.Object, _userHelperMock.Object);
            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet([ new()
            {
              Id =1,
              Approved = true
            } ]);

            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(-1);



            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ApproveRequest_ReturnsApiResult()
        {

            // Arrange
            var request = new ApproveRequest { Id = 1 };
            var user = new Core.ViewModel.UserDto { Id = Guid.NewGuid().ToString() };



            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            ApproveRequestHandler _handler = new ApproveRequestHandler(collection.Context.Object, _userHelperMock.Object);
            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet([ new()
            {
              Id =1,
              Title = "Test",
              Approved = false,
              Last = false,
            },
                new()
                {
                    Id=2,
                    Title= "Test",
                    Approved= true,
                    Last= true
                }
            ]);
            _userHelperMock.Setup(u => u.GetUserFromToken())
            .Returns(user);
            collection.Context.Setup(x => x.BankStaffs).ReturnsDbSet(new List<Core.Entities.BankStaff>()
            {
                new()
                {
                    Id = 1,
                    FirstName = "Test",
                    LastName = "Test",
                    UserId= "00000000-0000-0000-0000-000000000000",
                }
            });
            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);



            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

    }
}
