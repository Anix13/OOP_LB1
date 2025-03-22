using System;
using System.Collections.Generic;
using System.Linq;
using HRProject.Factories;
using HRProject.Models.HRProject.Models;

namespace HRProject.Models
{
    // Определение делегата
    public delegate void HRQueueEventHandler(string message);

    public class HRQueue
    {
        private Queue<HRDepartment> departments = new Queue<HRDepartment>();
        private readonly IHRDepartmentFactory defaultFactory;

        public event HRQueueEventHandler ItemAdded;
        public event HRQueueEventHandler ItemUpdated;

        public HRQueue(IHRDepartmentFactory defaultFactory)
        {
            this.defaultFactory = defaultFactory;
        }

        public void Enqueue(string companyName, int employees, double hoursPerMonth, decimal hourlyRate, double taxRate, string address, string contact)
        {
            IHRDepartmentFactory factory = defaultFactory;

            // Выбор фабрики в зависимости от названия компании
            if (companyName.IndexOf("ИТ", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                factory = new ITDepartmentFactory(); // Создаем ITDepartment
            }
            else
            {
                factory = new FinanceDepartmentFactory(); // Создаем FinanceDepartment
            }

            // Создание объекта через фабрику
            HRDepartment department = factory.CreateHRDepartment(companyName, employees, hoursPerMonth, hourlyRate, taxRate, address, contact);
            departments.Enqueue(department);

            // Определяем тип отдела и формируем сообщение
            string departmentType = department is ITDepartment ? "ИТ" : "финансовый";
            ItemAdded?.Invoke($"Добавлен {departmentType} отдел: {department.CompanyName}");
        }

        public void UpdateDepartment(HRDepartment department, int employees, double hours, decimal rate, double tax, string address, string contact)
        {
            department.UpdateFields(employees, hours, rate, tax, address, contact);
            string departmentType = department is ITDepartment ? "ИТ" : "финансовый";
            ItemUpdated?.Invoke($"Обновлен {departmentType} отдел: {department.CompanyName}");
        }

        public IEnumerable<HRDepartment> GetDepartments() => departments;
    }
}