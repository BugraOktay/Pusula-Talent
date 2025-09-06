using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;

public class FilterPeopleFromXmlSolution
{
    public static string FilterPeopleFromXml(string xmlData)
    {
        if (string.IsNullOrEmpty(xmlData))
        {
            return JsonSerializer.Serialize(new
            {
                Names = new List<string>(),
                TotalSalary = 0,
                AverageSalary = 0,
                MaxSalary = 0,
                Count = 0
            });
        }

        try
        {
            XDocument doc = XDocument.Parse(xmlData);
            var people = doc.Descendants("Person");

            var filteredPeople = people.Where(person =>
            {
                var ageElement = person.Element("Age");
                var departmentElement = person.Element("Department");
                var salaryElement = person.Element("Salary");
                var hireDateElement = person.Element("HireDate");

                if (ageElement == null || departmentElement == null || 
                    salaryElement == null || hireDateElement == null)
                    return false;

                if (!int.TryParse(ageElement.Value, out int age) ||
                    !decimal.TryParse(salaryElement.Value, out decimal salary) ||
                    !DateTime.TryParse(hireDateElement.Value, out DateTime hireDate))
                    return false;

                return age > 30 &&
                       departmentElement.Value == "IT" &&
                       salary > 5000 &&
                       hireDate < new DateTime(2019, 1, 1);
            }).ToList();

            if (filteredPeople.Count == 0)
            {
                return JsonSerializer.Serialize(new
                {
                    Names = new List<string>(),
                    TotalSalary = 0,
                    AverageSalary = 0,
                    MaxSalary = 0,
                    Count = 0
                });
            }

            var names = filteredPeople.Select(p => p.Element("Name").Value).OrderBy(n => n).ToList();
            var salaries = filteredPeople.Select(p => decimal.Parse(p.Element("Salary").Value)).ToList();

            var result = new
            {
                Names = names,
                TotalSalary = salaries.Sum(),
                AverageSalary = salaries.Average(),
                MaxSalary = salaries.Max(),
                Count = filteredPeople.Count
            };

            return JsonSerializer.Serialize(result);
        }
        catch
        {
            return JsonSerializer.Serialize(new
            {
                Names = new List<string>(),
                TotalSalary = 0,
                AverageSalary = 0,
                MaxSalary = 0,
                Count = 0
            });
        }
    }
}