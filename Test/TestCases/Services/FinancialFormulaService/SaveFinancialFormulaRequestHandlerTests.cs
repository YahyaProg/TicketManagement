using Application.Services.FinancialFormulaService;
using Core.Entities;
using Core.Helpers;
using Infrastructure;
using Keycloak.AuthServices.Sdk.Admin.Models;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Helper;

namespace Test.TestCases.Services.FinancialFormulaService
{
    public class SaveFinancialFormulaRequestHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsApiResultWithFailedAdd_WhenRequestIsValid()
        {
            // Arrange
            var userHelperMock = new Mock<IUserHelper>();
            var user = new Core.ViewModel.UserDto { Id = new Guid().ToString() };

            var request = new SaveFinancialFormulaRequest
            {
                Id = 2,
                title = "Title",
                formula = "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
                cutoff = "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
                cutoff2 = "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
                prevYearPercents = "[{\"value\": 50}, {\"value\": 50}]",
                customize = false
            };
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet(new List<Core.Entities.FinancialFormula>() { new()
            {
              Id =1,
              Approved = true
            } });
            userHelperMock.Setup(u => u.GetUserFromToken())
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

            var handler = new SaveFinancialFormulaRequestHandler(collection.UnitOfWork.Object, userHelperMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task Handle_ReturnsApiResultWithSuccessUpdate_WhenRequestIsValid()
        {
            // Arrange
            var userHelperMock = new Mock<IUserHelper>();
            var user = new Core.ViewModel.UserDto { Id = new Guid().ToString() };

            var request = new SaveFinancialFormulaRequest
            {
                Id = 2,
                title = "Title",
                formula = "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
                cutoff = "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
                cutoff2 = "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
                prevYearPercents = "[{\"value\": 50}, {\"value\": 50}]",
                customize = false
            };
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet(new List<Core.Entities.FinancialFormula>() { new()
            {
              Id =2,
              Approved = false
            } });
            userHelperMock.Setup(u => u.GetUserFromToken())
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

            var handler = new SaveFinancialFormulaRequestHandler(collection.UnitOfWork.Object, userHelperMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async Task Handle_ReturnsApiResultWithSuccessCustomize_WhenRequestIsValid()
        {
            // Arrange
            var userHelperMock = new Mock<IUserHelper>();
            var user = new Core.ViewModel.UserDto { Id = new Guid().ToString() };

            var request = new SaveFinancialFormulaRequest
            {
                Id = 2,
                title = "Title",
                formula = "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
                cutoff = "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
                cutoff2 = "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
                prevYearPercents = "[{\"value\": 50}, {\"value\": 50}]",
                customize = true,
                riskCustomerGroupId=2
            };
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet(new List<Core.Entities.FinancialFormula>() { new()
            {
              Id =2,
              Approved = false
            } });
            userHelperMock.Setup(u => u.GetUserFromToken())
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
            collection.Context.Setup(x => x.RiskCustomerGroups).ReturnsDbSet(new List<Core.Entities.RiskCustomerGroup>()
            {
                new()
                {
                    Id = 2,
                    Title = "Title",
                    Version = 1,
                }
            });
            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

            var handler = new SaveFinancialFormulaRequestHandler(collection.UnitOfWork.Object, userHelperMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async Task Handle_ReturnsApiResultWithFailed_WhenRequestIsNotValid()
        {
            // Arrange
            var userHelperMock = new Mock<IUserHelper>();
            var request = new SaveFinancialFormulaRequest
            {
                Id = 1,
                title = "Title",
                formula = "formula",
                cutoff = "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
                cutoff2 = "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
                prevYearPercents = "[{\"value\": 50}, {\"value\": 50}]",
                customize = true,
                riskCustomerGroupId = 1
            };
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet(new List<Core.Entities.FinancialFormula>() { new()
            {
              Id =1,
              Approved = true
            } });

            var handler = new SaveFinancialFormulaRequestHandler(collection.UnitOfWork.Object, userHelperMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task Handle_ReturnsApiResultWithFailed_WhenRequestIsNotValidcutoff()
        {
            // Arrange
            var userHelperMock = new Mock<IUserHelper>();
            var request = new SaveFinancialFormulaRequest
            {
                Id = 1,
                title = "Title",
                formula = "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
                cutoff = "[{\"start\": 1, \"end\": 2}, {\"start\": 4, \"end\": 5}]",
                cutoff2 = "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
                prevYearPercents = "[{\"value\": 50}, {\"value\": 50}]",
                customize = true,
                riskCustomerGroupId = 1
            };
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet(new List<Core.Entities.FinancialFormula>() { new()
            {
              Id =1,
              Approved = true
            } });

            var handler = new SaveFinancialFormulaRequestHandler(collection.UnitOfWork.Object, userHelperMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task Handle_ReturnsApiResultWithFailed_WhenRequestIsNotValidcutoff2()
        {
            // Arrange
            var userHelperMock = new Mock<IUserHelper>();
            var request = new SaveFinancialFormulaRequest
            {
                Id = 1,
                title = "Title",
                formula = "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
                cutoff = "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
                cutoff2 = "[{\"start\": 1, \"end\": 2}, {\"start\": 4, \"end\": 5}]",
                prevYearPercents = "[{\"value\": 50}, {\"value\": 50}]",
                customize = true,
                riskCustomerGroupId = 1
            };
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet(new List<Core.Entities.FinancialFormula>() { new()
            {
              Id =1,
              Approved = true
            } });

            var handler = new SaveFinancialFormulaRequestHandler(collection.UnitOfWork.Object, userHelperMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task Handle_ReturnsApiResultWithFailed_WhenRequestIsNotValidprevYearPercents()
        {
            // Arrange
            var userHelperMock = new Mock<IUserHelper>();
            var request = new SaveFinancialFormulaRequest
            {
                Id = 1,
                title = "Title",
                formula = "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
                cutoff = "[{\"start\": 1, \"end\": 2}, {\"start\": 2, \"end\": 3}]",
                cutoff2 = "[{\"start\": 1, \"end\": 2}, {\"start\": 4, \"end\": 5}]",
                prevYearPercents = "[{\"value\": 50}, {\"value\": 60}]",
                customize = true,
                riskCustomerGroupId = 1
            };
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet(new List<Core.Entities.FinancialFormula>() { new()
            {
              Id =1,
              Approved = true
            } });

            var handler = new SaveFinancialFormulaRequestHandler(collection.UnitOfWork.Object, userHelperMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.Code);
        }
    }
}
