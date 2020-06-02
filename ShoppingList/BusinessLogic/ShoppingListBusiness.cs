using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShoppingList.Contracts;
using ShoppingList.Contracts.Interfaces;
using ShoppingList.Enum;

namespace ShoppingList.BusinessLogic
{
    public class ShoppingListBusiness : IShoppingList
    {
        private IConfiguration _configuration;
        private readonly ILogger<ShoppingListBusiness> _logger;
        private readonly IShoppingListDataAccess _shoppingListDataAccess;

        public ShoppingListBusiness(IConfiguration configuration, ILogger<ShoppingListBusiness> logger, IShoppingListDataAccess shoppingListDataAccess)
        {
            _configuration = configuration;
            _logger = logger;
            _shoppingListDataAccess = shoppingListDataAccess;
        }

        public async Task<ShoppingResult> CreateListItem(Shopping shopping)
        {
            var result = new ShoppingResult();
            try
            {
                if (!String.IsNullOrEmpty(shopping.Description))
                {
                    _shoppingListDataAccess.CreateListItem(shopping);
                    result.ResultCode = ShoppingCodes.Success;
                }
                else
                {
                    result.Message = "There's no item";
                    result.ResultCode = ShoppingCodes.BadRequest;
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = ShoppingCodes.InternalServerError;
                result.Message = ex.Message;
                this._logger.LogError(ex, ex.Message);
                //throw ex;
            }

            return result;
        }

        public async Task<ShoppingResult> GetAll()
        {
            var result = new ShoppingResult();
            try
            {
                var resultList = _shoppingListDataAccess.GetAll();

                if (resultList != null)
                {
                    result.Data = resultList;
                    result.ResultCode = ShoppingCodes.Success;
                }
                else
                {
                    result.ResultCode = ShoppingCodes.NoContent;
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = ShoppingCodes.InternalServerError;
                this._logger.LogError(ex, ex.Message);
                throw ex;
            }
            return result;
        }

        public async Task<ShoppingResult> GetListById(int IdItem)
        {
            var result = new ShoppingResult();
            try
            {
                if (IdItem != 0)
                {
                    var resultList = _shoppingListDataAccess.GetListById(new Shopping() { IdItem = IdItem });

                    if (resultList.Count > 0)
                    {
                        result.Data = resultList;
                        result.ResultCode = ShoppingCodes.Success;
                    }
                    else
                    {
                        result.ResultCode = ShoppingCodes.NoContent;
                        result.Message = "There's no record with this IdItem!";
                    }
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = ShoppingCodes.InternalServerError;
                this._logger.LogError(ex, ex.Message);
                throw ex;
            }

            return result;
        }

        public async Task<ShoppingResult> UpdateListItem(Shopping shopping)
        {
            var result = new ShoppingResult();

            try
            {
                if (shopping != null)
                {
                    _shoppingListDataAccess.UpdateListItem(shopping);
                    result.ResultCode = ShoppingCodes.Success;
                }
                else
                {
                    result.ResultCode = ShoppingCodes.BadRequest;
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = ShoppingCodes.InternalServerError;
                this._logger.LogError(ex, ex.Message);
                throw ex;
            }
            return result;
        }

        public async Task<ShoppingResult> DeleteListItem(Shopping shopping)
        {
            var result = new ShoppingResult();

            try
            {
                if (shopping != null)
                {
                    if (shopping.IdItem != 0)
                    {
                        _shoppingListDataAccess.Delete(shopping);
                        result.ResultCode = ShoppingCodes.Success;
                    }
                }
                else
                {
                    result.ResultCode = ShoppingCodes.BadRequest;
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = ShoppingCodes.InternalServerError;
                this._logger.LogError(ex, ex.Message);
                throw ex;
            }
            return result;
        }
    }
}
