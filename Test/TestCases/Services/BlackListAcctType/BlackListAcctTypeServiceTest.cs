using Application.Services.BlackListAcctTypeService;
using Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.BlackListAcctType;
public class BlackListAcctTypeServiceTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddBlackListAcctTypeRequest_Success() =>
        Assert.NotNull(new AddBlackListAcctTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SoftDeleteBlackListAcctTypeRequest_Success() =>
        Assert.NotNull(new DeleteBlackListAcctTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetBlackListAcctTypeRequest_Success() =>
        Assert.NotNull(new GetBlackListAcctTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateBlackListAcctTypeRequest_Success() =>
        Assert.NotNull(new UpdateBlackListAcctTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchBlackListAcctTypeRequest_Success() =>
        Assert.NotNull(new SearchBlackListAcctTypeRequestHandler(_unitOfWork.Object));

}
