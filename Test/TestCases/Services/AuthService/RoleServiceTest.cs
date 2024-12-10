using Application.Services.Auth.RoleServService;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.AuthServiceTest
{
    public class RoleServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void GetRoleServiceRequest_Success() =>
         Assert.NotNull(new GetRoleServiceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchRoleServiceRequest_Success() =>
            Assert.NotNull(new SearchRoleServiceRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteRoleServiceRequest_Success() =>
            Assert.NotNull(new DeleteRoleServiceRequestHandler(_unitOfWork.Object));

        [Fact]
        public async Task SearchRoleServiceRequestHandler_Test()
        {
            // Arrange
            var collecion = MoqHelper.GetUnitOfWorkMoqCollection();

            collecion.Context.Setup(x => x.Services).ReturnsDbSet([
                new Core.Entities.Service(){Id = 1,},
            ]);

            collecion.Context.Setup(x => x.RoleServices).ReturnsDbSet([
                new Core.Entities.AuthRoleService(){Id = 1, RoleId = 1, ServiceId = 1},
            ]);
            var handler = new SearchRoleServiceRequestHandler(collecion.UnitOfWork.Object);


            // Act
            var result = await handler.Handle(new() { RoleId = 1, ServiceId = 1 }, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task AddRoleServiceRequestHandler_Test(int type)
        {
            // Arrange
            var collecion = MoqHelper.GetUnitOfWorkMoqCollection();


            collecion.Context.Setup(x => x.Services).ReturnsDbSet([
                 new Core.Entities.Service(){Id = type != 1 ? 1 : 0},
            ]);


            collecion.Context.Setup(x => x.Roles).ReturnsDbSet([
                 new Core.Entities.AuthRole(){Id = type != 2 ? 1 : 0},
            ]);


            collecion.Context.Setup(x => x.RoleServices).ReturnsDbSet([
                new Core.Entities.AuthRoleService(){Id = 1, RoleId = type == 3 ? 1 : 0, ServiceId = 1},
            ]);


            collecion.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(type != 4 ? 1 : 0);



            var handler = new AddRoleServiceRequestHandler(collecion.UnitOfWork.Object);


            // Act
            var result = await handler.Handle(new() { RoleId = 1, ServiceId = 1 }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task UpdateRoleServiceRequestHandler_Test(int type)
        {
            // Arrange
            var collecion = MoqHelper.GetUnitOfWorkMoqCollection();


            collecion.Context.Setup(x => x.Services).ReturnsDbSet([
                 new Core.Entities.Service(){Id = type != 1 ? 1 : 0},
            ]);


            collecion.Context.Setup(x => x.Roles).ReturnsDbSet([
                 new Core.Entities.AuthRole(){Id = type != 2 ? 1 : 0},
            ]);


            collecion.Context.Setup(x => x.RoleServices).ReturnsDbSet([
                new Core.Entities.AuthRoleService(){Id = type != 3 ? 1 : 0, RoleId = 1, ServiceId = 1},
            ]);


            collecion.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(type != 4 ? 1 : 0);



            var handler = new UpdateRoleServiceRequestHandler(collecion.UnitOfWork.Object);


            // Act
            var result = await handler.Handle(new() { RoleId = 1, ServiceId = 1, Id= 1 }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

    }
}
