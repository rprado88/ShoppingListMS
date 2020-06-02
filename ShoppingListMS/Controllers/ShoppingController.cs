using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShoppingList;
using ShoppingList.BusinessLogic;
using ShoppingList.Contracts;
using ShoppingList.Contracts.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingListMS.Api.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingController : Controller
    {
        private IConfiguration _configuration;
        private readonly ILogger<ShoppingController> _logger;
        private readonly IShoppingList _shoppingList;

        public ShoppingController(IConfiguration configuration, ILogger<ShoppingController> logger, IShoppingList shoppingList)
        {
            _configuration = configuration;
            _logger = logger;
            _shoppingList = shoppingList;
        }

        // GET: api/values
        [HttpGet]
        public Task<ShoppingResult> Get()
        {
            var response = _shoppingList.GetAll();
            return response;
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetListById")]
        public Task<ShoppingResult> Get(int id)
        {
            var response = _shoppingList.GetListById(id);

            return response;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<ShoppingResult>> Create([FromBody]Shopping shopping)
        {
            var response = _shoppingList.CreateListItem(shopping);

            return CreatedAtRoute("GetListById", new { id = shopping.IdItem }, shopping);
        }

        // PUT api/values/5
        [HttpPut]
        public void Put([FromBody]Shopping shopping)
        {
            var response = _shoppingList.UpdateListItem(shopping);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var response = _shoppingList.DeleteListItem(new Shopping { IdItem = id });
        }
    }
}
