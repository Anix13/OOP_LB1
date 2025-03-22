using HRProject.Models;
using HRProject.Models.HRProject.Models;

namespace HRProject.Factories
{
    // Фабрика для IT-отдела
    public class ITDepartmentFactory : IHRDepartmentFactory
    {
        public HRDepartment CreateHRDepartment(string companyName, int employees, double hoursPerMonth, decimal hourlyRate, double taxRate, string address, string contact)
        {
            return new ITDepartment(companyName, employees, hoursPerMonth, hourlyRate, taxRate, address, contact);
        }
    }

    // Фабрика для финансового отдела
    public class FinanceDepartmentFactory : IHRDepartmentFactory
    {
        public HRDepartment CreateHRDepartment(string companyName, int employees, double hoursPerMonth, decimal hourlyRate, double taxRate, string address, string contact)
        {
            return new FinanceDepartment(companyName, employees, hoursPerMonth, hourlyRate, taxRate, address, contact);
        }
    }
}