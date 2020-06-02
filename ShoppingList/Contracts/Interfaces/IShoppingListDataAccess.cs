using System;
using System.Collections.Generic;

namespace ShoppingList.Contracts.Interfaces
{
    public interface IShoppingListDataAccess
    {
        List<Shopping> GetAll();

        List<Shopping> GetListById(Shopping shopping);

        void CreateListItem(Shopping shopping);

        void UpdateListItem(Shopping shopping);

        void Delete(Shopping shopping);
    }
}
