using OOP_LB1.Models;
using OOP_LB1.Models.Exceptions;
using System;

namespace OOP_LB1.Models
{
    public class HRDepartment
    {
        public static int Count { get; private set; }
        private IHRState _currentState;

        public string CompanyName { get; set; }
        public int Employees { get; set; }
        public double HoursPerMonth { get; set; }
        public decimal HourlyRate { get; set; }
        public double TaxRate { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public decimal GrossSalary { get; set; }
        public string CurrentStatus => _currentState.GetStatus();

        public HRDepartment()
        {
            Count++;
            _currentState = new ActiveState();
        }

        public void SetState(IHRState newState)
        {
            _currentState = newState;
            _currentState.Handle(this);
        }
        public override string ToString()
        {
            return CompanyName;
        }
        public decimal CalculateSalary()
        {
            if (!_currentState.CanCalculateSalary)
                throw new InvalidOperationException($"Расчет зарплаты невозможен в состоянии: {_currentState.GetStatus()}");

            if (Employees <= 0 || HoursPerMonth <= 0 || HourlyRate <= 0)
                throw new CustomException("Невозможно вычислить зарплату", "HR001", "Неположительные значения");

            decimal grossSalary = (decimal)HoursPerMonth * HourlyRate;

            if (grossSalary > 100000000 || grossSalary < 0)
                throw new SalaryOverflowException("Переполнение при расчете", "HR002", "Зарплата слишком велика");

            return Math.Abs(grossSalary - (grossSalary * (decimal)TaxRate / 100));
        }
    }
}