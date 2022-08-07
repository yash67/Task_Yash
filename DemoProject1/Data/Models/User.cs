using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoProject1.Data.Models
{
    public class User
    {       
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("userId")]
        public int userId { get; set; }
        [JsonProperty("title")]
        public string title { get; set; }
        [JsonProperty("completed")]
        public bool completed { get; set; }
    }
}
