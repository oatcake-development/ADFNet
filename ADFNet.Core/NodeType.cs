using System.ComponentModel;
using System.Reflection;

namespace ADFNet.Core;

public enum NodeType
{
    [Description("paragraph")]
    Paragraph,
    [Description("text")]
    Text,
    [Description("bulletList")]
    BulletList,
    [Description("listItem")]
    ListItem,
    [Description("hardBreak")]
    HardBreak
}


public static class EnumExtensions
{
    public static string ToADFString(this Enum value)
    {
        return value
            .GetType()
            .GetMember(value.ToString())[0]
            .GetCustomAttribute<DescriptionAttribute>()?
            .Description ?? value.ToString();
    }
}
