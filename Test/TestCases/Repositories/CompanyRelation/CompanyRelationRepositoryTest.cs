using Application.Services.CompanyRelationService;
using Core.Entities;
using Core.ViewModel.CompanyRelation;
using Gateway;
using Infrastructure;
using Infrastructure.Extensions;
using Infrastructure.Repositories.CompanyRelationRepository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;
using Test.Helper;

namespace Test.TestCases.Repositories.CompanyRelation
{
    public class CompanyRelationRepositoryTest
    {
        private readonly Mock<DBContext> _contextMoq = new();

        [Fact]
        public async Task searchTest()
        {
            var data = new List<Core.Entities.CompanyRelation>()
            {
                new Core.Entities.CompanyRelation()
                {
                    FirstName = "amirhosein",
                    PositionTypeId = 123
                }
            };

            var data2 = new List<PositionType>()
            {
                new PositionType()
                {
                    Id = 123,
                    Title= "salam"
                }
            };

            var request = new SearchCompanyRelationRequest()
            {
                FirstName = "amirhosein",
                Page = 1,
                Size = 10
            };

            var input = new PaginationSpecification<CompanyRelationSearchVM>
            {
                Conditions = request.ToExpresion<SearchCompanyRelationRequest, CompanyRelationSearchVM>().ToList(),
                PageIndex = request.Page,
                PageSize = request.Size,
                OrderBy = EfExtension.GetOrderBy<SearchCompanyRelationRequest, CompanyRelationSearchVM>(request)
            };

            _contextMoq.Setup(x => x.CompanyRelations).ReturnsDbSet(new List<Core.Entities.CompanyRelation>(data));
            _contextMoq.Setup(x => x.PositionTypes).ReturnsDbSet(new List<Core.Entities.PositionType>(data2));


            //Arrange
            var systemUnderTest = new CompanyRelationRepo(_contextMoq.Object);

            //Act
            var result = await systemUnderTest.Search(request);

            //Assert
            Assert.NotNull(result);
        }
    }
}
