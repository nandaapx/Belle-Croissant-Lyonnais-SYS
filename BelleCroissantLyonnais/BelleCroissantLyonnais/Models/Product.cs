using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelleCroissantLyonnais.Models
{
    public class Product
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("cost")]
        public decimal Cost { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("seasonal")]
        public bool Seasonal { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("introducedDate")]
        public DateTime IntroducedDate { get; set; }
    }
}
