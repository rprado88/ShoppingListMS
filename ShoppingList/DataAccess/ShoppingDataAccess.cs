using System;
using System.Collections.Generic;
using MySql;
using Dapper;
using ShoppingList.Contracts;
using ShoppingList.Contracts.Interfaces;

namespace ShoppingList.DataAccess
{
    public class ShoppingDataAccess : IShoppingListDataAccess
    {
        private readonly string ConnectionString = "SERVER=localhost; DATABASE=ShoppingList; UID=root; PASSWORD=sa123";

        public ShoppingDataAccess()
        {
        }

        public void CreateListItem(Shopping shopping)
        {
            try
            {
                using (var sqlConnection = new MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                {
                    var sqlExec = sqlConnection.Execute("INSERT INTO ShoppingList(Description) VALUES (@Description)", shopping);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Shopping> GetAll()
        {
            try
            { 
            List<Shopping> resultList = new List<Shopping>();

            using (var sqlConnection = new MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
            {
                var result = sqlConnection.Query<Shopping>("Select * from ShoppingList");

                foreach (Shopping item in result)
                {
                    resultList.Add(item);
                }
            }
                return resultList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Shopping> GetListById(Shopping shopping)
        {
            try
            {
                List<Shopping> resultList = new List<Shopping>();

                using (var sqlConnection = new MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                {
                    var result = sqlConnection.Query<Shopping>("Select * from ShoppingList where IdItem = @IdItem", shopping);

                    foreach (Shopping item in result)
                    {
                        resultList.Add(item);
                    }
                }
                return resultList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateListItem(Shopping shopping)
        {
            try
            { 
                using (var sqlConnection = new MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                {
                    sqlConnection.Execute(@"UPDATE ShoppingList
                                           SET Description = @Description
                                           WHERE IdItem = @IdItem", shopping);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(Shopping shopping)
        {
            try
            {
                using (var sqlConnection = new MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                {
                    sqlConnection.Execute("DELETE FROM ShoppingList WHERE IdItem = @idItem", shopping);
                }
            }
            catch (Exception)
            {
                throw;
            }
}

    }
}
