using Newtonsoft.Json;
using System;

namespace Profile.Domain
{
    public class Profile
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
