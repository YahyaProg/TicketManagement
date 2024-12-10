using Application.Services.BaseService;
using Core.Entities;
using Core.Helpers;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using Test.Helper;
using static Application.Services.IndividualCustomerService.AddIndividualCustomerRequest;

namespace Test.TestCases.Services.IndividualCustomerServicesTest
{
    public class AddIndividualCustomerTest
    {

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task AddIndividualCustomer_Test(int type)
        {
            // arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            var helper = new Mock<IUserHelper>();
            var mediator = new Mock<IMediator>();

            helper.Setup(x => x.GetUserFromToken()).Returns(new Core.ViewModel.UserDto() { Id = "1" });

            collection.Context.Setup(x => x.Branches).ReturnsDbSet([
                new Branch(){Id = type != 3 ? 1 : 0} // 3= 0
                ]);


            collection.Context.Setup(x => x.BankStaffs).ReturnsDbSet([
                new BankStaff(){Id = 1, UserId = "1"}
                ]);

            collection.Context.Setup(x => x.Customers).ReturnsDbSet([
               new Customer(){
                   Id = type != 2 ? 1 : 0, // 2= 0
                   IndividualCustomer = new(){
                       Id = type != 2 ? 1 : 0, // 2 = 0
                       HasInquiry = false
                   }
                    
               }
               ]);

            mediator.Setup(x => x.Send(It.IsAny<InquiryCustomerV2Request>(), CancellationToken.None)).ReturnsAsync(
                new Core.GenericResultModel.ApiResult<long>()
                {
                    IsSuccess = type != 1 ? true : false, // 1 = false
                    Data = 1,
                    Message = "Afarin"
                });


            var handler = new AddIndividualCustomerRequestHandler(collection.Context.Object, mediator.Object, helper.Object);


            // act
            var result = await handler.Handle(new() { BranchId = 1, BirthDate = DateTime.Now, NationalId = "asda" }, CancellationToken.None);

            // assert
            Assert.NotNull(result);
        }
    }
}
