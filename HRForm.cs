using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OOP_LB1
{
    public partial class HRForm : Form
    {
        private List<HRDepartment> departments = new List<HRDepartment>();
        private ComboBox cmbDepartments;

        public HRForm()
        {
            InitializeComponent();
            InitializeFormComponents();
        }

        private void InitializeFormComponents()
        {
            this.Text = "HR Department Management";
            this.Size = new System.Drawing.Size(600, 500);
            this.BackColor = System.Drawing.Color.LightSkyBlue; // Фон формы

            TabControl tabControl = new TabControl { Dock = DockStyle.Fill, BackColor = System.Drawing.Color.AliceBlue };
            TabPage createPage = new TabPage("Создать") { BackColor = System.Drawing.Color.LightSteelBlue };
            TabPage updatePage = new TabPage("Обновить") { BackColor = System.Drawing.Color.LightSteelBlue };
            TabPage viewPage = new TabPage("Просмотр") { BackColor = System.Drawing.Color.LightSteelBlue }; // Новая вкладка

            FlowLayoutPanel createPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, FlowDirection = FlowDirection.TopDown };
            FlowLayoutPanel updatePanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, FlowDirection = FlowDirection.TopDown };

            // Поля для создания объекта
            TextBox txtCompanyName = new TextBox { Width = 200, Text = "Название компании", BackColor = System.Drawing.Color.LightBlue };
            TextBox txtEmployees = new TextBox { Width = 200, Text = "Кол-во сотрудников", BackColor = System.Drawing.Color.LightBlue };
            TextBox txtHours = new TextBox { Width = 200, Text = "Часы в месяц", BackColor = System.Drawing.Color.LightBlue };
            TextBox txtRate = new TextBox { Width = 200, Text = "Почасовая ставка", BackColor = System.Drawing.Color.LightBlue };
            TextBox txtTax = new TextBox { Width = 200, Text = "Налог", BackColor = System.Drawing.Color.LightBlue };
            TextBox txtAddress = new TextBox { Width = 200, Text = "Адрес", BackColor = System.Drawing.Color.LightBlue };
            TextBox txtContact = new TextBox { Width = 200, Text = "Контакт", BackColor = System.Drawing.Color.LightBlue };
            Button btnCreate = new Button { Text = "Создать", Width = 200, BackColor = System.Drawing.Color.SkyBlue, ForeColor = System.Drawing.Color.White };

            btnCreate.Click += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
                {
                    MessageBox.Show("Введите название компании");
                    return;
                }

                HRDepartment department = new HRDepartment(
                    txtCompanyName.Text,
                    int.TryParse(txtEmployees.Text, out int emp) ? emp : 0,
                    double.TryParse(txtHours.Text, out double hours) ? hours : 0,
                    decimal.TryParse(txtRate.Text, out decimal rate) ? rate : 0,
                    double.TryParse(txtTax.Text, out double tax) ? tax : 0,
                    txtAddress.Text,
                    txtContact.Text
                );
                departments.Add(department);
                MessageBox.Show("Отдел создан!");
                UpdateDepartmentList();
            };

            createPanel.Controls.AddRange(new Control[] { txtCompanyName, txtEmployees, txtHours, txtRate, txtTax, txtAddress, txtContact, btnCreate });
            createPage.Controls.Add(createPanel);

            // Обновление объекта
            cmbDepartments = new ComboBox { Width = 200, BackColor = System.Drawing.Color.LightBlue };
            TextBox txtUpdateEmployees = new TextBox { Width = 200, Text = "Кол-во сотрудников", BackColor = System.Drawing.Color.LightBlue };
            TextBox txtUpdateHours = new TextBox { Width = 200, Text = "Часы в месяц", BackColor = System.Drawing.Color.LightBlue };
            TextBox txtUpdateRate = new TextBox { Width = 200, Text = "Почасовая ставка", BackColor = System.Drawing.Color.LightBlue };
            TextBox txtUpdateTax = new TextBox { Width = 200, Text = "Налог", BackColor = System.Drawing.Color.LightBlue };
            TextBox txtUpdateAddress = new TextBox { Width = 200, Text = "Адрес", BackColor = System.Drawing.Color.LightBlue };
            TextBox txtUpdateContact = new TextBox { Width = 200, Text = "Контакт", BackColor = System.Drawing.Color.LightBlue };
            Button btnUpdate = new Button { Text = "Обновить", Width = 200, BackColor = System.Drawing.Color.SkyBlue, ForeColor = System.Drawing.Color.White };

            cmbDepartments.SelectedIndexChanged += (sender, e) =>
            {
                if (cmbDepartments.SelectedItem is HRDepartment selectedDept)
                {
                    txtUpdateEmployees.Text = selectedDept.Employees.ToString();
                    txtUpdateHours.Text = selectedDept.HoursPerMonth.ToString();
                    txtUpdateRate.Text = selectedDept.HourlyRate.ToString();
                    txtUpdateTax.Text = selectedDept.TaxRate.ToString();
                    txtUpdateAddress.Text = selectedDept.Address;
                    txtUpdateContact.Text = selectedDept.Contact;
                }
            };

            btnUpdate.Click += (sender, e) =>
            {
                if (cmbDepartments.SelectedItem is HRDepartment selectedDept)
                {
                    selectedDept.Employees = int.TryParse(txtUpdateEmployees.Text, out int emp) ? emp : selectedDept.Employees;
                    selectedDept.HoursPerMonth = double.TryParse(txtUpdateHours.Text, out double hours) ? hours : selectedDept.HoursPerMonth;
                    selectedDept.HourlyRate = decimal.TryParse(txtUpdateRate.Text, out decimal rate) ? rate : selectedDept.HourlyRate;
                    selectedDept.TaxRate = double.TryParse(txtUpdateTax.Text, out double tax) ? tax : selectedDept.TaxRate;
                    selectedDept.Address = txtUpdateAddress.Text;
                    selectedDept.Contact = txtUpdateContact.Text;
                    MessageBox.Show("Объект обновлен!");
                    UpdateDepartmentList();
                }
            };

            updatePanel.Controls.AddRange(new Control[] { cmbDepartments, txtUpdateEmployees, txtUpdateHours, txtUpdateRate, txtUpdateTax, txtUpdateAddress, txtUpdateContact, btnUpdate });
            updatePage.Controls.Add(updatePanel);

            // Вкладка для вывода
            DataGridView dgvDepartments = new DataGridView
            {
                Dock = DockStyle.Fill,  // Заполняем всю область вкладки
                AutoGenerateColumns = false,
                BackgroundColor = System.Drawing.Color.LightSkyBlue,
                AllowUserToAddRows = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right // Сделаем таблицу адаптивной

            };

            // Добавляем столбцы
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Название компании", DataPropertyName = "CompanyName", Width = 200 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Кол-во сотрудников", DataPropertyName = "Employees", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Часы в месяц", DataPropertyName = "HoursPerMonth", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Почасовая ставка", DataPropertyName = "HourlyRate", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Налог", DataPropertyName = "TaxRate", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Адрес", DataPropertyName = "Address", Width = 200 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Контакт", DataPropertyName = "Contact", Width = 150 });

            // Заполняем DataGridView данными
            dgvDepartments.DataSource = departments;

            dgvDepartments.SelectionMode = DataGridViewSelectionMode.CellSelect; // Запрещает выделение строк
            dgvDepartments.MultiSelect = false; // Запрещает выделение нескольких ячеек


            viewPage.Controls.Add(dgvDepartments);  // Добавляем таблицу на вкладку "Просмотр"


            tabControl.TabPages.AddRange(new TabPage[] { createPage, updatePage, viewPage }); // Добавляем новую вкладку
            this.Controls.Add(tabControl);
        }

        private void UpdateDepartmentList()
        {
            cmbDepartments.Items.Clear();
            foreach (var dept in departments)
            {
                cmbDepartments.Items.Add(dept);
            }

            // DisplayMember, чтобы отображались только названия компаний
            cmbDepartments.DisplayMember = "CompanyName";
        }

    }
}
