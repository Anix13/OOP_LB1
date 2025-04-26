using OOP_LB1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OOP_LB1.Controllers
{
    public class HRController
    {
        private readonly HRQueue _model;

        public event Action<IEnumerable<HRDepartment>> DepartmentsUpdated;
        public event Action<string> StatusChanged;
        public event Action<string> ErrorOccurred;

        public HRController(HRQueue model)
        {
            _model = model;
        }

        public void CreateDepartment(string name, int employees, double hours,
    decimal rate, double tax, string address, string contact)
        {
            try
            {
                // Проверка на уникальность названия
                if (_model.GetDepartments().Any(d => d.CompanyName.Equals(name, StringComparison.OrdinalIgnoreCase)))
                {
                    ErrorOccurred?.Invoke($"Отдел с названием '{name}' уже существует!");
                    return;
                }

                var department = new HRDepartment
                {
                    CompanyName = name,
                    Employees = employees,
                    HoursPerMonth = hours,
                    HourlyRate = rate,
                    TaxRate = tax,
                    Address = address,
                    Contact = contact
                };

                _model.Enqueue(department);
                DepartmentsUpdated?.Invoke(_model.GetDepartments());
                StatusChanged?.Invoke($"Отдел '{name}' успешно создан!");
                MessageBox.Show($"Отдел '{name}' успешно создан!", "Успех",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke($"Ошибка при создании: {ex.Message}");
            }
        }
    
        public void UpdateDepartment(HRDepartment department, int employees, double hours,
            decimal rate, double tax, string address, string contact)
        {
            if (department.CurrentStatus == "Пассивный")
            {
                ErrorOccurred?.Invoke("Нельзя изменить пассивный отдел!");
                return;
            }

            try
            {
                department.Employees = employees;
                department.HoursPerMonth = hours;
                department.HourlyRate = rate;
                department.TaxRate = tax;
                department.Address = address;
                department.Contact = contact;

                DepartmentsUpdated?.Invoke(_model.GetDepartments());
                StatusChanged?.Invoke($"Данные отдела '{department.CompanyName}' обновлены!");
                MessageBox.Show($"Данные отдела '{department.CompanyName}' обновлены!",
                              "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke($"Ошибка обновления: {ex.Message}");
            }
        }

        // Изменение состояния 
        public void ChangeState(HRDepartment department, IHRState newState)
        {
            department.SetState(newState);
            string message = $"Статус отдела '{department.CompanyName}' изменен на: {newState.GetStatus()}";
            StatusChanged?.Invoke(message);
            DepartmentsUpdated?.Invoke(_model.GetDepartments());
        }

        public IEnumerable<HRDepartment> GetDepartments()
        {
            return _model.GetDepartments();
        }
    }
}