using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class FilterEmployeesSolution
{
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        if (employees == null)
        {
            return JsonSerializer.Serialize(new
            {
                Names = new List<string>(),
                TotalSalary = 0,
                AverageSalary = 0,
                MinSalary = 0,
                MaxSalary = 0,
                Count = 0
            });
        }

        var filteredEmployees = employees.Where(emp =>
            emp.Age >= 25 && emp.Age <= 40 &&
            (emp.Department == "IT" || emp.Department == "Finance") &&
            emp.Salary >= 5000 && emp.Salary <= 9000 &&
            emp.HireDate > new DateTime(2017, 12, 31) // 2017'den sonra
        ).ToList();

        if (filteredEmployees.Count == 0)
        {
            return JsonSerializer.Serialize(new
            {
                Names = new List<string>(),
                TotalSalary = 0,
                AverageSalary = 0,
                MinSalary = 0,
                MaxSalary = 0,
                Count = 0
            });
        }

        // İsimleri uzunluklarına göre azalan, ardından alfabetik sıralama
        var sortedNames = filteredEmployees
            .OrderByDescending(emp => emp.Name.Length)
            .ThenBy(emp => emp.Name)
            .Select(emp => emp.Name)
            .ToList();

        var salaries = filteredEmployees.Select(emp => emp.Salary).ToList();

        var result = new
        {
            Names = sortedNames,
            TotalSalary = salaries.Sum(),
            AverageSalary = Math.Round(salaries.Average(), 2),
            MinSalary = salaries.Min(),
            MaxSalary = salaries.Max(),
            Count = filteredEmployees.Count
        };

        return JsonSerializer.Serialize(result);
    }
}