using System.Text;
using ADFNet.Core.Exporting;
using ADFNet.Core.Models;

namespace ADFNet.OrgMode.Exporters;

public class OrgModeExporter : IADFExporter
{
    public string Export(ADFNode node)
    {
        var sb = new StringBuilder();
        VisitNode(node, sb, 0);
        return sb.ToString();
    }

    public string Export(IEnumerator<ADFNode> nodes)
    {
        var sb = new StringBuilder();

        while (nodes.MoveNext())
        {
            VisitNode(nodes.Current, sb, 0);
        }

        return sb.ToString();
    }

    private void VisitNode(ADFNode node, StringBuilder sb, int depth)
    {
        switch (node)
        {
            case ParagraphNode paragraph:
                sb.AppendLine(ExportParagraph(paragraph));
                break;

            case TextNode text:
                sb.Append(text.Text);
                break;

            case BulletListNode list:
                foreach (var item in list.Items)
                {
                    sb.AppendLine($"- {Export(item)}");
                }
                break;

            case HardBreakNode:
                sb.AppendLine();
                break;

            default:
                sb.AppendLine($"[Unsupported node: {node.Type}]");
                break;
        }
    }

    private string ExportParagraph(ParagraphNode paragraph)
    {
        var sb = new StringBuilder();

        foreach (var child in paragraph.Content)
        {
            VisitNode(child, sb, 0);
        }

        return sb.ToString().TrimEnd();
    }
}