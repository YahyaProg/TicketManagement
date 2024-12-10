using Application.Services.ViewAccessInfoService;
using Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.ViewAccessInfo;
public class ViewAccessInfo
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddViewAccessInfoRequest_Success() =>
        Assert.NotNull(new AddViewAccessInfoRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SoftDeleteViewAccessInfoRequest_Success() =>
        Assert.NotNull(new DeleteViewAccessInfoRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetViewAccessInfoRequest_Success() =>
        Assert.NotNull(new GetViewAccessInfoRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateViewAccessInfoRequest_Success() =>
        Assert.NotNull(new UpdateViewAccessInfoRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchViewAccessInfoRequest_Success() =>
        Assert.NotNull(new SearchViewAccessInfoRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownViewAccessInfoRequest_Success() =>
        Assert.NotNull(new DropDownViewAccessInfoRequestHandler(_unitOfWork.Object));
}
