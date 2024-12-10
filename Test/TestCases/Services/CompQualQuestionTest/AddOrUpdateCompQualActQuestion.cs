using Application.Services.CompQualQuestionService;
using Core.Entities;
using Core.ViewModel;
using Infrastructure;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CompQualQuestionTest
{
    public class AddOrUpdateCompQualActQuestion
    {
        private readonly Mock<DBContext> context = new();

        [Fact]
        public async Task AddOrUpdateCompQualActQuestionRequest_Add_Success()
        {


            var request = new AddOrUpdateCompQualActQuestionRequest()
            {
                ActivityCompanySub = Core.Enums.ECOMP_QUAL_QUESTIONS_activityCompanySub.True,
                AssetsPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_assetsPermission.True,
                LoanPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_loanPermission.True,
                PeacePermission = Core.Enums.ECOMP_QUAL_QUESTIONS_peacePermission.True,
                ThirdPersonPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_thirdPersonPermission.True,
                CustomerSchemeId = 10,
                WealthDate = DateTime.Now,
                Description = "Description",
                SignatureCondDate = DateTime.Now.AddDays(-1),
                SignatureCondNo = "SignatureCondNo",
                WealthNo = "WealthNo"

            };

            List<CompQualQuestion> compQualQuestions =
                [
                new ()
                {
                    Id=10,
                    CustomerSchemeId=10,
                    Version =1,
                    AssetsPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_assetsPermission.NotAssigned,
                    ActivityCompanySub = Core.Enums.ECOMP_QUAL_QUESTIONS_activityCompanySub.NotAssigned,
                    PeacePermission = Core.Enums.ECOMP_QUAL_QUESTIONS_peacePermission.NotAssigned,
                    LoanPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_loanPermission.NotAssigned,

                },
                new ()
                {
                    Id=11,
                    CustomerSchemeId=10,
                    Version =1,
                    AssetsPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_assetsPermission.NotAssigned,
                    ActivityCompanySub = Core.Enums.ECOMP_QUAL_QUESTIONS_activityCompanySub.NotAssigned,
                    PeacePermission = Core.Enums.ECOMP_QUAL_QUESTIONS_peacePermission.NotAssigned,
                    LoanPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_loanPermission.NotAssigned,

                },
                new ()
                {
                    Id=12,
                    CustomerSchemeId=10,
                    Version =1,
                    AssetsPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_assetsPermission.NotAssigned,
                    ActivityCompanySub = Core.Enums.ECOMP_QUAL_QUESTIONS_activityCompanySub.NotAssigned,
                    PeacePermission = Core.Enums.ECOMP_QUAL_QUESTIONS_peacePermission.NotAssigned,
                    LoanPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_loanPermission.NotAssigned,

                }];

            _ = context.Setup(x => x.CompQualQuestions).ReturnsDbSet(compQualQuestions);
            context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(3);

            List<CompActvQuestion> compActvQuestions =
                [
                    new()
                    {
                        Id = 1,
                        CustomerSchemeId =10,
                        Version =1,
                        Description ="Description",
                        WealthDate = DateTime.Now,
                        SignatureCondDate = DateTime.Now.AddDays(-1),
                        SignatureCondNo = "SignatureCondNo",
                        WealthNo = "WealthNo"
                    }
                ];

            _ = context.Setup(x => x.CompActvQuestions).ReturnsDbSet(compActvQuestions);
                
            var handler = new AddOrUpdateCompQualActQuestionRequestHandler(context.Object);
            var result = await handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);

        }
        
        [Fact]
        public async Task AddOrUpdateCompQualActQuestionRequest_Add_SuccessAddCompQualActQuestion()
        {


            var request = new AddOrUpdateCompQualActQuestionRequest()
            {
                ActivityCompanySub = Core.Enums.ECOMP_QUAL_QUESTIONS_activityCompanySub.True,
                AssetsPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_assetsPermission.True,
                LoanPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_loanPermission.True,
                PeacePermission = Core.Enums.ECOMP_QUAL_QUESTIONS_peacePermission.True,
                ThirdPersonPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_thirdPersonPermission.True,
                CustomerSchemeId = 10,
                WealthDate = DateTime.Now,
                Description = "Description",
                SignatureCondDate = DateTime.Now.AddDays(-1),
                SignatureCondNo = "SignatureCondNo",
                WealthNo = "WealthNo"

            };

            _ = context.Setup(x => x.CompQualQuestions).ReturnsDbSet([]);
            context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            List<CompActvQuestion> compActvQuestions =
                [
                    new()
                    {
                        Id = 1,
                        CustomerSchemeId =10,
                        Version =1,
                        Description ="Description",
                        WealthDate = DateTime.Now,
                        SignatureCondDate = DateTime.Now.AddDays(-1),
                        SignatureCondNo = "SignatureCondNo",
                        WealthNo = "WealthNo"
                    }
                ];
            var mockActive = compActvQuestions.BuildMock();
            _ = context.Setup(x => x.CompActvQuestions).ReturnsDbSet(compActvQuestions);

            var handler = new AddOrUpdateCompQualActQuestionRequestHandler(context.Object);
            var result = await handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);

        } 
        
        [Fact]
        public async Task AddOrUpdateCompQualActQuestionRequest_Add_SuccessAddCompQualActQuestionAddResultZero()
        {


            var request = new AddOrUpdateCompQualActQuestionRequest()
            {
                ActivityCompanySub = Core.Enums.ECOMP_QUAL_QUESTIONS_activityCompanySub.True,
                AssetsPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_assetsPermission.True,
                LoanPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_loanPermission.True,
                PeacePermission = Core.Enums.ECOMP_QUAL_QUESTIONS_peacePermission.True,
                ThirdPersonPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_thirdPersonPermission.True,
                CustomerSchemeId = 10,
                WealthDate = DateTime.Now,
                Description = "Description",
                SignatureCondDate = DateTime.Now.AddDays(-1),
                SignatureCondNo = "SignatureCondNo",
                WealthNo = "WealthNo"

            };

            _ = context.Setup(x => x.CompQualQuestions).ReturnsDbSet([]);
            context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

            List<CompActvQuestion> compActvQuestions =
                [
                    new()
                    {
                        Id = 1,
                        CustomerSchemeId =10,
                        Version =1,
                        Description ="Description",
                        WealthDate = DateTime.Now,
                        SignatureCondDate = DateTime.Now.AddDays(-1),
                        SignatureCondNo = "SignatureCondNo",
                        WealthNo = "WealthNo"
                    }
                ];
            var mockActive = compActvQuestions.BuildMock();
            _ = context.Setup(x => x.CompActvQuestions).ReturnsDbSet(mockActive);

            var handler = new AddOrUpdateCompQualActQuestionRequestHandler(context.Object);
            var result = await handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);

        }
        
        [Fact]
        public async Task AddOrUpdateCompQualActQuestionRequest_Add_SuccessAddCompQualActiveQuestion()
        {


            var request = new AddOrUpdateCompQualActQuestionRequest()
            {
                ActivityCompanySub = Core.Enums.ECOMP_QUAL_QUESTIONS_activityCompanySub.True,
                AssetsPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_assetsPermission.True,
                LoanPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_loanPermission.True,
                PeacePermission = Core.Enums.ECOMP_QUAL_QUESTIONS_peacePermission.True,
                ThirdPersonPermission = Core.Enums.ECOMP_QUAL_QUESTIONS_thirdPersonPermission.True,
                CustomerSchemeId = 10,
                WealthDate = DateTime.Now,
                Description = "Description",
                SignatureCondDate = DateTime.Now.AddDays(-1),
                SignatureCondNo = "SignatureCondNo",
                WealthNo = "WealthNo"

            };

            _ = context.Setup(x => x.CompQualQuestions).ReturnsDbSet([]);
            context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            List<CompActvQuestion> compActvQuestions =  [ ];
            var mockActive = compActvQuestions.BuildMock();
            _ = context.Setup(x => x.CompActvQuestions).ReturnsDbSet([]);



            var handler = new AddOrUpdateCompQualActQuestionRequestHandler(context.Object);
            var result = await handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);

        }
    }
}
