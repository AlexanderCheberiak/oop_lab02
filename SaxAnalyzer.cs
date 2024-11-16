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
        public List<SearchResult> AnalyzeDocument(string filePath, string? name, string? faculty, string? department, string? position, string? salaryFrom, string? salaryTo)
        {
            var results = new List<SearchResult>();
            using var reader = XmlReader.Create(filePath);

            bool isSalaryFromValid = int.TryParse(salaryFrom, out var salaryFromValue);
            bool isSalaryToValid = int.TryParse(salaryTo, out var salaryToValue);

            string? currentFaculty = null;
            string? currentDepartment = null;

            string? employeeName = null;
            string? employeePosition = null;
            string? employeeSalary = null;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "faculty")
                    {
                        currentFaculty = reader.GetAttribute("name");
                    }
                    else if (reader.Name == "department")
                    {
                        currentDepartment = reader.GetAttribute("name");
                    }
                    else if (reader.Name == "employee")
                    {
                        employeeName = null;
                        employeePosition = null;
                        employeeSalary = null;
                    }
                    else if (reader.Name == "name")
                    {
                        employeeName = reader.ReadElementContentAsString();
                    }
                    else if (reader.Name == "position")
                    {
                        employeePosition = reader.ReadElementContentAsString();
                    }
                    else if (reader.Name == "salary")
                    {
                        employeeSalary = reader.ReadElementContentAsString();
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "employee")
                {
                    if (!string.IsNullOrEmpty(employeeName) &&
                        int.TryParse(employeeSalary, out var salary))
                    {
                        var employee = new SearchResult
                        {
                            Name = employeeName,
                            Faculty = currentFaculty,
                            Department = currentDepartment,
                            Position = employeePosition,
                            Salary = employeeSalary
                        };

                        if ((string.IsNullOrEmpty(name) || employee.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                            (string.IsNullOrEmpty(faculty) || currentFaculty?.Contains(faculty, StringComparison.OrdinalIgnoreCase) == true) &&
                            (string.IsNullOrEmpty(department) || currentDepartment?.Contains(department, StringComparison.OrdinalIgnoreCase) == true) &&
                            (string.IsNullOrEmpty(position) || employee.Position?.Contains(position, StringComparison.OrdinalIgnoreCase) == true) &&
                            (!isSalaryFromValid || salary >= salaryFromValue) &&
                            (!isSalaryToValid || salary <= salaryToValue))
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
