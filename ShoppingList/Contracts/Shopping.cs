using System;
using Newtonsoft.Json;

namespace ShoppingList.Contracts
{
    public class Shopping
    {
        [JsonProperty("id", Required = Required.Always)]
        public int IdItem { get; set; }

        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; }
    }
}
