namespace ADFNet.Core.Models;

public class ADFDocument : ADFNode
{
    public override NodeType Type => NodeType.Document;

    public List<ADFNode> Content { get; set; } = new();

    public ADFDocument() { }

    public ADFDocument(IEnumerable<ADFNode> content)
    {
        Content.AddRange(content);
    }
}