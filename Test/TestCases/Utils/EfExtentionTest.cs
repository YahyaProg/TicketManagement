using Core.CustomAttribute;
using Core.Utils;
using Core.ViewModel;
using Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Utils
{
    public class EfExtentionTest
    {
        [Theory]
        [InlineData(EOperation.Equal)]
        [InlineData(EOperation.NotEqual)]
        [InlineData(EOperation.GreaterThan)]
        [InlineData(EOperation.EqualOrGreaterThan)]
        [InlineData(EOperation.LessThan)]
        [InlineData(EOperation.EqualOrLessThan)]
        [InlineData(EOperation.Contains)]
        [InlineData(EOperation.StartWith)]
        [InlineData(EOperation.EndWith)]
        public void GetOperation_test(EOperation op)
        {
            //Arrange

            //Act
            var result = EfExtension.GetOperation(op, 0);

            //Assert
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(EOrAnd.And)]
        [InlineData(EOrAnd.Or)]
        public void GetExpressionType_test(EOrAnd op)
        {
            //Arrange

            //Act
            var result = EfExtension.GetExpressionType(op);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetOrderBy_test()
        {
            var model = new OrganizationIM();

            var list = new List<OrganizationIM>().AsQueryable();

            model.GetOrderBy(ref list);

            Assert.NotNull(list);
        }

        [Fact]
        public void SearchParamConstractor_test()
        {
            var model = new Test();

            Assert.NotNull(model);
        }
    }
    public class Test
    {
        [SearchParam(EOrAnd.And , "id")]
        public int Id { get; set; }
    }
}
