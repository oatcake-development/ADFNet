namespace ADFNet.Core.Models;

public class ListItemNode : ADFNode
{
    public override NodeType Type => NodeType.ListItem;
    public List<ADFNode> Content { get; set; } = new();
}