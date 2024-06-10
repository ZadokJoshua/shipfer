namespace Shipfer.Models;

public record Message
{
    public string Text { get; set; } = string.Empty;
    public bool IsBot { get; set; }
}
