using Core.Entities;
using Core.Enums;
using Core.ViewModel;
using Infrastructure;
using Infrastructure.Repositories.OrganizationRepository;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Repositories.OrganizationRepoTests
{
    public class OrganizationRepoTest
    {
        private readonly Mock<DBContext> context = new();

        [Fact]
        public async Task Success()
        {
            context.Setup(x => x.Organizations).ReturnsDbSet(
                new List<Organization>() {
                    new()
                    {
                        Id =1,
                        BranchOrganizationTypeId = 1,
                        ManagerId=1,
                        OrganizationType = EOrganizationType.corporate_banking,
                        ParentOrganizationId = 1,
                        Title = ""
                    }
                });

            context.Setup(x => x.BankStaffs).ReturnsDbSet(new List<BankStaff>()
            {
                new()
                {
                    Id=1,
                    FirstName = "reza",
                    LastName = "keramati"
                }
            });

            context.Setup(x => x.BranchOrganizationTypes).ReturnsDbSet(new List<BranchOrganizationType>()
            {
                new()
                {
                    Id = 1,
                    Title = ""
                }
            });

            var input = new OrganizationIM();

            var repo = new OrganizationRepo(context.Object);

            var res = await repo.Search(input);

            Assert.NotNull(res.Items);
        }
    }
}
