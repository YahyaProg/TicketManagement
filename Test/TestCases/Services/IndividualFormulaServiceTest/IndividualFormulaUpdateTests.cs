using Application.Services.IndividualFormulaService;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.IndividualFormulaServiceTest;

public class IndividualFormulaUpdateTests
{
    [Fact]
    public async Task Success()
    {
        var request = new IndividualFormulaUpdateRequest()
        {
            Code = "ajab",
            Formula = "reza+mamad",
            Id = 1
        };

        var context = new Mock<DBContext>();

        context.Setup(x => x.IndividualFormula)
            .ReturnsDbSet([new() { Id = 1, Code = "1234", Formula = "12314" },
            new() { Id = 2, Code = "mamad", Formula = "12314" }]);

        context.Setup(x => x.IndividualParam).ReturnsDbSet([new() { Id = 1, Code = "reza" }]);

        context.Setup(x => x.IndividualRelations).ReturnsDbSet([]);

        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

        var handler = new IndividualFormulaUpdateRequestHandler(context.Object);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.True(res.IsSuccess);
    }
}
