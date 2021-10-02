using System.Text.Json.Serialization;

namespace Todo.Grpc.Api.Models
{
    public class PersonDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        
        [JsonPropertyName("age")]
        public int Age { get; set; }
    }
}