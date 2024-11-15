using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab02
{
    public class LinqToXmlAnalyzer : IDocumentAnalyzer
    {
        public List<SearchResult> AnalyzeDocument(string filePath, string? name, string? faculty, string? position)
        {
            var results = new List<SearchResult>();

            var doc = XDocument.Load(filePath);

            var query = doc.Descendants("Person").Where(person =>
                (string.IsNullOrEmpty(name) || (person.Attribute("Name")?.Value.Contains(name, StringComparison.OrdinalIgnoreCase) ?? false)) &&
                (string.IsNullOrEmpty(faculty) || (person.Attribute("Faculty")?.Value.Contains(faculty, StringComparison.OrdinalIgnoreCase) ?? false)) &&
                (string.IsNullOrEmpty(position) || (person.Attribute("Position")?.Value.Contains(position, StringComparison.OrdinalIgnoreCase) ?? false))
            );

            foreach (var element in query)
            {
                results.Add(new SearchResult
                {
                    Name = element.Attribute("Name")?.Value,
                    Faculty = element.Attribute("Faculty")?.Value,
                    Position = element.Attribute("Position")?.Value,
                    Salary = element.Attribute("Salary")?.Value
                });
            }

            return results;
        }
    }



}
