namespace ADFNet.Core.Models;

public class TextNode(string text, bool bold = false, bool italic = false, bool code = false)
    : ADFNode
{
    public override NodeType Type => NodeType.Text;
    public string Text { get; set; } = text;
    public bool IsBold { get; set; } = bold;
    public bool IsItalic { get; set; } = italic;
    public bool IsCode { get; set; } = code;
}