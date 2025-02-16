using System;

namespace OOP_LB1
{
    public class HRDepartment
    {
        private static int instanceCount = 0;

        public string CompanyName { get; set; }
        public int Employees { get; set; }
        public double HoursPerMonth { get; set; }
        public decimal HourlyRate { get; set; }  
        public double TaxRate { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }

        public HRDepartment()
        {
            instanceCount++;
        }

        public HRDepartment(string companyName) : this()
        {
            CompanyName = companyName;
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
        }

        // Перегрузка ToString для вывода информации о департаменте
        public override string ToString()
        {
            return $"Компания: {CompanyName}\nРаботников: {Employees}\nНорма часов: {HoursPerMonth}\nСтавка: {HourlyRate:C}\nНалог: {TaxRate}%\nАдрес: {Address}\nКонтакт: {Contact}";
        }

        // Метод для получения количества сотрудников в шестнадцатеричном формате
        public string GetEmployeesHex() => Employees.ToString("X");

        // Статический метод для получения количества созданных экземпляров
        public static int GetInstanceCount() => instanceCount;

        // Метод для расчёта заработной платы сотрудника
        public decimal CalculateSalary()
        {
            decimal grossSalary = (decimal)HoursPerMonth * HourlyRate;
            return grossSalary - (grossSalary * (decimal)TaxRate / 100);
        }
        // Метод для изменения полей на основе пользовательского ввода
        public void UpdateFields()
        { 
        }

        }

}
