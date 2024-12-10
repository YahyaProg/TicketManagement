
using Application.Services.InboxCritemDuedateService;
using Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.InboxCritemDuedat;
public class InboxCritemDuedate
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();


    [Fact]
    public void AddInboxCritemDuedatRequest_Success() =>
        Assert.NotNull(new AddInboxCritemDuedateRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteInboxCritemDuedatRequest_Success() =>
        Assert.NotNull(new DeleteInboxCritemDuedateRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetByIdInboxCritemDuedatRequest_Success() =>
        Assert.NotNull(new GetInboxCritemDuedateRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateInboxCritemDuedatRequest_Success() =>
        Assert.NotNull(new UpdateInboxCritemDuedateRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchInboxCritemDuedatRequest_Success() =>
        Assert.NotNull(new SearchInboxCritemDuedateRequestHandler(_unitOfWork.Object));
}
