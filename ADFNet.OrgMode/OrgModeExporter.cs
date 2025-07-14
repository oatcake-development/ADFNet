/*
 * Copyright 2025 Lee Williams
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Text;
using ADFNet.Core.Export;
using ADFNet.Core.Models;

namespace ADFNet.OrgMode.Exporters;

public class OrgModeExporter : IADFExporter
{
    public string Export(ADFNode node)
    {
        var sb = new StringBuilder();
        ExportNode(node, sb);
        return sb.ToString();
    }

    private void ExportNode(ADFNode node, StringBuilder sb)
    {
        switch (node)
        {
            case ADFDocument doc:
                foreach (var child in doc.Content)
                    ExportNode(child, sb);
                break;

            case ParagraphNode para:
                foreach (var child in para.Content)
                    ExportNode(child, sb);
                sb.AppendLine(); // Paragraph break
                break;

            case TextNode text:
                sb.Append(RenderTextNode(text));
                break;

            case HardBreakNode:
                sb.AppendLine(); // Org-mode line break: 2 spaces + newline
                break;

            case BulletListNode list:
                foreach (var item in list.Items)
                    ExportNode(item, sb);
                break;

            case ListItemNode item:
                sb.Append("- ");
                foreach (var child in item.Content)
                    ExportNode(child, sb);
                sb.AppendLine();
                break;
        }
    }

    private string RenderTextNode(TextNode node)
    {
        var text = node.Text;

        if (node.Code)
            text = $"~{text}~";
        if (node.Bold)
            text = $"*{text}*";
        if (node.Italic)
            text = $"/{text}/";
        if (node.Underline)
            text = $"_{text}_";
        if (node.Strike)
            text = $"+{text}+";

        return text;
    }
}