using Application.Services.PrimaryRiskService;
using Core.GenericResultModel;
using Core.Helpers;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.PrimaryRisk
{
    public class UpdatePrimaryRiskTests
    {
        private readonly UpdatePrimaryRiskRequest request = new()
        {
            Category = Core.Enums.EPrimaryRisk_category.C,
            RiskCustomerGroupId = 1,
            Items = []
        };

        [Fact]
        public async Task ListIsNull()
        {
            var context = new Mock<DBContext>();
            var helper = new Mock<IUserHelper>();

            request.Items = [];

            var res = await getRes(context.Object, helper.Object);

            Assert.False(res.IsSuccess);
        }

        [Fact]
        public async Task ListIsNotValid()
        {
            var context = new Mock<DBContext>();
            var helper = new Mock<IUserHelper>();

            request.Items = [new() {
                Id = 1,
                Childs = [new(){}]
            }];

            var res = await getRes(context.Object, helper.Object);

            Assert.False(res.IsSuccess);
        }

        [Fact]
        public async Task Success()
        {
            var context = new Mock<DBContext>();
            var helper = new Mock<IUserHelper>();

            request.Items = [new() {
                Id = 1,
                Weight = 100,
                Childs = [new(){
                    RiskInfoGroupId = 1,
                    Weight = 100,
                    Childs = [new(){Weight = 100}]
                }]
            }];

            context.Setup(x => x.PrimaryRisks).ReturnsDbSet([
                new(){
                    Category = request.Category,
                    RiskCustomerGroupId = request.RiskCustomerGroupId
                }
            ]);

            helper.Setup(x => x.GetUserFromToken()).Returns(new Core.ViewModel.UserDto
            {
                Id = "1"
            });

            context.Setup(x => x.BankStaffs).ReturnsDbSet([
                new(){
                    Id = 1,
                    UserId = "1"
                }
            ]);

            context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

            var res = await getRes(context.Object, helper.Object);

            Assert.True(res.IsSuccess);
        }


        private async Task<ApiResult> getRes(DBContext context, IUserHelper helper)
        {
            var handler = new UpdatePrimaryRiskRequestHandler(context, helper);

            return await handler.Handle(request, CancellationToken.None);
        }
    }
}
