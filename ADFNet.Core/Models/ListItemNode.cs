namespace ADFNet.Core.Models;

public class ListItemNode : ADFNode
{
    public override NodeType Type => NodeType.ListItem;
    public List<ADFNode> Content { get; set; } = new();

    public ListItemNode(IEnumerable<ADFNode> content)
    {
        Content.AddRange(content);
    }
}