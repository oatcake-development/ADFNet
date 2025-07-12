namespace ADFNet.Core.Models;

public class BulletListNode : ADFNode
{
    public override NodeType Type => NodeType.BulletList;
    public List<ListItemNode> Items { get; set; } = new();
}