using Application.Services.RolePermissionService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.RolePermission;

public class RolePermissionRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddRolePermissionRequest_Success() =>
        Assert.NotNull(new AddRolePermissionRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetRolePermissionRequest_Success() =>
        Assert.NotNull(new GetRolePermissionRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchRolePermissionRequest_Success() =>
        Assert.NotNull(new SearchRolePermissionRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateRolePermissionRequest_Success() =>
        Assert.NotNull(new UpdateRolePermissionRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteRolePermissionRequest_Success() =>
        Assert.NotNull(new DeleteRolePermissionRequestHandler(_unitOfWork.Object));
}
