using System;
using System.Collections.Generic;
using HRProject.Factories;
using HRProject.Models;
using HRProject.Models.HRProject.Models;

namespace HRProject.Models
{
    // Определение делегата
    public delegate void HRQueueEventHandler(string message);

    public class HRQueue
    {
        private Queue<IHRDepartmentAdapter> departments = new Queue<IHRDepartmentAdapter>();
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

            // Создание объекта через фабрику и адаптер
            IHRDepartmentAdapter department = factory.CreateHRDepartment(companyName, employees, hoursPerMonth, hourlyRate, taxRate, address, contact);
            departments.Enqueue(department);

            // Определяем тип отдела и формируем сообщение
            string departmentType = department is ITDepartment ? "ИТ" : "финансовый";
            ItemAdded?.Invoke($"Добавлен {departmentType} отдел: {department.GetCompanyInfo()}");
        }

        // В методе UpdateDepartment или других методах, где вы пытаетесь обратиться к адресу, используйте метод GetAddress().
        public void UpdateDepartment(IHRDepartmentAdapter department, int employees, double hours, decimal rate, double tax, string address, string contact)
        {
            // Обновление данных через адаптер
            department.UpdateFields(employees, hours, rate, tax, address, contact);

            // Получение адреса через метод адаптера
            string departmentAddress = department.GetAddress();
            string departmentType = department is ITDepartment ? "ИТ" : "финансовый";
            ItemUpdated?.Invoke($"Обновлен {departmentType} отдел: {department.GetCompanyInfo()}, Адрес: {departmentAddress}");
        }

        public IEnumerable<IHRDepartmentAdapter> GetDepartments() => departments;
    }
}