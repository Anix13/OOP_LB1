using HRProject.Models;
using OOP_LB1;

namespace HRProject.Factories
{
    public class HRDepartmentFactory : IHRDepartmentFactory
    {
        public HRDepartment CreateHRDepartment(string companyName, int employees, double hoursPerMonth, decimal hourlyRate, double taxRate, string address, string contact)
        {
            return new HRDepartment(companyName, employees, hoursPerMonth, hourlyRate, taxRate, address, contact);
        }
    }
}
