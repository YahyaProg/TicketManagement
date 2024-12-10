using Application.Services.CompQualQuestionService;
using Core.ViewModel;
using Infrastructure;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CompQualQuestionTest
{
    public class AddOrUpdateCompQualQuestionTest
    {
        private readonly Mock<DBContext> context = new();

        [Fact]
        public async Task AddOrUpdateCompQualQuestionRequest_Add_Success()
        {
            //Arrange

            AddOrUpdateCompQualQuestionRequest request = new()
            {
                CustomerSchemeId=1,
                AssetsPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_assetsPermission.NotAssigned,
                ActivityCompanySub=Core.Enums.ECOMP_QUAL_QUESTIONS_activityCompanySub.NotAssigned,
                PeacePermission=Core.Enums.ECOMP_QUAL_QUESTIONS_peacePermission.NotAssigned,
                LoanPermission=Core.Enums.ECOMP_QUAL_QUESTIONS_loanPermission.NotAssigned,
            };


            List<Core.Entities.CompQualQuestion> compQualQuestions = [
                new()
                {
                    Id = 1,
                    CustomerSchemeId = 2,
                    AssetsPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_assetsPermission.NotAssigned,
                    ActivityCompanySub = Core.Enums.ECOMP_QUAL_QUESTIONS_activityCompanySub.NotAssigned,
                    PeacePermission = Core.Enums.ECOMP_QUAL_QUESTIONS_peacePermission.NotAssigned,
                    LoanPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_loanPermission.NotAssigned,
                }
            ];

            var mock = compQualQuestions.BuildMock();
            _ = context.Setup(x => x.CompQualQuestions).ReturnsDbSet(mock);
            _ = context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

            var _systemUnderTest = new AddOrUpdateCompQualQuestionRequestHandler(context.Object);

            //Act
            var result = await _systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);

        }


        [Fact]
        public async Task AddOrUpdateCompQualQuestionRequest_Update_Success()
        {
            //Arrange
            AddOrUpdateCompQualQuestionRequest request = new()
            {
                CustomerSchemeId = 1,
                AssetsPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_assetsPermission.NotAssigned,
                ActivityCompanySub = Core.Enums.ECOMP_QUAL_QUESTIONS_activityCompanySub.NotAssigned,
                PeacePermission = Core.Enums.ECOMP_QUAL_QUESTIONS_peacePermission.NotAssigned,
                LoanPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_loanPermission.NotAssigned,
            };


            List<Core.Entities.CompQualQuestion> compQualQuestions = [
                new()
                {
                    Id = 1,
                    CustomerSchemeId =1,
                    AssetsPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_assetsPermission.NotAssigned,
                    ActivityCompanySub = Core.Enums.ECOMP_QUAL_QUESTIONS_activityCompanySub.NotAssigned,
                    PeacePermission = Core.Enums.ECOMP_QUAL_QUESTIONS_peacePermission.NotAssigned,
                    LoanPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_loanPermission.NotAssigned,
                }
            ];
            _ = context.Setup(x => x.CompQualQuestions).ReturnsDbSet(compQualQuestions);

            _ = context.Setup(x => x.Update(request));
            _ = context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
            var _systemUnderTest = new AddOrUpdateCompQualQuestionRequestHandler(context.Object);

            //Act
            var result = await _systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }


        [Fact]
        public async Task AddOrUpdateCompQualQuestionRequest_Add_Fail()
        {
            //Arrange
            AddOrUpdateCompQualQuestionRequest request = new()
            {
                CustomerSchemeId = 1,
                AssetsPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_assetsPermission.NotAssigned,
                ActivityCompanySub = Core.Enums.ECOMP_QUAL_QUESTIONS_activityCompanySub.NotAssigned,
                PeacePermission = Core.Enums.ECOMP_QUAL_QUESTIONS_peacePermission.NotAssigned,
                LoanPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_loanPermission.NotAssigned,
            };


            List<Core.Entities.CompQualQuestion> compQualQuestions = [
                new()
                {
                    Id = 1,
                    CustomerSchemeId = 2,
                    AssetsPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_assetsPermission.NotAssigned,
                    ActivityCompanySub = Core.Enums.ECOMP_QUAL_QUESTIONS_activityCompanySub.NotAssigned,
                    PeacePermission = Core.Enums.ECOMP_QUAL_QUESTIONS_peacePermission.NotAssigned,
                    LoanPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_loanPermission.NotAssigned,
                }
            ];

            _ = context.Setup(x => x.CompQualQuestions).ReturnsDbSet(compQualQuestions);

            _ = context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);
            var _systemUnderTest = new AddOrUpdateCompQualQuestionRequestHandler(context.Object);

            //Act
            var result = await _systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.Code);
        }


        [Fact]
        public async Task AddOrUpdateCompQualQuestionRequest_Update_Fail()
        {
            //Arrange
            AddOrUpdateCompQualQuestionRequest request = new()
            {
                CustomerSchemeId = 1,
                AssetsPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_assetsPermission.NotAssigned,
                ActivityCompanySub = Core.Enums.ECOMP_QUAL_QUESTIONS_activityCompanySub.NotAssigned,
                PeacePermission = Core.Enums.ECOMP_QUAL_QUESTIONS_peacePermission.NotAssigned,
                LoanPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_loanPermission.NotAssigned,
            };


            List<Core.Entities.CompQualQuestion> compQualQuestions = [
                new()
                {
                    Id = 1,
                    CustomerSchemeId = 1,
                    AssetsPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_assetsPermission.NotAssigned,
                    ActivityCompanySub = Core.Enums.ECOMP_QUAL_QUESTIONS_activityCompanySub.NotAssigned,
                    PeacePermission = Core.Enums.ECOMP_QUAL_QUESTIONS_peacePermission.NotAssigned,
                    LoanPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_loanPermission.NotAssigned,
                }
            ];

            _ = context.Setup(x => x.CompQualQuestions).ReturnsDbSet(compQualQuestions);

            _ = context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

            var _systemUnderTest = new AddOrUpdateCompQualQuestionRequestHandler(context.Object);

            //Act
            var result = await _systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.Code);
        }

    }
}
