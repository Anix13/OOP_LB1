using System;
using System.Collections.Generic;

namespace HRProject.Models
{
    // Интерфейс адаптера
    public interface IHRDepartmentAdapter
    {
        string GetCompanyInfo();
        int GetEmployees();
        double GetHoursPerMonth();
        decimal GetHourlyRate();
        double GetTaxRate();
        string GetAddress();  // Метод для получения адреса
        string GetContact();
        decimal CalculateSalary();


        void UpdateFields(int employees, double hours, decimal rate, double tax, string address, string contact);
    }
}
