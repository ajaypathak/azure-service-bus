using System.Text.Json.Serialization;

namespace TopicsAndSubscriptions.Model
{
    public class IndianState
    {
        [JsonPropertyName("stateCode")]
        public string StateCode { get; set; }

    }
}