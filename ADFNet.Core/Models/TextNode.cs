namespace ADFNet.Core.Models;

public class TextNode : ADFNode
{
    public override NodeType Type => NodeType.Text;
    public string Text { get; set; } = string.Empty;
    public bool Bold { get; set; }
    public bool Italic { get; set; }
    public bool Underline { get; set; }
    public bool Strike {get; set;}
    public bool Code { get; set; }
}