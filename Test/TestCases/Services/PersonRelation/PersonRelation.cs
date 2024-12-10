using Application.Services.PersonRelationService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.PersonRelationTest
{
    public class PersonRelationRequestTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddPersonRelationRequest_Success() =>
            Assert.NotNull(new AddPersonRelationRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeletePersonRelationRequest_Success() =>
            Assert.NotNull(new DeletePersonRelationRequestHandler(_unitOfWork.Object));
        [Fact]
        public void DropDownPersonRelationRequest_Success() =>
            Assert.NotNull(new DropDownPersonRelationRequestHandler(_unitOfWork.Object));
        [Fact]
        public void GetPersonRelationRequest_Success() =>
            Assert.NotNull(new GetPersonRelationRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdatePersonRelationRequest_Success() =>
            Assert.NotNull(new UpdatePersonRelationRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchPersonRelationRequest_Success() =>
            Assert.NotNull(new SearchPersonRelationRequestHandler(_unitOfWork.Object));
    }
}
