using Application.Services.CustomerSchemeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.CustomerSchemeTest
{
    public class CustomerSchemeTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddCustomerRequest_Success() =>
            Assert.NotNull(new AddCustomerSchemeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void DeleteCustomerRequest_Success() =>
            Assert.NotNull(new DeleteCustomerSchemeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetByIdCustomerRequest_Success() =>
            Assert.NotNull(new GetCustomerSchemeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateCustomerRequest_Success() =>
            Assert.NotNull(new UpdateCustomerSchemeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchCustomerRequest_Success() =>
            Assert.NotNull(new SearchCustomerSchemeRequestHandler(_unitOfWork.Object));
    }
}
