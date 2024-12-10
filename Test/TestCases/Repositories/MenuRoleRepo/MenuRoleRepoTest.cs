using Core.Entities;
using Core.ViewModel.Auth.MenuRole;
using Moq.EntityFrameworkCore;
using TanvirArjel.EFCore.GenericRepository;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Repositories;
public class MenuRoleRepoTests
{

    [Fact]
    public async Task Search_Returns_PaginatedList_With_Filtered_Results()
    {
        // Arrange
        var menuRoles = new List<AuthMenuRole>
        {
            new() { Id = 1, MenuId = 1, RoleId = 1 },
            new() { Id = 2, MenuId = 2, RoleId = 2 }
        }.AsQueryable();

        var menus = new List<AuthMenu>
        {
            new() { Id = 1, Name = "Dashboard" },
            new() { Id = 2, Name = "Reports" }
        }.AsQueryable();


        var moq = GetUnitOfWorkMoqCollection();
        moq.Context.Setup(x => x.MenuRoles).ReturnsDbSet(menuRoles);
        moq.Context.Setup(x => x.Menus).ReturnsDbSet(menus);

        var menuRoleRepo = new Infrastructure.Repositories.MenuRoleRepository.MenuRoleRepo(moq.Context.Object);

        // Act
        var result = await menuRoleRepo.Search(new PaginationSpecification<MenuRoleSearchVM>
        {
            PageIndex = 1,
            PageSize = 10,
            Conditions = [m => true]
        }, "Reports");

        // Assert
        Assert.NotEmpty(result.Items);
        Assert.Single(result.Items); // Expect only one item matching "Reports"
        Assert.Equal("Reports", result.Items.First().MenuTitle); // Ensure result is "Reports"
        Assert.Equal(1, result.PageIndex); // Verify pagination
        Assert.Equal(10, result.PageSize);
        Assert.Equal(1, result.TotalItems); // Only one matching item expected
    }

}





