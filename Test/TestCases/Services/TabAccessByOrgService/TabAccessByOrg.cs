using Application.Services.TabAccessByOrgService;
using Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.TabAccessByOrgService;
public class TabAccessByOrg
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();


    [Fact]
    public void AddTabAccessByOrgRequest_Success() =>
        Assert.NotNull(new AddTabAccessByOrgRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteTabAccessByOrgRequest_Success() =>
        Assert.NotNull(new DeleteTabAccessByOrgRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetByIdTabAccessByOrgRequest_Success() =>
        Assert.NotNull(new GetTabAccessByOrgRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateTabAccessByOrgRequest_Success() =>
        Assert.NotNull(new UpdateTabAccessByOrgRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchTabAccessByOrgRequest_Success() =>
        Assert.NotNull(new AdvanceSearchTabAccessByOrgRequestHandler(_unitOfWork.Object));
}
