using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingList.Contracts;

namespace ShoppingList.Contracts.Interfaces
{
    public interface IShoppingList
    {
        Task<ShoppingResult> GetAll();

        Task<ShoppingResult> GetListById(int IdItem);

        Task<ShoppingResult> CreateListItem(Shopping shopping);

        Task<ShoppingResult> UpdateListItem(Shopping shopping);

        Task<ShoppingResult> DeleteListItem(Shopping shopping);
    }
}
