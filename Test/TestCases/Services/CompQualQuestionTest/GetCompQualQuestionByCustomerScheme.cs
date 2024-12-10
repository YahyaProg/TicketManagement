using Application.Services.CustomerSchemeService;
using Core.Entities;
using Infrastructure;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CompQualQuestionTest
{
    public class GetCompQualQuestionByCustomerScheme
    {
        private readonly Mock<DBContext> context = new();

        [Fact]
        public async Task GetCompQualQuestionByCustomerSchemeRequest_Add_Success()
        {
            var request = new GetCompQualQuestionByCustomerSchemeRequest()
            {
                CustomerSchemeId = 10,
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

            var mock = compQualQuestions.BuildMock();
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

            var handler = new GetCompQualQuestionByCustomerSchemeRequestHandler(context.Object);
            var result = await handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);
        }
    }
}
