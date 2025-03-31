using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using HRProject.Models;
using HRProject.Factories;
using System.Collections;

namespace OOP_LB1
{
    public partial class HRForm : Form
    {
        private IHRDepartmentFactory factory;
        internal HRQueue hrQueue;
        internal ComboBox cmbDepartments;
        internal TextBox txtCompanyName, txtEmployees, txtHours, txtRate, txtTax, txtAddress, txtContact;
        internal TextBox txtUpdateEmployees, txtUpdateHours, txtUpdateRate, txtUpdateTax, txtUpdateAddress, txtUpdateContact;
        internal DataGridView dgvDepartments;
        private TabPage performancePage;
        private ListView lstPerformanceResults;




        public HRForm()
        {
            factory = new HRDepartmentFactory();
            hrQueue = new HRQueue(factory);

            InitializeComponent();
            InitializeFormComponents();
            InitializePerformanceTab();


            hrQueue.ItemAdded += msg => MessageBox.Show(msg, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            hrQueue.ItemUpdated += msg => MessageBox.Show(msg, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void InitializeFormComponents()
        {
            string message = "Вариант 7:  Отдел кадров\n" +
                             "23ВП2\n" +
                             "Тареева Мирошниченко";

            MessageBox.Show(message, "Лабораторная работа номер ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Text = "HR Department Management";
            this.Size = new System.Drawing.Size(1000, 850);
            this.BackColor = System.Drawing.Color.LightSkyBlue;

            TabControl tabControl = new TabControl { Dock = DockStyle.Fill, BackColor = System.Drawing.Color.AliceBlue };
            TabPage createPage = new TabPage("Создать") { BackColor = System.Drawing.Color.LightSteelBlue };
            TabPage updatePage = new TabPage("Обновить") { BackColor = System.Drawing.Color.LightSteelBlue };
            TabPage viewPage = new TabPage("Просмотр") { BackColor = System.Drawing.Color.LightSteelBlue };

            dgvDepartments = new DataGridView { Dock = DockStyle.Fill };
            cmbDepartments = new ComboBox { Width = 250 };

            createPage.Controls.Add(CreateDepartmentPanel());
            updatePage.Controls.Add(UpdateDepartmentPanel());
            viewPage.Controls.Add(CreateViewPanel());

            tabControl.TabPages.AddRange(new TabPage[] { createPage, updatePage, viewPage });
            this.Controls.Add(tabControl);
        }

        private void InitializePerformanceTab()
        {
            performancePage = new TabPage("Тест производительности") { BackColor = System.Drawing.Color.LightSteelBlue };

            lstPerformanceResults = new ListView
            {
                Size = new System.Drawing.Size(1000, 150),  // Фиксированный размер для таблицы
                View = View.Details,
                FullRowSelect = true,
                Font = new System.Drawing.Font("Arial", 12) , // Устанавливаем размер шрифта для текста в таблице
                BorderStyle = BorderStyle.FixedSingle
            };

            // Добавление заголовков столбцов
            lstPerformanceResults.Columns.Add("Операция", 400);  // Устанавливаем ширину первого столбца в 150 пикселей
            lstPerformanceResults.Columns.Add("Массив (мс)", 150);
            lstPerformanceResults.Columns.Add("Очередь (мс)", 150);

            performancePage.Controls.Add(lstPerformanceResults);

            // Создаем панель для кнопки
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Location = new System.Drawing.Point(350, 250),  // Кнопка будет на координатах (150, 250), по центру
                Size = new System.Drawing.Size(60, 40),  // Размер кнопки
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true
            };

            // Кнопка для запуска теста производительности
            Button btnStartPerformanceTest = new Button
            {
                Text = "Запустить тест производительности",
                Width = 250,
                Height = 50,  // Увеличиваем высоту кнопки
                Font = new System.Drawing.Font("Arial", 12),  // Увеличиваем размер шрифта кнопки
            };

            // Обработчик нажатия кнопки
            btnStartPerformanceTest.Click += BtnStartPerformanceTest_Click;

            // Добавляем кнопку в панель
            buttonPanel.Controls.Add(btnStartPerformanceTest);

            // Добавляем панель с кнопкой в вкладку
            performancePage.Controls.Add(buttonPanel);

            // Добавление таба на TabControl
            TabControl tabControl = this.Controls.OfType<TabControl>().FirstOrDefault();
            tabControl.TabPages.Add(performancePage);
        }


        private FlowLayoutPanel CreateDepartmentPanel()
        {
            FlowLayoutPanel panel = CreateBasePanel();
            Label lblCompanyName = new Label { Width = 250, Text = "Название компании" };
            Label lblEmployees = new Label { Width = 250, Text = "Кол-во сотрудников" };
            Label lblHours = new Label { Width = 250, Text = "Часы в месяц" };
            Label lblRate = new Label { Width = 250, Text = "Почасовая ставка" };
            Label lblTax = new Label { Width = 250, Text = "Налог" };
            Label lblAddress = new Label { Width = 250, Text = "Адрес" };
            Label lblContact = new Label { Width = 250, Text = "Контакт" };

            txtCompanyName = new TextBox { Width = 250 };
            txtEmployees = new TextBox { Width = 250 };
            txtHours = new TextBox { Width = 250 };
            txtRate = new TextBox { Width = 250 };
            txtTax = new TextBox { Width = 250 };
            txtAddress = new TextBox { Width = 250 };
            txtContact = new TextBox { Width = 250 };

            Button btnCreate = new Button { Text = "Создать", Width = 250 };
            btnCreate.Click += BtnCreate_Click;



            panel.Controls.AddRange(new Control[] { lblCompanyName, txtCompanyName, lblEmployees, txtEmployees, lblHours, txtHours, lblRate, txtRate, lblTax,
                txtTax, lblAddress, txtAddress, lblContact, txtContact, btnCreate });
            return panel;
        }

        private FlowLayoutPanel UpdateDepartmentPanel()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            cmbDepartments = new ComboBox { Width = 250 };
            cmbDepartments.SelectedIndexChanged += CmbDepartments_SelectedIndexChanged;
            Label lblCompanyName = new Label { Width = 250, Text = "Название компании:" };
            Label lblEmployees = new Label { Width = 250, Text = "Кол-во сотрудников:" };
            Label lblHours = new Label { Width = 250, Text = "Часы в месяц:" };
            Label lblRate = new Label { Width = 250, Text = "Почасовая ставка:" };
            Label lblTax = new Label { Width = 250, Text = "Налог:" };
            Label lblAddress = new Label { Width = 250, Text = "Адрес:" };
            Label lblContact = new Label { Width = 250, Text = "Контакт:" };

            txtUpdateEmployees = new TextBox { Width = 250 };
            txtUpdateHours = new TextBox { Width = 250 };
            txtUpdateRate = new TextBox { Width = 250 };
            txtUpdateTax = new TextBox { Width = 250 };
            txtUpdateAddress = new TextBox { Width = 250 };
            txtUpdateContact = new TextBox { Width = 250 };

            Button btnUpdate = new Button { Text = "Обновить", Width = 250 };
            btnUpdate.Click += BtnUpdate_Click;

            panel.Controls.AddRange(new Control[] { lblCompanyName, cmbDepartments, lblEmployees, txtUpdateEmployees, lblHours, txtUpdateHours,
                lblRate, txtUpdateRate, lblTax, txtUpdateTax, lblAddress, txtUpdateAddress, lblContact, txtUpdateContact, btnUpdate });
            return panel;
        }

        private FlowLayoutPanel CreateBasePanel()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(350, 100, 5, 5),
                WrapContents = false
            };
        }

        private Control CreateViewPanel()
        {
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Название компании", DataPropertyName = "CompanyName", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Кол-во сотрудников", DataPropertyName = "Employees", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Часы в месяц", DataPropertyName = "HoursPerMonth", Width = 100 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Почасовая ставка", DataPropertyName = "HourlyRate", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Налог", DataPropertyName = "TaxRate", Width = 100 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Адрес", DataPropertyName = "Address", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Контакт", DataPropertyName = "Contact", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Средняя зп сотрудника", DataPropertyName = "GrossSalary", Width = 150 });

            UpdateDataGridView();
            return dgvDepartments;
        }

        internal void BtnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
                {
                    MessageBox.Show("Введите название компании");
                    return;
                }

                // Changed to use GetCompanyInfo() instead of CompanyName
                if (hrQueue.GetDepartments().Any(d => d.GetCompanyInfo().Equals(txtCompanyName.Text, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("Компания с таким названием уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string contact = txtContact.Text;
                string phonePattern = @"^\+?\d{1,4}?[-.\s]?(\(?\d{1,3}?\)?[-.\s]?)?\d{1,3}[-.\s]?\d{1,3}[-.\s]?\d{1,4}$";
                if (!Regex.IsMatch(contact, phonePattern))
                {
                    MessageBox.Show("Введите корректный телефонный номер.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                hrQueue.Enqueue(
                    txtCompanyName.Text,
                    int.TryParse(txtEmployees.Text, out int emp) ? emp : 0,
                    double.TryParse(txtHours.Text, out double hours) ? hours : 0,
                    decimal.TryParse(txtRate.Text, out decimal rate) ? rate : 0,
                    double.TryParse(txtTax.Text, out double tax) ? tax : 0,
                    txtAddress.Text,
                    txtContact.Text
                );

                UpdateDepartmentList();
            }
            catch (Exception ex)
            {
                HRDepartment.HandleException(ex);
            }
        }

        internal void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbDepartments.SelectedItem is IHRDepartmentAdapter selectedAdapter)
                {
                    if (!int.TryParse(txtUpdateEmployees.Text, out int updatedEmployees) || updatedEmployees < 0 ||
                        !double.TryParse(txtUpdateHours.Text, out double updatedHours) || updatedHours < 0 ||
                        !decimal.TryParse(txtUpdateRate.Text, out decimal updatedRate) || updatedRate < 0 ||
                        !double.TryParse(txtUpdateTax.Text, out double updatedTax) || updatedTax < 0 ||
                        !Regex.IsMatch(txtUpdateContact.Text, @"^\+?\d{1,4}?[-.\s]?(\(?\d{1,3}?\)?[-.\s]?)?\d{1,3}[-.\s]?\d{1,3}[-.\s]?\d{1,4}$"))
                    {
                        MessageBox.Show("Введите корректные данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    selectedAdapter.UpdateFields(
                        updatedEmployees,
                        updatedHours,
                        updatedRate,
                        updatedTax,
                        txtUpdateAddress.Text,
                        txtUpdateContact.Text
                    );

                    UpdateDepartmentList();
                }
            }
            catch (Exception ex)
            {
                HRDepartment.HandleException(ex);
            }
        }


        private void CmbDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDepartments.SelectedItem is IHRDepartmentAdapter selectedAdapter)
            {
                txtUpdateEmployees.Text = selectedAdapter.GetEmployees().ToString();
                txtUpdateHours.Text = selectedAdapter.GetHoursPerMonth().ToString();
                txtUpdateRate.Text = selectedAdapter.GetHourlyRate().ToString();
                txtUpdateTax.Text = selectedAdapter.GetTaxRate().ToString();
                txtUpdateAddress.Text = selectedAdapter.GetAddress();
                txtUpdateContact.Text = selectedAdapter.GetContact();
            }
        }

        internal void UpdateDepartmentList()
        {
            if (cmbDepartments == null || dgvDepartments == null) return;

            cmbDepartments.Items.Clear();
            foreach (var dept in hrQueue.GetDepartments())
            {
                cmbDepartments.Items.Add(dept);
            }
            cmbDepartments.DisplayMember = "CompanyName";

            UpdateDataGridView();
        }

        private void UpdateDataGridView()
        {
            dgvDepartments.Rows.Clear();
            foreach (var adapter in hrQueue.GetDepartments())
            {
                dgvDepartments.Rows.Add(
                    adapter.GetCompanyInfo(),  // Changed from CompanyName
                    adapter.GetEmployees(),   // Changed from Employees
                    adapter.GetHoursPerMonth(), // Changed from HoursPerMonth
                    adapter.GetHourlyRate(),   // Changed from HourlyRate
                    adapter.GetTaxRate(),     // Changed from TaxRate
                    adapter.GetAddress(),      // Changed from Address
                    adapter.GetContact(),     // Changed from Contact
                    adapter.CalculateSalary()  // This remains the same
                );
            }
        }
        private void PerformPerformanceTest()
        {
            int count = 500000;
            lstPerformanceResults.Items.Clear();

            var stopwatch = new Stopwatch();
            var departments = GenerateDepartments(count);

            // Вставка в HRQueue
            stopwatch.Start();
            var factory = new HRDepartmentFactory();
            var queue = new HRQueue(factory);
            foreach (var dept in departments)
            {
                queue.Enqueue(dept.CompanyName, dept.Employees, dept.HoursPerMonth, dept.HourlyRate, dept.TaxRate, dept.Address, dept.Contact);
            }
            stopwatch.Stop();
            long queueInsertTime = stopwatch.ElapsedMilliseconds;

            // Вставка в массив адаптеров
            stopwatch.Reset();
            stopwatch.Start();
            var adapterArray = new IHRDepartmentAdapter[count];
            for (int i = 0; i < count; i++)
            {
                adapterArray[i] = new HRDepartmentAdapter(departments[i]);
            }
            stopwatch.Stop();
            long arrayInsertTime = stopwatch.ElapsedMilliseconds;

            lstPerformanceResults.Items.Add(new ListViewItem(new[] { "Вставка", arrayInsertTime.ToString(), queueInsertTime.ToString() }));

            // Выборка по порядку из HRQueue
            stopwatch.Reset();
            stopwatch.Start();
            foreach (var adapter in queue.GetDepartments())
            {
                var dummy = adapter.GetCompanyInfo(); // Changed from CompanyName to GetCompanyInfo()
            }
            stopwatch.Stop();
            long queueSelectSequentialTime = stopwatch.ElapsedMilliseconds;

            // Выборка по порядку из массива адаптеров
            stopwatch.Reset();
            stopwatch.Start();
            foreach (var adapter in adapterArray)
            {
                var dummy = adapter.GetCompanyInfo(); // Changed from CompanyName to GetCompanyInfo()
            }
            stopwatch.Stop();
            long arraySelectSequentialTime = stopwatch.ElapsedMilliseconds;

            lstPerformanceResults.Items.Add(new ListViewItem(new[] { "Выборка по порядку", arraySelectSequentialTime.ToString(), queueSelectSequentialTime.ToString() }));

            // Выборка случайным порядком для HRQueue
            stopwatch.Reset();
            stopwatch.Start();
            var random = new Random();
            foreach (var adapter in queue.GetDepartments().OrderBy(x => random.Next()))
            {
                var dummy = adapter.GetCompanyInfo(); // Changed from CompanyName to GetCompanyInfo()
            }
            stopwatch.Stop();
            long queueSelectRandomTime = stopwatch.ElapsedMilliseconds;

            // Выборка случайным порядком для массива адаптеров
            stopwatch.Reset();
            stopwatch.Start();
            foreach (var adapter in adapterArray.OrderBy(x => random.Next()))
            {
                var dummy = adapter.GetCompanyInfo(); // Changed from CompanyName to GetCompanyInfo()
            }
            stopwatch.Stop();
            long arraySelectRandomTime = stopwatch.ElapsedMilliseconds;

            lstPerformanceResults.Items.Add(new ListViewItem(new[] { "Выборка случайным порядком", arraySelectRandomTime.ToString(), queueSelectRandomTime.ToString() }));
        }


        // Генерация элементов
        private List<HRDepartment> GenerateDepartments(int count)
        {
            var departments = new List<HRDepartment>();
            for (int i = 0; i < count; i++)
            {
                departments.Add(new HRDepartment(
                    $"Company {i}",
                    100,
                    160,
                    15m,
                    20,
                    "Some address",
                    "+1234567890"
                ));
            }
            return departments;
        }

        private void BtnStartPerformanceTest_Click(object sender, EventArgs e)
        {
            // Запуск теста производительности
            PerformPerformanceTest();
        }
    }
}
