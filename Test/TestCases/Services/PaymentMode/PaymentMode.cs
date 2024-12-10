using Application.Services.PaymentModeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.PaymentMode
{
    public class PaymentModeRequestTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddPaymentModeRequest_Success() =>
            Assert.NotNull(new AddPaymentModeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeletePaymentModeRequest_Success() =>
            Assert.NotNull(new DeletePaymentModeRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownPaymentModeRequest_Success() =>
   Assert.NotNull(new DropDownPaymentModeRequestHandler(_unitOfWork.Object));
        [Fact]
        public void GetPaymentModeRequest_Success() =>
            Assert.NotNull(new GetPaymentModeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdatePaymentModeRequest_Success() =>
            Assert.NotNull(new UpdatePaymentModeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchPaymentModeRequest_Success() =>
            Assert.NotNull(new SearchPaymentModeRequestHandler(_unitOfWork.Object));
    }
}
