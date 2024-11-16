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
        public List<SearchResult> AnalyzeDocument(string filePath, string? name, string? faculty, string? department, string? position, string? salaryFrom, string? salaryTo)
        {
            var results = new List<SearchResult>();

            var doc = new XmlDocument();
            doc.Load(filePath);

            bool isSalaryFromValid = int.TryParse(salaryFrom, out var salaryFromValue);
            bool isSalaryToValid = int.TryParse(salaryTo, out var salaryToValue);

            foreach (XmlNode facultyNode in doc.DocumentElement.SelectNodes("faculty"))
            {
                var facultyName = facultyNode.Attributes?["name"]?.Value;

                if (!string.IsNullOrEmpty(faculty) && !string.Equals(facultyName, faculty, StringComparison.OrdinalIgnoreCase))
                    continue;

                foreach (XmlNode departmentNode in facultyNode.SelectNodes("department"))
                {
                    var departmentName = departmentNode.Attributes?["name"]?.Value;

                    if (!string.IsNullOrEmpty(department) && !string.Equals(departmentName, department, StringComparison.OrdinalIgnoreCase))
                        continue;

                    foreach (XmlNode employeeNode in departmentNode.SelectNodes("employee"))
                    {
                        var employee = new SearchResult
                        {
                            Name = employeeNode.SelectSingleNode("name")?.InnerText,
                            Faculty = facultyName,
                            Department = departmentName,
                            Position = employeeNode.SelectSingleNode("position")?.InnerText,
                            Salary = employeeNode.SelectSingleNode("salary")?.InnerText
                        };

                        if (int.TryParse(employee.Salary, out var salary) &&
                            (!isSalaryFromValid || salary >= salaryFromValue) &&
                            (!isSalaryToValid || salary <= salaryToValue) &&
                            (string.IsNullOrEmpty(name) || employee.Name?.Contains(name, StringComparison.OrdinalIgnoreCase) == true) &&
                            (string.IsNullOrEmpty(position) || employee.Position?.Contains(position, StringComparison.OrdinalIgnoreCase) == true))
                        {
                            results.Add(employee);
                        }
                    }
                }
            }

            return results;
        }
    }

}