using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab02
{
    public class DomAnalyzer : IDocumentAnalyzer
    {
        public List<SearchResult> AnalyzeDocument(string filePath, string? name, string? faculty, string? position)
        {
            var results = new List<SearchResult>();

            var doc = new XmlDocument();
            doc.Load(filePath);

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var person = new SearchResult
                {
                    Name = node.Attributes["Name"]?.Value,
                    Faculty = node.Attributes["Faculty"]?.Value,
                    Position = node.Attributes["Position"]?.Value,
                    Salary = node.Attributes["Salary"]?.Value
                };

                if ((string.IsNullOrEmpty(name) || person.Name?.Contains(name, StringComparison.OrdinalIgnoreCase) == true) &&
                    (string.IsNullOrEmpty(faculty) || person.Faculty?.Contains(faculty, StringComparison.OrdinalIgnoreCase) == true) &&
                    (string.IsNullOrEmpty(position) || person.Position?.Contains(position, StringComparison.OrdinalIgnoreCase) == true))
                {
                    results.Add(person);
                }
            }

            return results;
        }
    }




}
