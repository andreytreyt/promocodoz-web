using Newtonsoft.Json;
using Promocodoz.Domain.Core.Enums;

namespace Promocodoz.Web.Models
{
    public class CodeJsonModel
    {
        [JsonProperty("sid")]
        public string Sid { get; set; }
        [JsonProperty("secret")]
        public string Secret { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("platform")]
        public Platform? Platform { get; set; }
    }
}