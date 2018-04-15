using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MVCAuth.Models
{
    [Serializable]
    public class Country
    {
        [JsonProperty("countryId")]
        public string CountryId { get; set; }
        [JsonProperty("countryName")]
        public string CountryName { get; set; }
        
        public string Currency{get;set;}
        public byte[] Img{get;set;}
    }
}