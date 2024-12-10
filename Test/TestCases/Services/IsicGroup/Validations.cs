//using Application.Services.IsicGroupService;
//using Infrastructure;
//using Moq;

//namespace Test.TestCases.Services.IsicGroupTest
//{
//    public class ValidationsTest
//    {
//        private readonly Mock<IUnitOfWork> _unitOfWork = new();
//        [Fact]
//        public void AddIsicGroupValidator_Success()
//        {
//            var _validator = new AddIsicGroupValidator(_unitOfWork.Object);
//            var request = new AddIsicGroupRequest()
//            {
//                Code = "123",
//                Title = "Title",
//            };
//            _unitOfWork.Setup(x => x.IsicGroupRepo.ExistCodeAsync(It.IsAny<string>())).Returns(false);
//            _unitOfWork.Setup(x => x.IsicGroupRepo.ExistTitleAsync(It.IsAny<string>())).Returns(false);
//            _validator.Validate(request);
//        }
//        [Fact]
//        public void UpdtaIsicGroupValidator_Success()
//        {
//            var _validator = new UpdateIsicGroupValidator(_unitOfWork.Object);
//            var request = new UpdateIsicGroupRequest()
//            {
//                Code = "123",
//                Title = "Title",
//            };
//            _unitOfWork.Setup(x => x.IsicGroupRepo.ExistCodeAsync(It.IsAny<string>())).Returns(false);
//            _unitOfWork.Setup(x => x.IsicGroupRepo.ExistTitleAsync(It.IsAny<string>())).Returns(false);
//            _validator.Validate(request);
//        }
//    }
//}
