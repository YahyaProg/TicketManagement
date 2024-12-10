using Application.Services.PorterService;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.PorterTests;

public class AddOrUpdatePorterTests
{
    [Fact]
    public async Task Success()
    {
        var request = new AddOrUpdatePorterRequest()
        {
            CustomerId = 1,
            ProposalId = 1,
            ProposalSchemeId = 1,
            Items =
            [
                new()
                {
                    Id = 1,
                    PorterItemId = 1,
                    Value = 1,
                },
                new()
                {
                    Id = null,
                    PorterItemId = 1,
                    Value = 1,
                }
            ]
        };

        var context = new Mock<DBContext>();
        context.Setup(x => x.Porters).ReturnsDbSet(new List<Porter>()
        {
            new()
            {
                Id= 1,
                PorterItemId = 1,
                Value = 1,
                ProposalId= 1,
                ProposalSchemeId= 1,
                CustomerId= 1,
            }
        });

        context.Setup(x => x.UpdateRange());
        context.Setup(x => x.AddRange());
        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

        var handler = new AddOrUpdatePorterRequestHandler(context.Object);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.True(res.IsSuccess);
    }
}
