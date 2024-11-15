using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab02
{
    public class SaxAnalyzer : IDocumentAnalyzer
    {
        public List<SearchResult> AnalyzeDocument(string filePath, string? name, string? faculty, string? position)
        {
            var results = new List<SearchResult>();
            using var reader = XmlReader.Create(filePath);

            SearchResult? currentResult = null;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Person")
                {
                    currentResult = new SearchResult
                    {
                        Name = reader.GetAttribute("Name"),
                        Faculty = reader.GetAttribute("Faculty"),
                        Position = reader.GetAttribute("Position"),
                        Salary = reader.GetAttribute("Salary")
                    };

                    if ((string.IsNullOrEmpty(name) || currentResult.Name?.Contains(name, StringComparison.OrdinalIgnoreCase) == true) &&
                        (string.IsNullOrEmpty(faculty) || currentResult.Faculty?.Contains(faculty, StringComparison.OrdinalIgnoreCase) == true) &&
                        (string.IsNullOrEmpty(position) || currentResult.Position?.Contains(position, StringComparison.OrdinalIgnoreCase) == true))
                    {
                        results.Add(currentResult);
                    }
                }
            }

            return results;
        }
    }



}
