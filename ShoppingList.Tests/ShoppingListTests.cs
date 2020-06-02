using System;
using Moq;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using ShoppingList;
using ShoppingList.Contracts.Interfaces;
using ShoppingList.BusinessLogic;

using ShoppingList.Enum;
using ShoppingList.Contracts;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ShoppingList.Tests
{
    public class ShoppingListTests
    {
        private readonly ShoppingListBusiness _shoppingList;
        private readonly Mock<IShoppingListDataAccess> _iShoppingList;

        public ShoppingListTests()
        {
            var loggerMock = new Mock<ILogger<ShoppingListBusiness>>();

            _iShoppingList = new Mock<IShoppingListDataAccess>();
            

            _shoppingList = new ShoppingListBusiness(loggerMock.Object, _iShoppingList.Object);
        }


        #region CreateListItem

        [Fact]
        public void CreateListItem_Request()
        {
            _iShoppingList.Setup(x => x.CreateListItem(It.IsAny<Shopping>()));
         
            var response = _shoppingList.CreateListItem(new Contracts.Shopping() { Description = "Banana" });

            response.Result.ResultCode.Should().Be(ShoppingCodes.Success);
        }

        [Fact]
        public void CreateListItem_NullRequest()
        {
            var response = _shoppingList.CreateListItem(null);

            response.Result.Data.Should().BeNull();
            response.Result.ResultCode.Should().Be(ShoppingCodes.InternalServerError);
        }

        [Fact]
        public void CreateListItem_InvalidRequest()
        {
            var response = _shoppingList.CreateListItem(new Contracts.Shopping());

            response.Result.Data.Should().BeNull();
            response.Result.ResultCode.Should().Be(ShoppingCodes.BadRequest);
        }

        #endregion

        #region GetAll

        [Fact]
        public void GetAll_ShouldReturnSucess()
        {

            _iShoppingList.Setup(x => x.GetAll())
                .Returns((new List<Shopping>()
                {
                   new Shopping(){ IdItem = 1 , Description = "Banana"},
                   new Shopping(){ IdItem = 2 , Description = "Apple"}

                }));

            var response = _shoppingList.GetAll();

            response.Result.Data.Should().NotBeNull();
            response.Result.ResultCode.Should().Be(ShoppingCodes.Success);
        }

        [Fact]
        public void GetAll_ShouldReturnNoContent()
        {
            var response = _shoppingList.GetAll();

            response.Result.Data.Should().BeNull();
            response.Result.ResultCode.Should().Be(ShoppingCodes.NoContent);
        }

        #endregion

        #region GetListById

        [Fact]
        public void GetListById_ShouldReturnSucess()
        {

            _iShoppingList.Setup(x => x.GetListById(It.IsAny<Shopping>()))
                .Returns((new List<Shopping>()
                {
                   new Shopping(){ IdItem = 1 , Description = "Banana"}
                }));

            var response = _shoppingList.GetListById(9);

            response.Result.Data.Should().NotBeNull();
            response.Result.ResultCode.Should().Be(ShoppingCodes.Success);
        }

        [Fact]
        public void GetListById_ShouldReturnNoContent()
        {

            _iShoppingList.Setup(x => x.GetListById(It.IsAny<Shopping>()))
                .Returns((new List<Shopping>()));

            var response = _shoppingList.GetListById(9);

            response.Result.Data.Should().BeNull();
            response.Result.ResultCode.Should().Be(ShoppingCodes.NoContent);
        }

        #endregion
    }
}
