using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab8.Models
{
    public class Country
    {
        [JsonProperty(PropertyName = "Država")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "Valuta")]
        public string currency { get; set; }

        [JsonProperty(PropertyName = "Srednji za devize")]
        public float devize { get; set; }
    }
}
