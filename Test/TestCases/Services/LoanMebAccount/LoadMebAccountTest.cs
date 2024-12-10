using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Services.MebAccountService;
using Core.Entities;
using Core.Enums;
using Core.GenericResultModel;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Tests.Services.MebAccountServiceTest
{
    public class LoadMebAccountHandlerTests
    {
        private readonly Mock<DBContext> con;
        private readonly LoadMebAccountHandler _handler;

        public LoadMebAccountHandlerTests()
        {
            con = new Mock<DBContext>();

            // Arrange in-memory data
            var proposals = new List<Proposal>
            {
                new Proposal { Id = 1, CustomerId = 1, Customer = new Customer { Id = 1 } }
            }.AsQueryable();

            var customers = new List<Customer>
            {
                new Customer { Id = 1 }
            }.AsQueryable();

            var blackListAcctTypes = new List<BlackListAcctType>().AsQueryable();

            // Setup DbSet mocks using Moq.EntityFrameworkCore
            con.Setup(m => m.Proposals).ReturnsDbSet(proposals);
            con.Setup(m => m.Customers).ReturnsDbSet(customers);
            con.Setup(m => m.BlackListAcctTypes).ReturnsDbSet(blackListAcctTypes);

            _handler = new LoadMebAccountHandler(con.Object);
        }

        [Fact]
        public async Task Handle_ProposalNotFound_ReturnsNotFoundResult()
        {
            // Arrange: Use an empty list for Proposals to simulate a "not found" scenario
            con.Setup(m => m.Proposals).ReturnsDbSet(new List<Proposal>().AsQueryable());

            var request = new LoadMebAccountRequest
            {
                ProposalId = 1,
                Accperiod = EMebAccount_Accperiod.SeMahe
            };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(404, result.Code);
            Assert.Equal("پرونده یافت نشد!", result.Message);
        }

        [Theory]
        [InlineData(EMebAccount_Accperiod.YeSale)]
        [InlineData(EMebAccount_Accperiod.ShishMahe)]
        [InlineData(EMebAccount_Accperiod.SeMahe)]
        public async Task Handle_ProposalNotFound_Success(EMebAccount_Accperiod accperiod)
        {
            // Arrange: Use an empty list for Proposals to simulate a "not found" scenario
            con.Setup(m => m.Proposals).ReturnsDbSet([new() { Id = 1, CustomerId = 2 }]);
            con.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 2 }]);
            con.Setup(x => x.BlackListAcctTypes).ReturnsDbSet([new() { Id = 1, Type = EBlackListAcctType_type.acct, Module = "1", Scheme = "1" }]);
            con.Setup(x => x.AccountTurnovers).ReturnsDbSet([new() {
                OwnerId = 2 ,
                AccountNo = "1",
                AccountType = "1",
                CurrencyId = 1,
                FinalBalance = "1",
                InterestRate = "1",
                OpeningDate = DateTime.Now,
                AvgBalanceQuarter = "1",
                AvgBalanceHalf = "1",
                AvgBalanceYear = "1",
                CreditTurnoverHalf = "1",
                CreditTurnoverQuarter = "1",
                CreditTurnoverYear = "1",
            },new() {
                OwnerId = 3 ,
                AccountNo = "12",
                AccountType = "1",
                CurrencyId = 1,
                FinalBalance = "1",
                InterestRate = "1",
                OpeningDate = DateTime.Now,
                AvgBalanceQuarter = "1",
                AvgBalanceHalf = "1",
                AvgBalanceYear = "1",
                CreditTurnoverHalf = "1",
                CreditTurnoverQuarter = "1",
                CreditTurnoverYear = "1",
            },new(){
                AccountNo = "1/1",
            }]);

            con.Setup(x => x.MebAccounts).ReturnsDbSet([new() {
                ProposalId = 1,
                AccountNo = "1",
                Deleted = true,
            },new() {
                ProposalId = 1,
                AccountNo = "1/1",
                Deleted = true,
            }]);

            var request = new LoadMebAccountRequest
            {
                ProposalId = 1,
                Accperiod = accperiod
            };

            con.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() { Id = 2 }]);
            con.Setup(x => x.CustomerSchemes).ReturnsDbSet([new() { Id = 1, ProposalId = 1 }]);
            con.Setup(x => x.Managers).ReturnsDbSet([new() { PersonId = 3, CustomerSchemeId = 1, Deleted = false }]);
            con.Setup(x => x.ProposalManagerAccounts).ReturnsDbSet([new() {
                ProposalId = 1,
                AccountId = 1
            }]);

            con.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

    }
}