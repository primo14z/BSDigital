using System.Text.Json.Serialization;

namespace BSDigital.Models;

public class OrderBook
{
    public DateTime AcqTime { get; set; }
    [JsonPropertyName("Bids")]
    public List<Order> Bids { get; set; }
    [JsonPropertyName("Asks")]
    public List<Order> Asks { get; set; }
}