using ADFNet.Core.Models;
using JetBrains.Annotations;

namespace ADFNet.Core.Export;

public interface IADFExporter
{
    string Export(ADFNode node);
}