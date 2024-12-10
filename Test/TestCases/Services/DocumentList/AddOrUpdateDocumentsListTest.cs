using Application.Services.DocumentListService;
using Core.Helpers;
using Core.Logger;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.EntityFrameworkCore;
using System.Text;

namespace Test.TestCases.Services.DocumentList;

public class AddOrUpdateDocumentsListTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<InitSetting> _initSetting = new();
    private readonly Mock<ILoggerManager> _loggerManager = new();
    private readonly Mock<IFormFile> _formFile = new();

    [Fact]
    public async Task AddOrUpdateDocumentsList_Fail1()
    {
        var command = new AddOrUpdateDocumentsListRequest { DocumentId = 0 };

        _unitOfWork.Setup(x => x.Context.DocumentDefs).ReturnsDbSet([]);
        _unitOfWork.Setup(x => x.Context.Documents).ReturnsDbSet([]);

        var handler = new AddOrUpdateDocumentsListRequestHandler(_unitOfWork.Object, _initSetting.Object, _loggerManager.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddOrUpdateDocumentsList_Fail2()
    {
        var command = new AddOrUpdateDocumentsListRequest();

        _unitOfWork.Setup(x => x.Context.DocumentDefs).ReturnsDbSet([new() { ComplienceVisible = false, Mandatory = true }]);

        var handler = new AddOrUpdateDocumentsListRequestHandler(_unitOfWork.Object, _initSetting.Object, _loggerManager.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddOrUpdateDocumentsList_Fail3()
    {
        var bytes = Encoding.UTF8.GetBytes("This is a test file");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "TestFile", "TestFile.txt");

        var command = new AddOrUpdateDocumentsListRequest { DocumentFiles = [file, file], DocumentId = 0 };

        _unitOfWork.Setup(x => x.Context.DocumentDefs).ReturnsDbSet([new() { ComplienceVisible = false }]);
        _unitOfWork.Setup(x => x.Context.Documents).ReturnsDbSet([new()]);
        _unitOfWork.Setup(x => x.Repository.Update(It.IsAny<Core.Entities.Document>));
        _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

        var initSetting = new InitSetting { UploadDirectory = "" };

        var handler = new AddOrUpdateDocumentsListRequestHandler(_unitOfWork.Object, initSetting, _loggerManager.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddOrUpdateDocumentsList_Fail4()
    {
        var command = new AddOrUpdateDocumentsListRequest { DocumentFiles = [] };

        _unitOfWork.Setup(x => x.Context.DocumentDefs).ReturnsDbSet([new() { ComplienceVisible = false }]);

        var handler = new AddOrUpdateDocumentsListRequestHandler(_unitOfWork.Object, _initSetting.Object, _loggerManager.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task AddOrUpdateDocumentsList_Success()
    {
        var command = new AddOrUpdateDocumentsListRequest { DocumentFiles = [_formFile.Object] };

        _unitOfWork.Setup(x => x.Context.DocumentDefs).ReturnsDbSet([new() { ComplienceVisible = false }]);
        _unitOfWork.Setup(x => x.Repository.AddAsync(It.IsAny<Core.Entities.Document>, It.IsAny<CancellationToken>()));
        _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new AddOrUpdateDocumentsListRequestHandler(_unitOfWork.Object, _initSetting.Object, _loggerManager.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
