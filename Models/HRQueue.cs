using System;
using System.Collections.Generic;
using HRProject.Factories;

namespace HRProject.Models
{
    public delegate void HRQueueEventHandler(string message);

    public class HRQueue
    {
        private Queue<HRDepartment> departments = new Queue<HRDepartment>();
        private readonly IHRDepartmentFactory factory;

        public event HRQueueEventHandler ItemAdded;
        public event HRQueueEventHandler ItemUpdated;

        // Конструктор
        public HRQueue(IHRDepartmentFactory factory)
        {
            this.factory = factory;
        }

        // Метод добавления отдела
        public void Enqueue(string companyName, int employees, double hoursPerMonth, decimal hourlyRate, double taxRate, string address, string contact)
        {
            // Создание нового отдела с использованием фабрики
            HRDepartment department = factory.CreateHRDepartment(companyName, employees, hoursPerMonth, hourlyRate, taxRate, address, contact);

            // Добавление отдела в очередь
            departments.Enqueue(department);

            // Вызываем событие добавления нового отдела
            ItemAdded?.Invoke($"Добавлен отдел: {department.CompanyName}");
        }

        // Метод для обновления информации об отделе
        public void UpdateDepartment(HRDepartment department, int employees, double hours, decimal rate, double tax, string address, string contact)
        {
            department.UpdateFields(employees, hours, rate, tax, address, contact);
            ItemUpdated?.Invoke($"Обновлен отдел: {department.CompanyName}");
        }

        // Получение всех отделов
        public IEnumerable<HRDepartment> GetDepartments() => departments;
    }
}
