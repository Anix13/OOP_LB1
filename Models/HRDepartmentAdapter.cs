
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Models
{
    // Адаптер для HRDepartment
    public class HRDepartmentAdapter : IHRDepartmentAdapter
    {
        private readonly HRDepartment _department;

        public HRDepartmentAdapter(HRDepartment department)
        {
            _department = department;
        }
        public string CompanyName => _department.CompanyName;
        public string GetCompanyInfo() => _department.CompanyName;
        public int GetEmployees() => _department.Employees;
        public double GetHoursPerMonth() => _department.HoursPerMonth;
        public decimal GetHourlyRate() => _department.HourlyRate;
        public double GetTaxRate() => _department.TaxRate;
        public string GetAddress() => _department.Address;
        public string GetContact() => _department.Contact;
        public decimal CalculateSalary() => _department.CalculateSalary();

        public void UpdateFields(int employees, double hours, decimal rate, double tax, string address, string contact)
        {
            _department.UpdateFields(employees, hours, rate, tax, address, contact);  
        }
    }


}
