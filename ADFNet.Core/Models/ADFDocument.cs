namespace ADFNet.Core.Models;

public class ADFDocument
{
    public int Version { get; set; } = 1;
    public string Type { get; set; } = "doc";
    public List<ADFNode> Content { get; set; } = new();

    public bool IsValid()
    {
        return Type == "doc" && Version == 1 && Content.Any();
    }
}