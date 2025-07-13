using ADFNet.Core.Models;
using JetBrains.Annotations;

namespace ADFNet.Core.Exporting;

public interface IADFExporter
{
    string Export(ADFNode node);
    string Export(IEnumerator<ADFNode> nodes);
}