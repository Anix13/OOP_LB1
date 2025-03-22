using HRProject.Models;

namespace HRProject.Factories
{
    public interface IHRDepartmentFactory
    {
        HRDepartment CreateHRDepartment(string companyName, int employees, double hoursPerMonth, decimal hourlyRate, double taxRate, string address, string contact);
    }
}
