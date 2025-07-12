namespace ADFNet.Core.Models;

public class ParagraphNode : ADFNode
{
    public override NodeType Type => NodeType.Paragraph;
    public List<ADFNode> Content { get; set; } = new();

    public ParagraphNode(IEnumerable<ADFNode> content)
    {
        Content.AddRange(content);
    }
}
