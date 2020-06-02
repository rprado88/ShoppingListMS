using System;
using System.Collections.Generic;
using ShoppingList.Contracts;
using ShoppingList.Enum;
namespace ShoppingList.Contracts
{
    public class ShoppingResult
    {
        public ShoppingResult()
        {
        }

        public ShoppingCodes ResultCode { get; set; }

        public List<Shopping> Data { get; set; }

        public string Message { get; set; }

        public bool IsSuccess
        {
            get
            {
                return this.ResultCode == ShoppingCodes.Success || this.ResultCode == ShoppingCodes.NoContent;
            }
        }
    }
}
