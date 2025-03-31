using HRProject.Models;
using HRProject.Models.HRProject.Models;

namespace HRProject.Factories
{

    public class HRDepartmentFactory : IHRDepartmentFactory
    {
        public IHRDepartmentAdapter CreateHRDepartment(string companyName, int employees, double hoursPerMonth, decimal hourlyRate, double taxRate, string address, string contact)
        {
            return new HRDepartmentAdapter(new HRDepartment(companyName, employees, hoursPerMonth, hourlyRate, taxRate, address, contact));
        }
    }
    // Фабрика для IT-отдела
    public class ITDepartmentFactory : IHRDepartmentFactory
    {
        public IHRDepartmentAdapter CreateHRDepartment(string companyName, int employees, double hoursPerMonth, decimal hourlyRate, double taxRate, string address, string contact)
        {
            return new HRDepartmentAdapter(new ITDepartment(companyName, employees, hoursPerMonth, hourlyRate, taxRate, address, contact));
        }
    }

    // Фабрика для финансового отдела
    public class FinanceDepartmentFactory : IHRDepartmentFactory
    {
        public IHRDepartmentAdapter CreateHRDepartment(string companyName, int employees, double hoursPerMonth, decimal hourlyRate, double taxRate, string address, string contact)
        {
            return new HRDepartmentAdapter(new FinanceDepartment(companyName, employees, hoursPerMonth, hourlyRate, taxRate, address, contact));
        }
    }
}