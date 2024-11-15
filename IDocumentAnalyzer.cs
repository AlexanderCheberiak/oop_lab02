using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02
{
    public interface IDocumentAnalyzer
    {
        List<SearchResult> AnalyzeDocument(string filePath, string? name = null, string? faculty = null, string? position = null);
    }

}
