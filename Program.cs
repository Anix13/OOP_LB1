using OOP_LB1.Controllers;
using OOP_LB1.Models;
using OOP_LB1.Views;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace OOP_LB1
{
    static class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeConsole();

        [STAThread]
        static void Main()
        {
            // Сначала выделяем консоль, чтобы можно было читать ввод
            AllocConsole();

            var model = new HRQueue();
            var controller = new HRController(model);

            Console.WriteLine("Выберите интерфейс:");
            Console.WriteLine("1. Консоль");
            Console.WriteLine("2. Графический интерфейс (WinForms)");
            Console.Write("\nВаш выбор: ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                var consoleView = new ConsoleHRView(controller);
                consoleView.Run();
            }
            else if (choice == "2")
            {
                // Освобождаем консоль, чтобы не висело пустое окно
                FreeConsole();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var formView = new HRForm(controller);
                Application.Run(formView);
            }
            else
            {
                Console.WriteLine("Неверный выбор. Завершение программы...");
                Console.ReadKey(); // Чтобы окно консоли не закрылось мгновенно
            }
        }
    }
}
