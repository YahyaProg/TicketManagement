using Application.Services.CustomerSchemeService;
using Core.ViewModel;
using Infrastructure;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CustomerSchemeTest
{
    public class AddOrUpdateCustomerSchemeRequestTest
    {
        private readonly Mock<DBContext> context = new();

        [Fact]
        public async Task AddOrUpdateCustomerSchemeRequest_Add_Success()
        {
            //Arrange

            AddOrUpdateCustomerSchemeRequest request = new()
            {
                ActivityDescriptions = "1درخواست وام",
                CompanySubject = "تولیدی",
                Moaref = " معرف 1",
                ProposalId = 1,
                MajorChangesAndHistory = "MajorChangesAndHistory"
            };


            List<Core.Entities.CustomerScheme> customerSchemes = [
                new()
                {
                    Id = 1,
                    ActivityDescriptions = "2درخواست وام",
                    CompanySubject = "تولیدی",
                    Moaref = " معرف 2",
                    ProposalId = 2
                }
            ];

            _ = context.Setup(x => x.CustomerSchemes).ReturnsDbSet(customerSchemes);

            List<Core.Entities.ProposalScheme> proposalSchemes = [
                    new()
                    {
                        Id = 1,
                        CustomerId = 1,
                        ProposalId = 1
                    }
                ];

            _ = context.Setup(x => x.ProposalSchemes).ReturnsDbSet(proposalSchemes);


            _ = context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var _systemUnderTest = new AddOrUpdateCustomerSchemeRequestHandler(context.Object);

            //Act
            var result = await _systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);

        }
        [Fact]
        public async Task AddOrUpdateCustomerSchemeRequest_Add_Success_searchResultIsNull()
        {
            //Arrange

            AddOrUpdateCustomerSchemeRequest request = new()
            {
                ActivityDescriptions = "1درخواست وام",
                CompanySubject = "تولیدی",
                Moaref = " معرف 1",
                ProposalId = 1,
                MajorChangesAndHistory = "MajorChangesAndHistory",
                ProposalSchemeId = 1
            };

            _ = context.Setup(x => x.CustomerSchemes).ReturnsDbSet([]);

            List<Core.Entities.ProposalScheme> proposalSchemes = [
                    new()
                    {
                        Id = 1,
                        CustomerId = 1,
                        ProposalId = 1
                    }
                ];
            _ = context.Setup(x => x.ProposalSchemes).ReturnsDbSet(proposalSchemes);


            context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

            var _systemUnderTest = new AddOrUpdateCustomerSchemeRequestHandler(context.Object);

            //Act
            var result = await _systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async Task AddOrUpdateCustomerSchemeRequest_Add_Success_searchProposalSchemeResultCount()
        {
            //Arrange

            AddOrUpdateCustomerSchemeRequest request = new()
            {
                ActivityDescriptions = "1درخواست وام",
                CompanySubject = "تولیدی",
                Moaref = " معرف 1",
                ProposalId = 1,
                MajorChangesAndHistory = "MajorChangesAndHistory",
                ProposalSchemeId = 1
            };


            _ = context.Setup(x => x.CustomerSchemes).ReturnsDbSet([]);

            _ = context.Setup(x => x.ProposalSchemes).ReturnsDbSet([]);


            context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

            var _systemUnderTest = new AddOrUpdateCustomerSchemeRequestHandler(context.Object);

            //Act
            var result = await _systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddOrUpdateCustomerSchemeRequest_Update_Success()
        {
            //Arrange

            AddOrUpdateCustomerSchemeRequest request = new()
            {
                ActivityDescriptions = "1درخواست وام",
                CompanySubject = "تولیدی",
                Moaref = " معرف 1",
                ProposalId = 1
            };


            List<Core.Entities.CustomerScheme> customerSchemes = [
                new()
                {
                    Id = 1,
                    ActivityDescriptions = "2درخواست وام",
                    CompanySubject = "تولیدی",
                    Moaref = " معرف 2",
                    ProposalId = 1
                }
            ];

            _ = context.Setup(x => x.CustomerSchemes).ReturnsDbSet(customerSchemes);


            _ = context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

            var _systemUnderTest = new AddOrUpdateCustomerSchemeRequestHandler(context.Object);

            //Act
            var result = await _systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }


        [Fact]
        public async Task AddOrUpdateCustomerSchemeRequest_Add_Fail()
        {
            //Arrange

            AddOrUpdateCustomerSchemeRequest request = new()
            {
                ActivityDescriptions = "1درخواست وام",
                CompanySubject = "تولیدی",
                Moaref = " معرف 1",
                ProposalId = 1
            };


            List<Core.Entities.CustomerScheme> customerSchemes = [
                new()
                {
                    Id = 1,
                    ActivityDescriptions = "2درخواست وام",
                    CompanySubject = "تولیدی",
                    Moaref = " معرف 2",
                    ProposalId = 2
                }
            ];

            _ = context.Setup(x => x.CustomerSchemes).ReturnsDbSet(customerSchemes);

            List<Core.Entities.ProposalScheme> proposalSchemes = [
                    new()
                    {
                        Id = 1,
                        CustomerId = 1,
                        ProposalId = 1
                    }
                ];

            _ = context.Setup(x => x.ProposalSchemes).ReturnsDbSet(proposalSchemes);


            _ = context.Setup(x =>  x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

            var _systemUnderTest = new AddOrUpdateCustomerSchemeRequestHandler(context.Object);

            //Act
            var result = await _systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.Code);

        }


        [Fact]
        public async Task AddOrUpdateCustomerSchemeRequest_Update_Fail()
        {
            //Arrange

            AddOrUpdateCustomerSchemeRequest request = new()
            {
                ActivityDescriptions = "1درخواست وام",
                CompanySubject = "تولیدی",
                Moaref = " معرف 1",
                ProposalId = 1
            };


            List<Core.Entities.CustomerScheme> customerSchemes = [
                new()
                {
                    Id = 1,
                    ActivityDescriptions = "2درخواست وام",
                    CompanySubject = "تولیدی",
                    Moaref = " معرف 2",
                    ProposalId = 1
                }
            ];
            var mockCustomerSchemesQ = customerSchemes.BuildMock();
            _ = context.Setup(x => x.CustomerSchemes).ReturnsDbSet(customerSchemes);


            _ = context.Setup(x =>  x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

            var _systemUnderTest = new AddOrUpdateCustomerSchemeRequestHandler(context.Object);

            //Act
            var result = await _systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task AddOrUpdateCustomerSchemeRequest_Update_Fail2()
        {
            //Arrange

            AddOrUpdateCustomerSchemeRequest request = new()
            {
                ActivityDescriptions = "1درخواست وام",
                CompanySubject = "تولیدی",
                Moaref = " معرف 1",
                ProposalId = 1
            };


            List<Core.Entities.CustomerScheme> customerSchemes = [
                new()
                {
                    Id = 1,
                    ActivityDescriptions = "2درخواست وام",
                    CompanySubject = "تولیدی",
                    Moaref = " معرف 2",
                    ProposalId = 1
                }
            ];
            var mockCustomerSchemesQ = customerSchemes.BuildMock();
            _ = context.Setup(x => x.CustomerSchemes).ReturnsDbSet(customerSchemes);


            _ = context.Setup(x =>  x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(-1);

            var _systemUnderTest = new AddOrUpdateCustomerSchemeRequestHandler(context.Object);

            //Act
            var result = await _systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.Code);
        }
        [Fact]
        public async Task AddOrUpdateCustomerSchemeRequestWithMajorChangeAndStructuralDiagram_Add_Success()
        {
            //Arrange

            AddOrUpdateCustomerSchemeRequest request = new()
            {
                ActivityDescriptions = "1درخواست وام",
                CompanySubject = "تولیدی",
                Moaref = " معرف 1",
                ProposalId = 1,
                MajorChangesAndHistory = "تستی",
                StructuralDiagram = "تست2"
            };


            List<Core.Entities.CustomerScheme> customerSchemes = [
                new()
                {
                    Id = 1,
                    ActivityDescriptions = "2درخواست وام",
                    CompanySubject = "تولیدی",
                    Moaref = " معرف 2",
                    ProposalId = 2
                }
            ];
            var mockCustomerSchemesQ = customerSchemes.BuildMock();
            _ = context.Setup(x => x.CustomerSchemes).ReturnsDbSet(customerSchemes);

            List<Core.Entities.ProposalScheme> proposalSchemes = [
                    new()
                    {
                        Id = 1,
                        CustomerId = 1,
                        ProposalId = 1
                    }
                ];
            var mockProposalSchemesQ = proposalSchemes.BuildMock();
            _ = context.Setup(x => x.ProposalSchemes).ReturnsDbSet(proposalSchemes);


            _ = context.Setup(x =>  x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

            var _systemUnderTest = new AddOrUpdateCustomerSchemeRequestHandler(context.Object);

            //Act
            var result = await _systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);

        }


        [Fact]
        public async Task AddOrUpdateCustomerSchemeRequestWithZeroSearchCount_Add_Failed()
        {
            //Arrange

            AddOrUpdateCustomerSchemeRequest request = new()
            {
                ActivityDescriptions = "1درخواست وام",
                CompanySubject = "تولیدی",
                Moaref = " معرف 1",
                ProposalId = 1
            };


            List<Core.Entities.CustomerScheme> customerSchemes = [
            ];
            var mockCustomerSchemesQ = customerSchemes.BuildMock();
            _ = context.Setup(x => x.CustomerSchemes).ReturnsDbSet(customerSchemes);

            List<Core.Entities.ProposalScheme> proposalSchemes = [
                    new()
                    {
                        Id = 1,
                        CustomerId = 1,
                        ProposalId = 1
                    }
                ];
            var mockProposalSchemesQ = proposalSchemes.BuildMock();
            _ = context.Setup(x => x.ProposalSchemes).ReturnsDbSet(proposalSchemes);


            _ = context.Setup(x =>  x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

            var _systemUnderTest = new AddOrUpdateCustomerSchemeRequestHandler(context.Object);

            //Act
            var result = await _systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(404, result.Code);

        }

        [Fact]
        public async Task AddOrUpdateCustomerSchemeRequestWithZeroSearchCount_Add_Success()
        {
            //Arrange

            AddOrUpdateCustomerSchemeRequest request = new()
            {
                ActivityDescriptions = "1درخواست وام",
                CompanySubject = "تولیدی",
                Moaref = " معرف 1",
                ProposalId = 1,
                ProposalSchemeId = 1,
            };


            List<Core.Entities.CustomerScheme> customerSchemes = [
            ];
            var mockCustomerSchemesQ = customerSchemes.BuildMock();
            _ = context.Setup(x => x.CustomerSchemes).ReturnsDbSet(customerSchemes);

            List<Core.Entities.ProposalScheme> proposalSchemes = [
                    new()
                    {
                        Id = 1,
                        CustomerId = 1,
                        ProposalId = 1
                    }
                ];
            var mockProposalSchemesQ = proposalSchemes.BuildMock();
            _ = context.Setup(x => x.ProposalSchemes).ReturnsDbSet(proposalSchemes);


            _ = context.Setup(x =>  x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

            var _systemUnderTest = new AddOrUpdateCustomerSchemeRequestHandler(context.Object);

            //Act
            var result = await _systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);

        }

    }
}
