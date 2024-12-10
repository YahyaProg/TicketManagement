using Application.Services.FinancialService;
using Core.GenericResultModel;
using Core.ViewModel.FinancialModels;
using Infrastructure;
using MediatR;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.FinancialTests;

public class GetCalcFinancialExcelTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Test(bool isSuccess)
    {
        var mediator = new Mock<IMediator>();

        mediator.Setup(x => x.Send(It.IsAny<GetCalcFinancialRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ApiResult<SearchCalcFinancialVM>(200, isSuccess) 
            {
                Message = "3123123141231",
                Data = new SearchCalcFinancialVM
                {
                    Years =
                    [
                        new()
                        {
                            Id = 1,
                            ToDate = DateTime.Now,
                        }
                    ],
                    Rows = [
                        new(){
                            Id = 1,
                            Code = "1",
                            Indx = 1,
                            IsHeader = false,
                            Title = "1",
                            RowItems = [
                                new(){
                                    Id = 2,
                                    RowId = 1,
                                    Title = "2",
                                    ToDate = DateTime.Now,
                                    Value = 100,
                                    YearId = 1,
                                }
                                ]
                        }
                        ]
                }
            });

        var context = new Mock<DBContext>();
        context.Setup(x => x.ProposalDescriptions).ReturnsDbSet([new() {
            Category = "finInfo1",
            Descriptions = ""
        }]);

        var request = new GetCalcFinancialExcelRequest() { CorporateCustomerId = 1, Type = Core.Enums.ECalcFinancialInfo_type.ammodi_income_statement };

        var handler = new GetCalcFinancialExcelRequestHandler(mediator.Object, context.Object);

        var res = await handler.Handle(request, CancellationToken.None);

        Assert.Equal(isSuccess, res.IsSuccess);
    }
}
