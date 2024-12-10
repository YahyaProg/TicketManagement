
using Application.Services.CustomerRequestDetailService;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CustomerRequestDetailTest;


public class CustomerRequestDetailRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    private readonly Mock<DBContext> _dbContext = new();

    public CustomerRequestDetailRequestTest()
    {
        _dbContext.Setup(x => x.CustomerRequestDetails).ReturnsDbSet([]);
    }
    [Fact]
    public void AddCustomerRequestDetailRequest_Success() =>
        Assert.NotNull(new AddCustomerRequestDetailRequestHandler(_dbContext.Object));

    [Fact]
    public void SoftDeleteCustomerRequestDetailRequest_Success() =>
        Assert.NotNull(new DeleteCustomerRequestDetailRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetCustomerRequestDetailRequest_Success() =>
        Assert.NotNull(new GetCustomerRequestDetailRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateCustomerRequestDetailRequest_Success() =>
        Assert.NotNull(new UpdateCustomerRequestDetailRequestHandler(_dbContext.Object));

    [Fact]
    public void SearchCustomerRequestDetailRequest_Success() =>
        Assert.NotNull(new SearchCustomerRequestDetailRequestHandler(_unitOfWork.Object));
}