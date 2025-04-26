using OOP_LB1.Controllers;
using OOP_LB1.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OOP_LB1.Views
{
    public partial class HRForm : Form
    {
        private readonly HRController _controller;

        // UI элементы
        private DataGridView _dgvDepartments;
        private ComboBox _cmbDepartments, _cmbStates;
        private TextBox _txtCompanyName, _txtEmployees, _txtHours, _txtRate, _txtTax, _txtAddress, _txtContact;
        private TextBox _txtUpdateEmployees, _txtUpdateHours, _txtUpdateRate, _txtUpdateTax, _txtUpdateAddress, _txtUpdateContact;
        private ToolStripStatusLabel _statusLabel;

        public HRForm(HRController controller)
        {
            _controller = controller;
            InitializeComponent();
            InitializeUI();
            SubscribeToEvents();

            MessageBox.Show("Вариант 7: Отдел кадров\n23ВП2\nТареева Мирошниченко",
                          "Лабораторная работа номер 5",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information);
        }

        private void InitializeUI()
        {
            // Основные настройки формы
            Text = "HR Department Management";
            Size = new Size(1000, 850);
            BackColor = Color.LightSkyBlue;

            // Создание вкладок
            var tabControl = new TabControl { Dock = DockStyle.Fill };
            tabControl.TabPages.Add(CreateDepartmentTab());
            tabControl.TabPages.Add(ManageDepartmentTab());
            tabControl.TabPages.Add(CreateViewTab());

            Controls.Add(tabControl);

            // Статус бар
            var statusStrip = new StatusStrip();
            _statusLabel = new ToolStripStatusLabel();
            statusStrip.Items.Add(_statusLabel);
            Controls.Add(statusStrip);
        }

        private void SubscribeToEvents()
        {
            _controller.DepartmentsUpdated += UpdateDepartmentsView;
            _controller.StatusChanged += UpdateStatus;
            _controller.ErrorOccurred += ShowError;
        }

        #region Вкладка создания отдела
        private TabPage CreateDepartmentTab()
        {
            var tab = new TabPage("Создать") { BackColor = Color.LightSteelBlue };
            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(350, 100, 5, 5)
            };

            // Создание элементов управления
            _txtCompanyName = new TextBox { Width = 250 };
            _txtEmployees = new TextBox { Width = 250 };
            _txtHours = new TextBox { Width = 250 };
            _txtRate = new TextBox { Width = 250 };
            _txtTax = new TextBox { Width = 250 };
            _txtAddress = new TextBox { Width = 250 };
            _txtContact = new TextBox { Width = 250 };

            var btnCreate = new Button { Text = "Создать", Width = 250 };
            btnCreate.Click += BtnCreate_Click;

            // Добавление элементов на панель
            panel.Controls.AddRange(new Control[] {
                new Label { Width = 250, Text = "Название компании" }, _txtCompanyName,
                new Label { Width = 250, Text = "Кол-во сотрудников" }, _txtEmployees,
                new Label { Width = 250, Text = "Часы в месяц" }, _txtHours,
                new Label { Width = 250, Text = "Почасовая ставка" }, _txtRate,
                new Label { Width = 250, Text = "Налог" }, _txtTax,
                new Label { Width = 250, Text = "Адрес" }, _txtAddress,
                new Label { Width = 250, Text = "Контакт" }, _txtContact,
                btnCreate
            });

            tab.Controls.Add(panel);
            return tab;
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtCompanyName.Text))
            {
                ShowError("Введите название компании");
                return;
            }

            if (!Regex.IsMatch(_txtContact.Text, @"^\+?\d{1,4}?[-.\s]?(\(?\d{1,3}?\)?[-.\s]?)?\d{1,3}[-.\s]?\d{1,3}[-.\s]?\d{1,4}$"))
            {
                ShowError("Введите корректный телефонный номер");
                return;
            }

            _controller.CreateDepartment(
                _txtCompanyName.Text,
                int.TryParse(_txtEmployees.Text, out int emp) ? emp : 0,
                double.TryParse(_txtHours.Text, out double hours) ? hours : 0,
                decimal.TryParse(_txtRate.Text, out decimal rate) ? rate : 0,
                double.TryParse(_txtTax.Text, out double tax) ? tax : 0,
                _txtAddress.Text,
                _txtContact.Text
            );
        }
        #endregion

        #region Вкладка управления отделами
        private TabPage ManageDepartmentTab()
        {
            var tab = new TabPage("Управление") { BackColor = Color.LightSteelBlue };
            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(350, 100, 5, 5)
            };

            // Элементы управления
            _cmbDepartments = new ComboBox { Width = 250 };
            _cmbDepartments.SelectedIndexChanged += CmbDepartments_SelectedIndexChanged;

            _txtUpdateEmployees = new TextBox { Width = 250 };
            _txtUpdateHours = new TextBox { Width = 250 };
            _txtUpdateRate = new TextBox { Width = 250 };
            _txtUpdateTax = new TextBox { Width = 250 };
            _txtUpdateAddress = new TextBox { Width = 250 };
            _txtUpdateContact = new TextBox { Width = 250 };

            _cmbStates = new ComboBox { Width = 250 };
            _cmbStates.Items.AddRange(new object[] { new ActiveState(), new PassiveState() });
            _cmbStates.DisplayMember = "GetStatus";
            _cmbStates.SelectedIndex = 0;

            var btnUpdate = new Button { Text = "Обновить данные", Width = 250 };
            btnUpdate.Click += BtnUpdate_Click;

            var btnChangeState = new Button { Text = "Изменить состояние", Width = 250 };
            btnChangeState.Click += BtnChangeState_Click;

            // Добавление элементов
            panel.Controls.AddRange(new Control[] {
                new Label { Width = 250, Text = "Выберите отдел:" }, _cmbDepartments,
                new Label { Width = 250, Text = "Кол-во сотрудников:" }, _txtUpdateEmployees,
                new Label { Width = 250, Text = "Часы в месяц:" }, _txtUpdateHours,
                new Label { Width = 250, Text = "Почасовая ставка:" }, _txtUpdateRate,
                new Label { Width = 250, Text = "Налог:" }, _txtUpdateTax,
                new Label { Width = 250, Text = "Адрес:" }, _txtUpdateAddress,
                new Label { Width = 250, Text = "Контакт:" }, _txtUpdateContact,
                new Label { Width = 250, Text = "Изменить состояние:" }, _cmbStates,
                btnChangeState, btnUpdate
            });

            tab.Controls.Add(panel);
            return tab;
        }

        private void CmbDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cmbDepartments.SelectedItem is HRDepartment selectedDept)
            {
                _txtUpdateEmployees.Text = selectedDept.Employees.ToString();
                _txtUpdateHours.Text = selectedDept.HoursPerMonth.ToString();
                _txtUpdateRate.Text = selectedDept.HourlyRate.ToString("N2");
                _txtUpdateTax.Text = selectedDept.TaxRate.ToString("N1");
                _txtUpdateAddress.Text = selectedDept.Address;
                _txtUpdateContact.Text = selectedDept.Contact;

                bool isActive = selectedDept.CurrentStatus == "Активный";
                _txtUpdateEmployees.Enabled = isActive;
                _txtUpdateHours.Enabled = isActive;
                _txtUpdateRate.Enabled = isActive;
                _txtUpdateTax.Enabled = isActive;
                _txtUpdateAddress.Enabled = isActive;
                _txtUpdateContact.Enabled = isActive;

                UpdateStatus($"Текущий статус: {selectedDept.CurrentStatus}");
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (_cmbDepartments.SelectedItem is HRDepartment selectedDept)
            {
                if (selectedDept.CurrentStatus == "Пассивный")
                {
                    ShowError("Невозможно изменить данные отдела в пассивном состоянии");
                    return;
                }

                // Проверка валидности введенных данных
                if (!int.TryParse(_txtUpdateEmployees.Text, out int employees) || employees < 0)
                {
                    ShowError("Введите корректное количество сотрудников (целое положительное число)");
                    return;
                }

                if (!double.TryParse(_txtUpdateHours.Text, out double hours) || hours <= 0)
                {
                    ShowError("Введите корректное количество часов (положительное число)");
                    return;
                }

                if (!decimal.TryParse(_txtUpdateRate.Text, out decimal rate) || rate <= 0)
                {
                    ShowError("Введите корректную почасовую ставку (положительное число)");
                    return;
                }

                if (!double.TryParse(_txtUpdateTax.Text, out double tax) || tax < 0 || tax > 100)
                {
                    ShowError("Введите корректный налог (число от 0 до 100)");
                    return;
                }

                if (string.IsNullOrWhiteSpace(_txtUpdateAddress.Text))
                {
                    ShowError("Введите адрес отдела");
                    return;
                }

                if (!Regex.IsMatch(_txtUpdateContact.Text, @"^\+?\d{1,4}?[-.\s]?(\(?\d{1,3}?\)?[-.\s]?)?\d{1,3}[-.\s]?\d{1,3}[-.\s]?\d{1,4}$"))
                {
                    ShowError("Введите корректный телефонный номер");
                    return;
                }

                try
                {
                    // Вызов метода контроллера для обновления данных
                    _controller.UpdateDepartment(
                        selectedDept,
                        employees,
                        hours,
                        rate,
                        tax,
                        _txtUpdateAddress.Text,
                        _txtUpdateContact.Text
                    );

                    // Обновление статусной строки
                    UpdateStatus($"Данные отдела '{selectedDept.CompanyName}' успешно обновлены");
                }
                catch (Exception ex)
                {
                    ShowError($"Ошибка при обновлении данных: {ex.Message}");
                }
            }
            else
            {
                ShowError("Выберите отдел для обновления");
            }
        }

        private void BtnChangeState_Click(object sender, EventArgs e)
        {
            if (_cmbDepartments.SelectedItem is HRDepartment selectedDept &&
                _cmbStates.SelectedItem is IHRState selectedState)
            {
                _controller.ChangeState(selectedDept, selectedState);
            }
        }
        #endregion

        #region Вкладка просмотра
        private TabPage CreateViewTab()
        {
            var tab = new TabPage("Просмотр") { BackColor = Color.LightSteelBlue };

            _dgvDepartments = new DataGridView { Dock = DockStyle.Fill };
            _dgvDepartments.Columns.AddRange(
                new DataGridViewTextBoxColumn { HeaderText = "Название компании", Width = 150 },
                new DataGridViewTextBoxColumn { HeaderText = "Кол-во сотрудников", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "Часы в месяц", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "Почасовая ставка", Width = 120 },
                new DataGridViewTextBoxColumn { HeaderText = "Налог", Width = 80 },
                new DataGridViewTextBoxColumn { HeaderText = "Адрес", Width = 150 },
                new DataGridViewTextBoxColumn { HeaderText = "Контакт", Width = 120 },
                new DataGridViewTextBoxColumn { HeaderText = "Статус", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "Зарплата", Width = 120 }
            );

            tab.Controls.Add(_dgvDepartments);
            return tab;
        }
        #endregion

        #region Методы обновления UI
        private void UpdateDepartmentsView(IEnumerable<HRDepartment> departments)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateDepartmentsView(departments)));
                return;
            }

            _dgvDepartments.Rows.Clear();
            _cmbDepartments.Items.Clear();

            foreach (var dept in departments)
            {
                _dgvDepartments.Rows.Add(
                    dept.CompanyName,
                    dept.Employees,
                    dept.HoursPerMonth,
                    dept.HourlyRate.ToString("C"),
                    dept.TaxRate.ToString("P1"),
                    dept.Address,
                    dept.Contact,
                    dept.CurrentStatus,
                    dept.CalculateSalary().ToString("C")
                );

                _cmbDepartments.Items.Add(dept);
            }

            if (_cmbDepartments.Items.Count > 0)
                _cmbDepartments.SelectedIndex = 0;
        }

        private void UpdateStatus(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateStatus(message)));
                return;
            }
            _statusLabel.Text = message;
        }

        private void ShowError(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowError(message)));
                return;
            }
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
    }
}