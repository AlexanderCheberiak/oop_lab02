using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab02
{
    public class LinqToXmlAnalyzer : IDocumentAnalyzer
    {
        public List<SearchResult> AnalyzeDocument(string filePath, string? name, string? faculty, string? department, string? position, string? salaryFrom, string? salaryTo = null)
        {
            var results = new List<SearchResult>();

            var doc = XDocument.Load(filePath);

            bool isSalaryFromValid = int.TryParse(salaryFrom, out var salaryFromValue);
            bool isSalaryToValid = int.TryParse(salaryTo, out var salaryToValue);

            var query = doc.Descendants("employee").Where(employee =>
            {
                var parentDepartment = employee.Parent;
                var parentFaculty = parentDepartment?.Parent;

                var salaryText = employee.Element("salary")?.Value;
                bool isSalaryValid = int.TryParse(salaryText, out var salary);

                return
                    (string.IsNullOrEmpty(name) || (employee.Element("name")?.Value.Contains(name, StringComparison.OrdinalIgnoreCase) ?? false)) &&
                    (string.IsNullOrEmpty(faculty) || (parentFaculty?.Attribute("name")?.Value.Contains(faculty, StringComparison.OrdinalIgnoreCase) ?? false)) &&
                    (string.IsNullOrEmpty(department) || (parentDepartment?.Attribute("name")?.Value.Contains(department, StringComparison.OrdinalIgnoreCase) ?? false)) &&
                    (string.IsNullOrEmpty(position) || (employee.Element("position")?.Value.Contains(position, StringComparison.OrdinalIgnoreCase) ?? false)) &&
                    (!isSalaryFromValid || (isSalaryValid && salary >= salaryFromValue)) &&
                    (!isSalaryToValid || (isSalaryValid && salary <= salaryToValue));
            });

            foreach (var element in query)
            {
                var parentDepartment = element.Parent;
                var parentFaculty = parentDepartment?.Parent;

                results.Add(new SearchResult
                {
                    Name = element.Element("name")?.Value,
                    Faculty = parentFaculty?.Attribute("name")?.Value,
                    Department = parentDepartment?.Attribute("name")?.Value,
                    Position = element.Element("position")?.Value,
                    Salary = element.Element("salary")?.Value
                });
            }

            return results;
        }


    }

}
