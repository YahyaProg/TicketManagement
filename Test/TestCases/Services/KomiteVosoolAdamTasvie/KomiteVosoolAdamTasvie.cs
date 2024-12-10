using Application.Services.KomiteVosoolAdamTasvieService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.KomiteVosoolAdamTasvie;

public class KomiteVosoolAdamTasvieRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddKomiteVosoolAdamTasvieRequest_Success() =>
        Assert.NotNull(new AddKomiteVosoolAdamTasvieRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetKomiteVosoolAdamTasvieRequest_Success() =>
        Assert.NotNull(new GetKomiteVosoolAdamTasvieRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchKomiteVosoolAdamTasvieRequest_Success() =>
        Assert.NotNull(new SearchKomiteVosoolAdamTasvieRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownKomiteVosoolAdamTasvieRequest_Success() =>
        Assert.NotNull(new DropDownKomiteVosoolAdamTasvieRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateKomiteVosoolAdamTasvieRequest_Success() =>
        Assert.NotNull(new UpdateKomiteVosoolAdamTasvieRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteKomiteVosoolAdamTasvieRequest_Success() =>
        Assert.NotNull(new DeleteKomiteVosoolAdamTasvieRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownKomiteVosoolAdamTasvieRequest()
    {
        var request = new DropDownKomiteVosoolAdamTasvieRequest
        {
            KeyWord = "a",
            Deleted = true,
            DefaultValue = 0
        };

        Assert.NotNull(request);
    }
}
