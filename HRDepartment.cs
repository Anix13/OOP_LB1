using System;
using System.Net;

namespace OOP_LB1
{
    public class HRDepartment
    {
        public static int Count { get; private set; }

        public string CompanyName { get; set; }
        public int Employees { get; set; }
        public double HoursPerMonth { get; set; }
        public decimal HourlyRate { get; set; }  
        public double TaxRate { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public decimal GrossSalary {  get; set; }

        public HRDepartment()
        {
            Count++;
        }

        public HRDepartment(string companyName) : this()
        {
            CompanyName = companyName;
            Employees = 0;
            HoursPerMonth = 0;
            HourlyRate = 0;
            TaxRate = 0;
            Address = "-";
            Contact = "-";
            GrossSalary = 0;
        }

        public HRDepartment(string companyName, int employees) : this(companyName)
        {
            Employees = employees;
        }

        public HRDepartment(string companyName, int employees, double hoursPerMonth, decimal hourlyRate, double taxRate, string address, string contact) : this(companyName, employees)
        {
            HoursPerMonth = hoursPerMonth;
            HourlyRate = hourlyRate;
            TaxRate = taxRate;
            Address = address;
            Contact = contact;
            GrossSalary = CalculateSalary();
        }

        // Перегрузка ToString для вывода информации о департаменте
        public override string ToString()
        {
            return $"Компания: {CompanyName}\nРаботников: {Employees}\nНорма часов: {HoursPerMonth}\nСтавка: {HourlyRate:C}\nНалог: {TaxRate}%\nАдрес: {Address}\nКонтакт: {Contact}";
        }

        // Метод для получения количества сотрудников в шестнадцатеричном формате
        public string GetEmployeesHex() => Employees.ToString("X");


        public decimal CalculateSalary()
        {
            if (Employees <= 0 || HoursPerMonth <= 0 || HourlyRate <= 0)
            {
                throw new CustomException("Невозможно вычислить зарплату из-за отрицательных значений.",
                                           "HR001", "Введены неположительные значения для сотрудников, часов, ставки или налога.");
            }

            decimal grossSalary = (decimal)HoursPerMonth * HourlyRate;

            // Проверка на переполнение
            if ((grossSalary > 100000000) || (grossSalary < 0))
            {
                throw new SalaryOverflowException("Переполнение при расчете зарплаты.",
                                                  "HR002", "Зарплата слишком велика для обработки. Слишком большие значения почасовой ставки или кол-ва часов");
            }

            return Math.Abs(grossSalary - (grossSalary * (decimal)TaxRate / 100));
        }

    

    // Метод для изменения полей на основе пользовательского ввода
    public void UpdateFields(int employees, double hoursPerMonth, decimal hourlyRate, double taxRate, string address, string contact)
        {
            Employees = employees;
            HoursPerMonth = hoursPerMonth;
            HourlyRate = hourlyRate;
            TaxRate = taxRate;
            Address = address;
            Contact = contact;

            // Если зарплата зависит от этих значений, пересчитываем её.
            GrossSalary = CalculateSalary();
        }



        // Метод для обработки исключений
        public static void HandleException(Exception ex)
        {
            if (ex is CustomException customEx)
            {
                MessageBoxHelper.ShowMessage(customEx.ToString(), "Ошибка в расчете зарплаты");
            }
            else if (ex is SalaryOverflowException overflowEx)
            {
                MessageBoxHelper.ShowMessage(overflowEx.ToString(), "Ошибка переполнения");
            }
            else
            {
                MessageBoxHelper.ShowMessage(ex.Message, "Ошибка");
            }
        }

    }


}
