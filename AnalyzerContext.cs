using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02
{
    public class AnalyzerContext
    {
        private IDocumentAnalyzer? _analyzer;

        // Метод для встановлення аналізатора
        public void SetAnalyzer(IDocumentAnalyzer analyzer)
        {
            _analyzer = analyzer;
        }

        // Метод для делегування виклику аналізу документів
        public List<SearchResult> AnalyzeDocument(string filePath, string? name = null, string? faculty = null, string? position = null)
        {
            if (_analyzer == null)
            {
                throw new InvalidOperationException("Аналізатор не встановлено.");
            }

            return _analyzer.AnalyzeDocument(filePath, name, faculty, position);
        }
    }

}
