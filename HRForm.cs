using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OOP_LB1
{
    public partial class HRForm : Form
    {
        internal HRQueue hrQueue = new HRQueue();
        internal ComboBox cmbDepartments;
        internal ComboBox cmbStates;
        internal TextBox txtCompanyName, txtEmployees, txtHours, txtRate, txtTax, txtAddress, txtContact;
        internal TextBox txtUpdateEmployees, txtUpdateHours, txtUpdateRate, txtUpdateTax, txtUpdateAddress, txtUpdateContact;
        internal DataGridView dgvDepartments;
        private TabPage performancePage;
        private ListView lstPerformanceResults;
        private ToolStripStatusLabel statusLabel;

        public HRForm()
        {
            InitializeComponent(); // Должно быть ПЕРВЫМ вызовом в конструкторе
            InitializeFormComponents();
            InitializePerformanceTab();

            hrQueue = new HRQueue(); // Инициализация hrQueue перед подпиской на события

            hrQueue.ItemAdded += msg => MessageBox.Show(msg, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            hrQueue.ItemUpdated += msg => MessageBox.Show(msg, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            hrQueue.StateChanged += msg => MessageBox.Show(msg, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void InitializeFormComponents()
        {
            string message = "Вариант 7: Отдел кадров\n23ВП2\nТареева Мирошниченко";
            MessageBox.Show(message, "Лабораторная работа номер 5", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

            // Добавляем статус бар
            StatusStrip statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            statusStrip.Items.Add(statusLabel);
            this.Controls.Add(statusStrip);
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

            // Создаем элементы управления
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

            // Добавляем элементы на панель (без выбора состояния)
            panel.Controls.AddRange(new Control[] {
        txtCompanyName,lblCompanyName,
        txtEmployees, lblEmployees,
        txtHours, lblHours,
        txtRate,lblRate,
        txtTax, lblTax,
        txtAddress, lblAddress,
        txtContact, lblContact, 
        btnCreate
    });

            return panel;
        }



        private FlowLayoutPanel UpdateDepartmentPanel()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            // Создаем элементы управления
            Label lblCompanyName = new Label { Width = 250, Text = "Выберите отдел:" };
            cmbDepartments = new ComboBox { Width = 250 };
            cmbDepartments.SelectedIndexChanged += CmbDepartments_SelectedIndexChanged;

            Label lblEmployees = new Label { Width = 250, Text = "Кол-во сотрудников:" };
            Label lblHours = new Label { Width = 250, Text = "Часы в месяц:" };
            Label lblRate = new Label { Width = 250, Text = "Почасовая ставка:" };
            Label lblTax = new Label { Width = 250, Text = "Налог:" };
            Label lblAddress = new Label { Width = 250, Text = "Адрес:" };
            Label lblContact = new Label { Width = 250, Text = "Контакт:" };
            Label lblState = new Label { Width = 250, Text = "Изменить состояние:" };

            txtUpdateEmployees = new TextBox { Width = 250 };
            txtUpdateHours = new TextBox { Width = 250 };
            txtUpdateRate = new TextBox { Width = 250 };
            txtUpdateTax = new TextBox { Width = 250 };
            txtUpdateAddress = new TextBox { Width = 250 };
            txtUpdateContact = new TextBox { Width = 250 };

            // ComboBox для состояний
            cmbStates = new ComboBox { Width = 250 };
            cmbStates.Items.AddRange(new object[] {
        new ActiveState(),
        new PassiveState()
    });
            cmbStates.DisplayMember = "GetStatus";
            cmbStates.SelectedIndex = 0;

            // Кнопки
            Button btnUpdate = new Button { Text = "Обновить данные", Width = 250 };
            btnUpdate.Click += BtnUpdate_Click;

            Button btnChangeState = new Button { Text = "Изменить состояние", Width = 250 };
            btnChangeState.Click += BtnChangeState_Click;

            // Добавляем элементы на панель
            panel.Controls.AddRange(new Control[] {
        cmbDepartments, lblCompanyName,
        txtUpdateEmployees, lblEmployees,
        txtUpdateHours, lblHours,
        txtUpdateRate, lblRate,
        txtUpdateTax, lblTax,
        txtUpdateAddress, lblAddress,
        txtUpdateContact, lblContact,
        cmbStates, lblState, 
        btnChangeState,
        btnUpdate
    });

            // Заполняем ComboBox отделами
            UpdateDepartmentComboBox();

            return panel;
        }

        private void UpdateDepartmentComboBox()
        {
            cmbDepartments.Items.Clear();
            foreach (var dept in hrQueue.GetDepartments())
            {
                cmbDepartments.Items.Add(dept);
            }
            cmbDepartments.DisplayMember = "CompanyName";
            if (cmbDepartments.Items.Count > 0)
            {
                cmbDepartments.SelectedIndex = 0;
            }
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
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Статус", DataPropertyName = "CurrentStatus", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Средняя зп сотрудника", DataPropertyName = "GrossSalary", Width = 150 });

            UpdateDataGridView();
            return dgvDepartments;
        }
        private IHRState CreateStateFromStatus(string status)
        {
            switch (status)
            {
                case "Активный":
                    return new ActiveState();

                case "Пассивный":
                    return new PassiveState();
                default:
                    throw new ArgumentException($"Unknown status: {status}");
            }
        }
        private void BtnChangeState_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbDepartments.SelectedItem is HRDepartment selectedDept &&
                    cmbStates.SelectedItem is IHRState selectedState)
                {
                    // Получаем статус из выбранного состояния
                    string status = selectedState.GetStatus();

                    // Создаем новое состояние на основе статуса
                    IHRState newState;
                    switch (status)
                    {
                        case "Активный":
                            newState = new ActiveState();
                            break;
                        case "Пассивный":
                            newState = new PassiveState();
                            break;
                        default:
                            throw new ArgumentException($"Unknown status: {status}");
                    }

                    // Применяем новое состояние
                    hrQueue.ChangeDepartmentState(selectedDept, newState);

                    // Обновляем UI
                    UpdateDepartmentList();
                    statusLabel.Text = $"Текущий статус: {selectedDept.CurrentStatus}";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении состояния: {ex.Message}",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                if (hrQueue.GetDepartments().Any(d => d.CompanyName.Equals(txtCompanyName.Text, StringComparison.OrdinalIgnoreCase)))
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

                var department = new HRDepartment(
                    txtCompanyName.Text,
                    int.TryParse(txtEmployees.Text, out int emp) ? emp : 0,
                    double.TryParse(txtHours.Text, out double hours) ? hours : 0,
                    decimal.TryParse(txtRate.Text, out decimal rate) ? rate : 0,
                    double.TryParse(txtTax.Text, out double tax) ? tax : 0,
                    txtAddress.Text,
                    txtContact.Text
                );

                // Устанавливаем состояние по умолчанию (ActiveState)
                department.SetState(new ActiveState());

                hrQueue.Enqueue(department);
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
                if (cmbDepartments.SelectedItem is HRDepartment selectedDept)
                {
                    if (selectedDept.CurrentStatus == "Пассивный")
                    {
                        MessageBox.Show("Невозможно изменить данные отдела в пассивном состоянии.",
                                      "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!int.TryParse(txtUpdateEmployees.Text, out int updatedEmployees) || updatedEmployees < 0 ||
                        !double.TryParse(txtUpdateHours.Text, out double updatedHours) || updatedHours < 0 ||
                        !decimal.TryParse(txtUpdateRate.Text, out decimal updatedRate) || updatedRate < 0 ||
                        !double.TryParse(txtUpdateTax.Text, out double updatedTax) || updatedTax < 0 ||
                        !Regex.IsMatch(txtUpdateContact.Text, @"^\+?\d{1,4}?[-.\s]?(\(?\d{1,3}?\)?[-.\s]?)?\d{1,3}[-.\s]?\d{1,3}[-.\s]?\d{1,4}$"))
                    {
                        MessageBox.Show("Введите корректные данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    hrQueue.UpdateDepartment(selectedDept, updatedEmployees, updatedHours, updatedRate,
                                          updatedTax, txtUpdateAddress.Text, txtUpdateContact.Text);
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
            try
            {
                if (cmbDepartments.SelectedItem is HRDepartment selectedDept)
                {
                    // Update text fields
                    txtUpdateEmployees.Text = selectedDept.Employees.ToString();
                    txtUpdateHours.Text = selectedDept.HoursPerMonth.ToString();
                    txtUpdateRate.Text = selectedDept.HourlyRate.ToString("N2");
                    txtUpdateTax.Text = selectedDept.TaxRate.ToString("N1");
                    txtUpdateAddress.Text = selectedDept.Address;
                    txtUpdateContact.Text = selectedDept.Contact;

                    // Enable/disable fields based on status
                    bool isActive = selectedDept.CurrentStatus == "Активный";
                    txtUpdateEmployees.Enabled = isActive;
                    txtUpdateHours.Enabled = isActive;
                    txtUpdateRate.Enabled = isActive;
                    txtUpdateTax.Enabled = isActive;
                    txtUpdateAddress.Enabled = isActive;
                    txtUpdateContact.Enabled = isActive;

                    UpdateStatesComboBox(selectedDept);
                    statusLabel.Text = $"Текущий статус: {selectedDept.CurrentStatus}";
                    HighlightSelectedDepartmentInGrid(selectedDept);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных отдела: {ex.Message}",
                               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateStatesComboBox(HRDepartment department)
        {
            // Ищем соответствующее состояние в ComboBox
            foreach (var item in cmbStates.Items)
            {
                if (item is IHRState state && state.GetStatus() == department.CurrentStatus)
                {
                    cmbStates.SelectedItem = item;
                    return;
                }
            }

            // Если не нашли - выбираем первый элемент
            if (cmbStates.Items.Count > 0)
            {
                cmbStates.SelectedIndex = 0;
            }
        }

        private void HighlightSelectedDepartmentInGrid(HRDepartment department)
        {
            if (dgvDepartments == null || dgvDepartments.Rows.Count == 0)
                return;

            foreach (DataGridViewRow row in dgvDepartments.Rows)
            {
                if (row.Cells[0].Value?.ToString() == department.CompanyName)
                {
                    row.Selected = true;
                    dgvDepartments.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
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
            dgvDepartments.SuspendLayout();
            dgvDepartments.Rows.Clear();

            foreach (var dept in hrQueue.GetDepartments())
            {
                int rowIndex = dgvDepartments.Rows.Add(
                    dept.CompanyName,
                    dept.Employees,
                    dept.HoursPerMonth,
                    dept.HourlyRate,
                    dept.TaxRate,
                    dept.Address,
                    dept.Contact,
                    dept.CurrentStatus,
                    dept.CalculateSalary()
                );

                // Подсветка строк в зависимости от состояния
                if (dept.CurrentStatus == "Заморожен")
                    dgvDepartments.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                else if (dept.CurrentStatus == "Пассивный")
                    dgvDepartments.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
            }

            dgvDepartments.ResumeLayout();
        }
        private void PerformPerformanceTest()
        {
            int count = 500000;
            // Очистка таблицы перед заполнением новыми результатами
            lstPerformanceResults.Items.Clear();

            var stopwatch = new Stopwatch();

            // Генерация 100 000 элементов
            var departments = GenerateDepartments(count);

            // Вставка в HRQueue
            stopwatch.Start();
            var queue = new HRQueue();
            foreach (var dept in departments)
            {
                queue.Enqueue(dept);
            }
            stopwatch.Stop();
            long queueInsertTime = stopwatch.ElapsedMilliseconds;

            // Вставка в массив
            stopwatch.Reset();
            stopwatch.Start();
            var deptArray = new HRDepartment[count];
            for (int i = 0; i < count; i++)
            {
                deptArray[i] = departments[i];
            }
            stopwatch.Stop();
            long arrayInsertTime = stopwatch.ElapsedMilliseconds;

            // Добавление строк в таблицу с результатами
            lstPerformanceResults.Items.Add(new ListViewItem(new[] { "Вставка", arrayInsertTime.ToString(), queueInsertTime.ToString() }));

            // Выборка по порядку из HRQueue
            stopwatch.Reset();
            stopwatch.Start();
            foreach (var dept in queue.GetDepartments())
            {
                var dummy = dept.CompanyName; // Просто выборка для теста
            }
            stopwatch.Stop();
            long queueSelectSequentialTime = stopwatch.ElapsedMilliseconds;

            // Выборка по порядку из массива
            stopwatch.Reset();
            stopwatch.Start();
            foreach (var dept in deptArray)
            {
                var dummy = dept.CompanyName; // Просто выборка для теста
            }
            stopwatch.Stop();
            long arraySelectSequentialTime = stopwatch.ElapsedMilliseconds;

            // Добавление строк в таблицу с результатами
            lstPerformanceResults.Items.Add(new ListViewItem(new[] { "Выборка по порядку", arraySelectSequentialTime.ToString(), queueSelectSequentialTime.ToString() }));

            // Выборка случайным порядком для HRQueue
            stopwatch.Reset();
            stopwatch.Start();
            var random = new Random();
            foreach (var dept in queue.GetDepartments().OrderBy(x => random.Next()))
            {
                var dummy = dept.CompanyName; // Просто выборка для теста
            }
            stopwatch.Stop();
            long queueSelectRandomTime = stopwatch.ElapsedMilliseconds;

            // Выборка случайным порядком для массива
            stopwatch.Reset();
            stopwatch.Start();
            foreach (var dept in deptArray.OrderBy(x => random.Next()))
            {
                var dummy = dept.CompanyName; // Просто выборка для теста
            }
            stopwatch.Stop();
            long arraySelectRandomTime = stopwatch.ElapsedMilliseconds;

            // Добавление строк в таблицу с результатами
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
