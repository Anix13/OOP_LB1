using OOP_LB1.Controllers;
using OOP_LB1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OOP_LB1.Views
{
    public class ConsoleHRView
    {
        private readonly HRController _controller;
        private HRDepartment _selectedDepartment;

        public ConsoleHRView(HRController controller)
        {
            _controller = controller;

            // Подписываемся на события контроллера
            _controller.DepartmentsUpdated += OnDepartmentsUpdated;
            _controller.StatusChanged += OnStatusChanged;
            _controller.ErrorOccurred += OnErrorOccurred;
        }

        public void Run()
        {

            ShowMainMenu();
        }
        private void SafeClearConsole()
        {
            try
            {
                Console.Clear();
            }
            catch (IOException)
            {
                // Игнорируем ошибку, если консоли нет
            }
            catch (System.Security.SecurityException)
            {
                // На случай проблем с правами
            }
        }
        private void ShowMainMenu()
        {
            while (true)
            {
                SafeClearConsole();
                Console.WriteLine("=== HR Management System ===");
                Console.WriteLine("1. Создать новый отдел");
                Console.WriteLine("2. Обновить отдел");
                Console.WriteLine("3. Изменить состояние отдела");
                Console.WriteLine("4. Просмотреть все отделы");
                Console.WriteLine("5. Выход");

                Console.Write("\nВыберите действие: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowCreateDepartmentMenu();
                        break;
                    case "2":
                        ShowUpdateDepartmentMenu();
                        break;
                    case "3":
                        ShowChangeStateMenu();
                        break;
                    case "4":
                        DisplayAllDepartments();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ShowCreateDepartmentMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Создание нового отдела ===");

            Console.Write("Название компании: ");
            string name = Console.ReadLine();

            Console.Write("Количество сотрудников: ");
            int employees = int.Parse(Console.ReadLine());

            Console.Write("Часов в месяц: ");
            double hours = double.Parse(Console.ReadLine());

            Console.Write("Почасовая ставка: ");
            decimal rate = decimal.Parse(Console.ReadLine());

            Console.Write("Налоговая ставка (%): ");
            double tax = double.Parse(Console.ReadLine());

            Console.Write("Адрес: ");
            string address = Console.ReadLine();

            Console.Write("Контакт: ");
            string contact = Console.ReadLine();

            _controller.CreateDepartment(name, employees, hours, rate, tax, address, contact);
            Console.ReadKey();
        }

        private void ShowUpdateDepartmentMenu()
        {
            Console.Clear();
            var departments = _controller.GetDepartments().ToList();
            DisplayAllDepartments(departments);

            Console.Write("\nВведите номер отдела для обновления: ");
            int index = int.Parse(Console.ReadLine()) - 1;

            if (index < 0 || index >= departments.Count)
            {
                Console.WriteLine("Неверный номер отдела!");
                Console.ReadKey();
                return;
            }

            _selectedDepartment = departments[index];

            Console.WriteLine($"\nОбновление отдела: {_selectedDepartment.CompanyName}");

            Console.Write("Количество сотрудников: ");
            int employees = int.Parse(Console.ReadLine());

            Console.Write("Часов в месяц: ");
            double hours = double.Parse(Console.ReadLine());

            Console.Write("Почасовая ставка: ");
            decimal rate = decimal.Parse(Console.ReadLine());

            Console.Write("Налоговая ставка (%): ");
            double tax = double.Parse(Console.ReadLine());

            Console.Write("Адрес: ");
            string address = Console.ReadLine();

            Console.Write("Контакт: ");
            string contact = Console.ReadLine();

            _controller.UpdateDepartment(_selectedDepartment, employees, hours, rate, tax, address, contact);
            Console.ReadKey();
        }

        private void ShowChangeStateMenu()
        {
            Console.Clear();
            var departments = _controller.GetDepartments().ToList();
            DisplayAllDepartments(departments);

            Console.Write("\nВведите номер отдела для изменения состояния: ");
            int index = int.Parse(Console.ReadLine()) - 1;

            if (index < 0 || index >= departments.Count)
            {
                Console.WriteLine("Неверный номер отдела!");
                Console.ReadKey();
                return;
            }

            _selectedDepartment = departments[index];

            Console.WriteLine("\nВыберите новое состояние:");
            Console.WriteLine("1. Активный");
            Console.WriteLine("2. Пассивный");
            Console.Write("Выбор: ");
            var choice = Console.ReadLine();

            IHRState newState = choice == "1" ? new ActiveState() : new PassiveState();
            _controller.ChangeState(_selectedDepartment, newState);
            Console.ReadKey();
        }

        private void DisplayAllDepartments()
        {
            var departments = _controller.GetDepartments().ToList();
            DisplayAllDepartments(departments);
        }

        private void DisplayAllDepartments(List<HRDepartment> departments)
        {
            Console.WriteLine("Список отделов:");
            Console.WriteLine("====================================================================");
            Console.WriteLine("| # | Название          | Сотрудники | Статус    | Зарплата       |");
            Console.WriteLine("====================================================================");

            for (int i = 0; i < departments.Count; i++)
            {
                var dept = departments[i];
                Console.WriteLine($"| {i + 1,1} | {dept.CompanyName,-17} | {dept.Employees,10} | {dept.CurrentStatus,-9} | {dept.CalculateSalary(),13:C2} |");
            }

            Console.WriteLine("====================================================================");
        }

        private void OnDepartmentsUpdated(IEnumerable<HRDepartment> departments)
        {
            Console.Clear();
            DisplayAllDepartments(departments.ToList());
        }

        private void OnStatusChanged(string message)
        {
            Console.WriteLine($"\nСтатус: {message}");
        }

        private void OnErrorOccurred(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nОшибка: {message}");
            Console.ResetColor();
        }
    }
}