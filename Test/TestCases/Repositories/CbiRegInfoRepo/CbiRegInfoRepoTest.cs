using Core.ViewModel.CbiRegInfo;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Repositories.CbiRegInfoRepo
{

    public class CbiRegInfoRepoTest
    {
        private readonly Mock<DBContext> _contextMoq = new();
        private readonly CbiRegInfoIM _cbiRegInfoIm = new();
        [Fact]
        public async Task AdvanceSearch_ReturnsPaginatedList()
        {
            //Arrange
            var CbiRegInfoModel = new List<CbiRegInfo>()
            {
                 new(){Id = 1,RegPlaceId=2 }
            };
            var CityModel = new List<City>()
            {
                new(){Id= 2, Code= "123",Title="testCity" }
            };

            _contextMoq.Setup(x => x.CbiRegInfos).ReturnsDbSet(CbiRegInfoModel);

            _contextMoq.Setup(x => x.Cities).ReturnsDbSet(CityModel);


            //Act
            var cbiRegInfoRepo = new Infrastructure.Repositories.CbiRegInfoRepository.CbiRegInfoRepo(_contextMoq.Object);
            var result = await cbiRegInfoRepo.AdvanceSearch(_cbiRegInfoIm);

            //Assert
            Assert.NotEmpty(result.Items);

        }
    }
}
